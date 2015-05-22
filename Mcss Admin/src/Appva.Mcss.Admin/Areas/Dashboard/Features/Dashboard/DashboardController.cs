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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("dashboard")]
    public sealed class DashboardController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        public DashboardController()
        {
        }

        #endregion

        #region Routes.

        #region Index.

        /// <summary>
        /// Returns the dashboard view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            /*var identity = Identity();
            var view = ExecuteCommand<HomeViewModel>(new CreateOverviewCommand
            {
                Identity = identity
            });
            view.SevenDayStartDate = DateTimeExt.Now().AddDays(-7);
            view.SevenDayEndDate = DateTimeExt.Now();*/
            return View(new HomeViewModel
                {
                   
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
        [HttpGet]
        [Route("load")]
        public PartialViewResult Load(string header, string action, string controller, string widgetArea)
        {
            return PartialView(LoadWidget.CreateNew(header, action, controller, widgetArea));
        }

        #endregion

        #region Chart.

        /*
        /// <summary>
        /// Returns the dashboard overview chart json data.
        /// </summary>
        /// <param name="sId">The schedule settings id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult Chart(Guid? sId, DateTime startDate, DateTime endDate)
        {
            var taxon = FilterCache.Get(Session);
            Guid? taxonId = null;
            if (taxon != null)
            {
                taxonId = taxon.Id;
            }
            var scheduleSettings = sId.HasValue ? Session.Get<ScheduleSettings>(sId.Value) : null;
            return Json(ExecuteCommand<List<object[]>>(new CreateChartCommand<FullReportFilter>
            {
                StartDate = startDate,
                EndDate = endDate,
                Filter = new FullReportFilter
                {
                    TaxonId = taxonId,
                    ScheduleSettingsId = sId,
                    ScheduleSettings = scheduleSettings
                }
            }),
                JsonRequestBehavior.AllowGet
            );
        }*/

        #endregion

        #region Report.

        /*
        /// <summary>
        /// Returns the dashboard seven day report chart json data.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult GetSevenDayReport()
        {
            var taxon = FilterCache.Get(Session);
            Guid? taxonId = null;
            if (taxon != null)
            {
                taxonId = taxon.Id;
            }
            var result = ExecuteCommand<ReportViewModel>(new CreateReportCommand<FullReportFilter>
            {
                StartDate = DateTimeExt.Now().AddDays(-7),
                EndDate = DateTimeExt.Now(),
                Filter = new FullReportFilter
                {
                    TaxonId = taxonId
                }
            });
            return Json(new
            {
                TasksNotOnTime = result.TasksNotOnTime,
                TasksOnTime = result.TasksOnTime,
                ComparedDateSpanTasksOnTime = result.ComparedDateSpanTasksOnTime
            },
                JsonRequestBehavior.AllowGet
            );
        }*/

        #endregion

        #endregion
    }
}