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
    using Appva.Mcss.Admin.Features.Area51.Cache;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("settings")]
    public sealed class SettingsController : Controller
    {
        #region Routes.

        /// <summary>
        /// Returns all settings for the current tenant.
        /// </summary>
        /// <returns>The settings list view</returns>
        [HttpGet, Route("list"), Dispatch(typeof(Parameterless<IEnumerable<ListSetting>>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion
    }
}