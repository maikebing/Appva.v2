// <copyright file="SettingsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Settings
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Area51.Features.Settings.Create;
    using Appva.Mcss.Admin.Features.Area51.Cache;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AuthorizeUserAndTenantAttribute]
    [RouteArea("area51"), RoutePrefix("settings")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class SettingsController : Controller
    {
        #region Routes.

        #region List Settings.

        /// <summary>
        /// Returns all settings for the current tenant.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{ListSetting}"/></returns>
        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<IEnumerable<ListSetting>>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Create Setting.

        /// <summary>
        /// Creates a new setting for the current tenant.
        /// </summary>
        /// <returns>The settings list view</returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreateSetting>))]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Creates a new setting for the current tenant.
        /// </summary>
        /// <returns>The settings list view</returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult Create(CreateSetting request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}