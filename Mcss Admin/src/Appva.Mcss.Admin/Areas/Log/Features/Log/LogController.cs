// <copyright file="LogController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Log.Features.Log
{
    #region Imports.

    using Appva.Mcss.Admin.Areas.Log.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("log"), RoutePrefix("")]
    public sealed class LogController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogController"/> class.
        /// </summary>
        public LogController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Returns the list view.
        /// </summary>
        /// <returns>The view</returns>
        [Route("list")]
        [HttpGet, Dispatch]
        public ActionResult List(ListLog request)
        {
            return this.View();
        }

        #endregion
    }
}