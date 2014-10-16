// <copyright file="UnprocessableEntityResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Results
{
    #region Imports.

    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    #endregion

    /// <summary>
    /// The 422 (Unprocessable Entity) status code means the server
    /// understands the content type of the request entity (hence a
    /// 415(Unsupported Media Type) status code is inappropriate), and the
    /// syntax of the request entity is correct (thus a 400 (Bad Request)
    /// status code is inappropriate) but was unable to process the contained 
    /// instructions.  For example, this error condition may occur if an XML 
    /// request body contains well-formed (i.e., syntactically correct), but 
    /// semantically erroneous, XML instructions.
    /// <link>https://tools.ietf.org/html/rfc4918</link>
    /// </summary>
    /// <typeparam name="T">The result type</typeparam>
    public sealed class UnprocessableEntityResult<T> : NegotiatedContentResult<T>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityResult{T}"/> class.
        /// </summary>
        /// <param name="controller">The <see cref="ApiController"/></param>
        /// <param name="content">The result object</param>
        public UnprocessableEntityResult(ApiController controller, T content)
            : base((HttpStatusCode) 422, content, controller)
        {
        }

        #endregion

        #region NegotiatedContentResult{T} Overrides.

        /// <inheritdoc/>
        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await base.ExecuteAsync(cancellationToken);
        }

        #endregion
    }
}