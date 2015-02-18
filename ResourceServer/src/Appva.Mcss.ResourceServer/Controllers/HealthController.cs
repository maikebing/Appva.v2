// <copyright file="HealthController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System.Web.Http;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HealthController : ApiController
    {
        #region Routes.

        /// <summary>
        /// Returns ok if everything is ok :D
        /// </summary>
        /// <returns>Ok</returns>
        [HttpGet, Route("v1/health")]
        public IHttpActionResult Health()
        {
            return this.Ok("ok");
        }

        #endregion
    }
}