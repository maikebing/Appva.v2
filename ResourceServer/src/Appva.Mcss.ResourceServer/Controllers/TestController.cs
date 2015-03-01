// <copyright file="TestController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    /*
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Appva.Apis.TenantServer;
    using Appva.Apis.TenantServer.Contracts;
    using Appva.Logging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("v1/test")]
    public sealed class TestController : ApiController
    {
        /// <summary>
        /// The <see cref="ITenantClient"/>.
        /// </summary>
        private readonly ITenantClient tenantClient;

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        public TestController(ITenantClient tenantClient)
        {
            this.tenantClient = tenantClient;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Dead lock test yay!.
        /// </summary>
        /// <returns>Will never return anything</returns>
        [HttpGet, Route("deadlock")]
        public IHttpActionResult Deadlock()
        {
            //// Block the context here.
            var result = this.tenantClient.ListAllAsync().Result;
            //// We will never get here since it's blocked in this current context 
            //// when internal method is trying to reattach.
            return this.Ok(result);
        }

        /// <summary>
        /// Simple client test without deadlocks.
        /// </summary>
        /// <returns>A list of tenants</returns>
        [HttpGet, Route("tenantserverclient/list")]
        public IHttpActionResult ListAllTenants()
        {
            var result = this.tenantClient.ListAll();
            return this.Ok(result);
        }

        #endregion
    }
    */
}