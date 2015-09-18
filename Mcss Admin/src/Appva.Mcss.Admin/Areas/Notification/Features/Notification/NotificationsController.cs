// <copyright file="NotificationsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Notification.Features.Notifications
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
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
    [RouteArea("notification"), RoutePrefix("")]
    [Permissions(Permissions.Notification.ReadValue)]
    public sealed class NotificationController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// </summary>
        public NotificationController()
        {
        }

        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Returns the list view.
        /// </summary>
        /// <returns>The view</returns>
        [Route]
        [HttpGet]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}