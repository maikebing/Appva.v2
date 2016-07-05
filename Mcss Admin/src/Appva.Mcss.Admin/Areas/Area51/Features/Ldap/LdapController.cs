// <copyright file="LdapController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Ldap
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("ldap")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class LdapController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapController"/> class.
        /// </summary>
        public LdapController()
        {
        }

        #endregion

        #region Routes.

        #region Index

        [Route("index")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        #region Config

        /// <summary>
        /// Update configuration for the ldap connection
        /// </summary>
        /// <returns></returns>
        [Route("config")]
        [HttpGet, Dispatch]
        public ActionResult Config(LdapConfig request)
        {
            return this.View();
        }

        /// <summary>
        /// Update configuration for the ldap connection
        /// </summary>
        /// <returns></returns>
        [Route("config")]
        [HttpPost, Dispatch("list","ldap")]
        public ActionResult Config(LdapConfigModel request)
        {
            return this.View();
        }

        #endregion

        [Route("finduser")]
        [HttpPost]
        [Dispatch]
        public ActionResult FindUser(FindUser request)
        {
            return this.View();
        }

        [Route("list")]
        [HttpGet]
        [Dispatch]
        public ActionResult List(ListLdap request)
        {
            return this.View();
        }

        #endregion
    }
}