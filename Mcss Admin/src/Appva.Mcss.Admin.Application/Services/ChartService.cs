// <copyright file="ChartService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IChartService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IList<object[]> Create(IReportingFilter filter, DateTime startDate, DateTime endDate);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ChartService : IChartService
    {
        /*#region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartService"/> class.
        /// </summary>
        public ChartService(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion
        
        #region IChartService Members.

        /// <inheritdoc />
        public IList<object[]> Create(IReportingFilter filter, DateTime startDate, DateTime endDate)
        {
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.IsActive == true)
                .And(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= startDate)
                .And(x => x.Scheduled <= endDate.LastInstantOfDay())
                .OrderBy(x => x.Scheduled)
                .Asc;
            filter.Filter(query);
            return this.GenerateChartData(query.List());
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private List<object[]> GenerateChartData(IList<Task> tasks)
        {
            var dataPoints = new Dictionary<DateTime, ChartData>();
            foreach (var task in tasks)
            {
                if (task.Delayed)
                {
                    AddOnTimeOrNotOnTime(false, task.Scheduled.Date, dataPoints);
                }
                else
                {
                    AddOnTimeOrNotOnTime(true, task.Scheduled.Date, dataPoints);
                }
            }
            dataPoints.OrderBy(x => x.Key);
            List<object[]> result = new List<object[]>();
            foreach (var dataPoint in dataPoints)
            {
                TimeSpan span = new TimeSpan(DateTime.Parse("1970-01-01").Ticks);
                var onTime = dataPoint.Value.OnTime;
                var total = dataPoint.Value.OnTime + dataPoint.Value.NotOnTime;
                result.Add(new object[] { dataPoint.Key.Subtract(span).Ticks / 10000, ((onTime) / (total) * 100.00) });
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onTime"></param>
        /// <param name="date"></param>
        /// <param name="data"></param>
        private void AddOnTimeOrNotOnTime(bool onTime, DateTime date, Dictionary<DateTime, ChartData> data)
        {
            if (data.ContainsKey(date))
            {
                if (onTime)
                {
                    data[date].OnTime = data[date].OnTime + 1.0;
                }
                else
                {
                    data[date].NotOnTime = data[date].NotOnTime + 1.0;
                }
            }
            else
            {
                if (onTime)
                {
                    data.Add(date, new ChartData(1.0, 0.0));
                }
                else
                {
                    data.Add(date, new ChartData(0.0, 1.0));
                }
            }
        }
        
        #endregion*/
        public IList<object[]> Create(IReportingFilter filter, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}