// <copyright file="NotModifiedResult.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NotModifiedResult : IHttpActionResult
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="ApiController"/>.
        /// </summary>
        private readonly ApiController controller;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotModifiedResult"/> class.
        /// </summary>
        /// <param name="controller">The <see cref="ApiController"/></param>
        public NotModifiedResult(ApiController controller)
        {
            this.controller = controller;
        }

        #endregion

        #region IHttpActionResult Members

        /// <inheritdoc />
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.controller.Request.CreateResponse(HttpStatusCode.NotModified);
            return Task.FromResult(response);
        }

        #endregion
    }
}