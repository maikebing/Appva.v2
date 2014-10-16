// <copyright file="OkOrNotModifiedResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Results
{
    #region Imports.

    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The result type</typeparam>
    public sealed class OkOrNotModifiedResult<T> : NegotiatedContentResult<T> where T : class, IETag
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ApiController"/>.
        /// </summary>
        private readonly ApiController controller;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OkOrNotModifiedResult{T}"/> class.
        /// </summary>
        /// <param name="controller">The <see cref="ApiController"/></param>
        /// <param name="content">The result object</param>
        public OkOrNotModifiedResult(ApiController controller, T content)
            : base(HttpStatusCode.OK, content, controller)
        {
            this.controller = controller;
        }

        #endregion

        #region NegotiatedContentResult{T} Overrides.

        /// <inheritdoc />
        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var request = this.Request;
            if (request.Method.Equals(HttpMethod.Get) && request.Headers.IfNoneMatch.Count > 0)
            {
                var ifNoneMatch = request.Headers.IfNoneMatch.FirstOrDefault();
                if (ifNoneMatch.IsNotNull())
                {
                    string checksum = ifNoneMatch.Tag.Replace("\"", string.Empty);
                    if (this.Content.ToString().ToHex().ToLower().Equals(checksum))
                    {
                        return await new NotModifiedResult(this.controller).ExecuteAsync(cancellationToken);
                    }
                }
            }
            if (request.Method == HttpMethod.Put && request.Headers.IfMatch.Count > 0)
            {
                var ifMatch = request.Headers.IfMatch.FirstOrDefault();
                if (ifMatch.IsNotNull())
                {
                    string checksum = ifMatch.Tag.Replace("\"", string.Empty);
                    if (this.Content.ToString().ToHex().ToLower().Equals(checksum))
                    {
                        return await new ConflictResult(this.controller).ExecuteAsync(cancellationToken);
                    }
                }
            }
            var response = await base.ExecuteAsync(cancellationToken);
            response.Headers.ETag = new EntityTagHeaderValue("\"" + this.Content.ToString().ToHex().ToLower() + "\"", false);
            return response;
        }

        #endregion
    }
}