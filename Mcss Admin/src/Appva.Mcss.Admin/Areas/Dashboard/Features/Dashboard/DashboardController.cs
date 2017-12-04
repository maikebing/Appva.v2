// <copyright file="DashboardController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Controllers
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("dashboard")]
    [Permissions(Permissions.Dashboard.ReadValue)]
    public sealed class DashboardController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region Routes.

        #region Index.

        /// <summary>
        /// Returns the dashboard view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route]
        [HttpGet]
        public ActionResult Index()
        {
            return View(new HomeViewModel
                {
                    HasCalendarOverview      = this.settingsService.HasCalendarOverview(),
                    HasOrderOverview         = this.settingsService.HasOrderRefill(),
                    ArticleModuleIsInstalled = this.settingsService.Find<OrderListConfiguration>(ApplicationSettings.OrderListSettings).IsInstalled,
                    SevenDayStartDate        = DateTime.Now.AddDays(-7),
                    SevenDayEndDate          = DateTime.Now   
                });
        }

        #endregion

        #region Load widgets.

        /// <summary>
        /// Loads a new widget.
        /// </summary>
        /// <param name="header">The header</param>
        /// <param name="action">The action route</param>
        /// <param name="action">The controller route</param>
        /// <returns><see cref="LoadWidget"/></returns>
        [Route("load")]
        [HttpGet]
        public PartialViewResult Load(string header, string action, string controller, string widgetArea)
        {
            return PartialView(LoadWidget.CreateNew(header, action, controller, widgetArea));
        }

        #endregion

        #endregion
    }
}