// <copyright file="TestController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.Monitoring.Features.Test
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.Monitoring.Controllers.Test;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("v1/test")]
    public class TestController : ApiController
    {
        #region Routes.

        [HttpGet, Route, Dispatch(typeof(TestRequest))]
        public IHttpActionResult Test()
        {
            return this.BadRequest();
        }

        #endregion
    }
}