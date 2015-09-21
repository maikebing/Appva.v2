// <copyright file="NewsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Notification.Features.News
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Models;
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
    [RouteArea("notification"), RoutePrefix("news")]
    public sealed class NewsController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsController"/> class.
        /// </summary>
        public NewsController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Shows an popup notice
        /// </summary>
        /// <param name="request">The account id</param>
        /// <returns>A popup notice</returns>
        [Route("popup")]
        [HttpGet, Dispatch]
        public PartialViewResult Popup(PopupNews request)
        {
            return PartialView();
        }

        #region Activate.

        /// <summary>
        /// Returns the home view.
        /// </summary>
        /// <returns>The view</returns>
        [Route("activate")]
        [HttpGet, Dispatch("List", "Notification")]
        [PermissionsAttribute(Permissions.News.UpdateValue)]
        public ActionResult Activate(ActivateNews request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}