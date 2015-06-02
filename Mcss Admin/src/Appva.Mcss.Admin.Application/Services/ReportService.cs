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

        /// <summary>
        /// Returns a list of <see cref="ChartPoint>"/> for a chart
        /// </summary>
        /// <param name="filter">The filter for the chart</param>
        /// <returns>List of <see cref="ReportData"/></returns>
        IList<ReportData> GetChartData(ChartDataFilter filter);

        /// <summary>
        /// Returns a list of <see cref="ChartPoint>"/> for a chart
        /// </summary>
        /// <param name="filter">The filter for the chart</param>
        /// <returns>List of <see cref="ReportData"/></returns>
        ReportData GetReportData(ChartDataFilter filter);
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

        /// <inheritdoc />
        public IList<ReportData> GetChartData(ChartDataFilter filter)
        {
            var query = NewQueryAndFilter(filter);

            ReportData point = null;

            /// Select the points
            query.Select(
                Projections.ProjectionList()
                    .Add(
                        Projections.GroupProperty(
                            Projections.SqlFunction(
                                "date", 
                                NHibernateUtil.DateTime, 
                                Projections.Property<Task>(x => x.Scheduled)))
                        .WithAlias(() => point.Date))
                    .Add(
                        Projections.Count<Task>(x => x.Id)
                            .WithAlias(() => point.Total))
                    .Add(
                        Projections.Sum(
                            Projections.Conditional(
                                Restrictions.Eq(Projections.Property<Task>(x => x.Delayed), true), 
                                Projections.Constant(1), 
                                Projections.Constant(0)))
                            .WithAlias(() => point.NotOnTime))
                    .Add(
                        Projections.Sum(
                            Projections.Conditional(
                                Restrictions.Eq(Projections.Property<Task>(x => x.Delayed), false), 
                                Projections.Constant(1), 
                                Projections.Constant(0)))
                            .WithAlias(() => point.OnTime)));

            /// Ordering and transforming
            query.OrderByAlias(() => point.Date).Asc
                .TransformUsing(NHibernate.Transform.Transformers.AliasToBean<ReportData>());

            return query.List<ReportData>();
        }

        /// <inheritdoc />
        public ReportData GetReportData(ChartDataFilter filter)
        {
            var query = NewQueryAndFilter(filter);

            ReportData point = null;

            /// Select the points
            query.Select(
                Projections.ProjectionList()
                    .Add(
                        Projections.Count<Task>(x => x.Id)
                            .WithAlias(() => point.Total))
                    .Add(
                        Projections.Sum(
                            Projections.Conditional(
                                Restrictions.Eq(Projections.Property<Task>(x => x.Delayed), true),
                                Projections.Constant(1),
                                Projections.Constant(0)))
                            .WithAlias(() => point.NotOnTime))
                    .Add(
                        Projections.Sum(
                            Projections.Conditional(
                                Restrictions.Eq(Projections.Property<Task>(x => x.Delayed), false),
                                Projections.Constant(1),
                                Projections.Constant(0)))
                            .WithAlias(() => point.OnTime)));

            /// Transforming
            query.TransformUsing(NHibernate.Transform.Transformers.AliasToBean<ReportData>());

            return query.SingleOrDefault<ReportData>();
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

        private IQueryOver<Task, Task> NewQueryAndFilter(ChartDataFilter filter)
        {
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                .And(x => x.IsActive == true)
                .And(x => x.Scheduled >= filter.StartDate.Date)
                .And(x => x.Scheduled <= filter.EndDate.LastInstantOfDay());

            //// Optional filters
            if (!filter.Organisation.GetValueOrDefault().IsEmpty())
            {
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.Like(Projections.Property<Taxon>(x => x.Path), filter.Organisation.GetValueOrDefault().ToString(), MatchMode.Anywhere));
            }
            if (!filter.Patient.GetValueOrDefault().IsEmpty())
            {
                query.JoinQueryOver<Patient>(x => x.Patient)
                    .Where(x => x.Id == filter.Patient.GetValueOrDefault());
            }
            if (!filter.Account.GetValueOrDefault().IsEmpty())
            {
                query.JoinQueryOver<Account>(x => x.CompletedBy)
                    .Where(x => x.Id == filter.Account.GetValueOrDefault());
            }
            if (!filter.ScheduleSetting.GetValueOrDefault().IsEmpty())
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .Where(x => x.Id == filter.ScheduleSetting.GetValueOrDefault());
            }

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