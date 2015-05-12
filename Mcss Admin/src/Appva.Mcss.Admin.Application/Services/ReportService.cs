// <copyright file="ReportService.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IReportService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Report Create(IReportingFilter filter, DateTime? startDate, DateTime? endDate, int? page = 1, int? size = 30);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ReportService : IReportService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportService"/> class.
        /// </summary>
        public ReportService(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region IReportService Members.
        
        /// <inheritdoc /> 
        public Report Create(IReportingFilter filter, DateTime? startDate, DateTime? endDate, int? page = 1, int? size = 30)
        {
            //// TODO: This should be firstInstantOfDay here
            var start = startDate ?? DateTime.Now.FirstOfMonth();
            var end = (endDate.HasValue) ? endDate.Value.LastInstantOfDay() : DateTime.Now.LastInstantOfDay();
            var span = end.Subtract(start).Days;
            var tasks = QueryAndFilter(filter, start, end);
            var tasksWithinStartDateAndEndDate = tasks.Where(x => x.Scheduled >= start && x.Scheduled <= end)
                .OrderBy(x => x.Scheduled).Desc;
            var dateSpan = GenerateReportSegment(tasksWithinStartDateAndEndDate.ToRowCountQuery());
            var total = tasksWithinStartDateAndEndDate.ToRowCountQuery().RowCount();
            var comparableDateSpan = GenerateReportSegment(tasks.ToRowCountQuery()
                .Where(x => x.Scheduled >= start.AddDays(-span) && x.Scheduled <= end.AddDays(-span)));
            var items = tasksWithinStartDateAndEndDate
                .Skip(((page.Value - 1) * size.Value)).Take(size.Value).List().Where(task => task != null).ToList();
            return new Report
            {
                StartDate = start,
                EndDate = end,
                TasksOnTime = dateSpan.TasksOnTime,
                TasksNotOnTime = dateSpan.TasksNotOnTime,
                ComparedDateSpanTasksOnTime = comparableDateSpan.TasksOnTime,
                ComparedDateSpanTasksNotOnTime = comparableDateSpan.TasksNotOnTime,
                AverageDifferenceInTime = dateSpan.AverageDifferenceInTime,
                ComparedAverageDifferenceInTime = comparableDateSpan.AverageDifferenceInTime,
                Search = new ReportSearch<Task>
                {
                    Items = items,
                    PageSize = page.Value,
                    PageNumber = size.Value,
                    TotalItemCount = total
                }
            };
        }

        #endregion

        #region Private Methods.

        private IQueryOver<Task, Task> QueryAndFilter(IReportingFilter filter, DateTime startDate, DateTime endDate)
        {
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= startDate.AddDays(-endDate.Subtract(startDate).Days))
                .And(x => x.Scheduled <= endDate)
                .Fetch(x => x.Patient).Eager
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            filter.Filter(query);
            return query;
        }

        private ReportSegment GenerateReportSegment(IQueryOver<Task, Task> tasks)
        {
            var percent = 100.00;
            var tasksOnTime = 0.0;
            var tasksNotOnTime = 0.0;
            var minutes = 0.0;
            tasksOnTime = tasks.ToRowCountQuery().Where(task => !task.Delayed).RowCount();
            tasksNotOnTime = tasks.ToRowCountQuery().Where(task => task.Delayed).RowCount();
            var signedTasksNotOnTime = tasks.ToRowCountQuery().Where(task => task.Delayed && task.IsCompleted).RowCount();
            var count = tasksOnTime + tasksNotOnTime;
            minutes = tasks.ToRowCountQuery().Where(task => task.Delayed && task.IsCompleted)
                .Select(Projections.Sum(Projections.SqlProjection("datediff(n, Scheduled , this_.CompletedDate) as Minutes", new[] { "Minutes" }, new[] { NHibernateUtil.Double }))).SingleOrDefault<double>();
            return new ReportSegment
            {
                TasksOnTime = count > 0 ? Math.Round((tasksOnTime / count) * percent) : 0,
                TasksNotOnTime = count > 0 ? Math.Round((tasksNotOnTime / count) * percent) : 0,
                AverageDifferenceInTime = count > 0 ? Math.Round(minutes / count) : 0
            };
        }

        private class ReportSegment
        {
            public double TasksOnTime
            {
                get;
                set;
            }
            public double TasksNotOnTime
            {
                get;
                set;
            }
            public double AverageDifferenceInTime
            {
                get;
                set;
            }
        }
        
        #endregion
    }
}