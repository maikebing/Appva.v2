// <copyright file="ErrorController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Error
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("oh-no")]
    public sealed class ErrorController : Controller
    {
        #region Routes.

        /// <summary>
        /// Returns a 404
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("404/not-found")]
        public ActionResult FileNotFound()
        {
            return this.View();
        }

        /// <summary>
        /// Returns a HTTP 400 Bad request when no tenant
        /// identity can be identified.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("400/bad-request")]
        public ActionResult TenantNotFound()
        {
            return this.View();
        }

        /// <summary>
        /// Returns a HTTP 403 if the current tenant is not
        /// authorized for the URL.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("403/forbidden")]
        public ActionResult TenantAndHostAreNotCompatible()
        {
            return this.View();
        }

        #endregion
    }
}