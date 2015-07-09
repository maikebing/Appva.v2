// <copyright file="ReportDataController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Statistics
{
    #region Imports.

    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Mcss.Admin.Features.Statistics.Chart;
    using Appva.Mcss.Admin.Features.Statistics.Data;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("statistics")]
    public class StatisticsController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsController"/> class.
        /// </summary>
        public StatisticsController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Deletes an activity by id.
        /// </summary>
        /// <param name="id">The task id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("chart")]
        [HttpGet, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult GetChartData(StatisticsChartData request)
        {
            return this.JsonGet();
        }

         /// <summary>
        /// Returns the dashboard seven day report chart json data.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("data")]
        [HttpGet, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult GetStatisicsData(StatisticsData request)
        {
            return this.JsonGet();
        }

        #endregion
    }
}