// <copyright file="HealthController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HealthController : ApiController
    {
        private readonly IPersistenceContext context;
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthController"/> class.
        /// </summary>
        public HealthController(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region Routes.

        [HttpGet, Route("v1/health")]
        [AuthorizeToken(Scope.ReadWrite)]
        public IHttpActionResult Health()
        {
            return Ok("ok");
        }

        #endregion
    }
}