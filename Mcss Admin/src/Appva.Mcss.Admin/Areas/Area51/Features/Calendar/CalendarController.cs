// <copyright file="CalendarController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Calendar
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
    [RouteArea("area51"), RoutePrefix("calendar")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class CalendarController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarController"/> class.
        /// </summary>
        public CalendarController()
        {
        }

        #endregion

        #region Routes.

        [Route("index")]
        [HttpGet, Dispatch]
        public ActionResult Index(CalendarIndex request)
        {
            return this.View();
        }

        [Route("install-colors")]
        [HttpPost, Dispatch("index","calendar")]
        public ActionResult InstallColors(InstallColors request)
        {
            return this.View();
        }


        #endregion
    }
}