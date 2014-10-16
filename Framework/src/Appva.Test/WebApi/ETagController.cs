// <copyright file="ETagController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Http
{
    #region Imports.

    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Appva.WebApi;
    using Appva.WebApi.Extensions;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a summary for readability.
    /// </summary>
    [RoutePrefix("v0/etag")]
    public class ETagController : ApiController
    {
        /// <summary>
        /// TODO: Add a summary for readability.
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var retval = await Task.Run(() => new ETagResponseModel()
            {
                Id = id,
                Version = 1
            });
            return this.OkOrNotModified(retval);
        }

        /// <summary>
        /// TODO: Add a summary for readability.
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id, ETagRequestModel model)
        {
            var retval = await Task.Run(() => new ETagResponseModel()
            {
                Id = id,
                Version = 1
            });
            return this.OkOrNotModified(retval);
        }
    }
}
