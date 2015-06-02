// <copyright file="ReportController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Report.Features
{
    #region Imports.

    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// 
    [RouteArea("report")]
    public class ReportController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        public ReportController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Creates a full report for the organisation
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("full")]
        [HttpGet, Dispatch]
        public ActionResult FullReport(FullReport request)
        {
            return this.View();
        }

        #endregion
    }
}