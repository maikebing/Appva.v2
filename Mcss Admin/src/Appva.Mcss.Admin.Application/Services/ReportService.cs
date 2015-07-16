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
    using Appva.NHibernateUtils.Projections;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IReportService : IService
    {
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
        public IList<ReportData> GetChartData(ChartDataFilter filter)
        {
            var query = QueryAndFilter(filter);

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
            var currentSpanData = this.GetReportDataSegment(filter);

            var newEnd = filter.StartDate.AddDays(-1);
            var newStart = filter.StartDate.AddDays((filter.StartDate - filter.EndDate).Days);
            filter.EndDate = newEnd;
            filter.StartDate = newStart;

            var previousSpanData = this.GetReportDataSegment(filter);

            return new ReportData
            {
                Date = currentSpanData.Date,
                NotOnTime = currentSpanData.NotOnTime,
                OnTime = currentSpanData.OnTime,
                Total = currentSpanData.Total,
                Signed = currentSpanData.Signed,
                AverageMinutesDelayed = currentSpanData.AverageMinutesDelayed,
                PreviousPeriod = new ReportData
                {
                    OnTime = previousSpanData.OnTime,
                    NotOnTime = previousSpanData.NotOnTime,
                    Total = previousSpanData.Total,
                    Signed = previousSpanData.Signed,
                    AverageMinutesDelayed = previousSpanData.AverageMinutesDelayed
                }
            };
        }

        #endregion

        #region Private Methods.

        private ReportDataSegment GetReportDataSegment(ChartDataFilter filter)
        {
            var query = QueryAndFilter(filter);

            ReportDataSegment point = null;

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
                    .Add(Projections.Sum(
                            Projections.Conditional(
                                Restrictions.Eq(Projections.Property<Task>(x => x.Delayed), false),
                                Projections.Constant(1),
                                Projections.Constant(0)))
                            .WithAlias(() => point.OnTime))
                    .Add(Projections.Sum(
                        Projections.Conditional(
                            Restrictions.Conjunction()
                                .Add(Restrictions.Eq(Projections.Property<Task>(x => x.Delayed), true))
                                .Add(Restrictions.Eq(Projections.Property<Task>(x => x.IsCompleted), true)),
                            ArithmeticProjections.Sub(
                                DateProjections.DateDiff<Task>("mi", x => x.Scheduled, x => x.CompletedDate),
                                Projections.Property<Task>(x => x.RangeInMinutesAfter)),
                            Projections.Constant(0))).WithAlias(() => point.AverageMinutesDelayed))
                    .Add(Projections.Sum(
                        Projections.Conditional(
                            Restrictions.Eq(Projections.Property<Task>(x => x.IsCompleted), true),
                            Projections.Constant(1),
                            Projections.Constant(0)))
                        .WithAlias(() => point.Signed)));

            /// Transforming
            query.TransformUsing(NHibernate.Transform.Transformers.AliasToBean<ReportDataSegment>());

            return query.SingleOrDefault<ReportDataSegment>();
        }

        private IQueryOver<Task, Task> QueryAndFilter(ChartDataFilter filter)
        {
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                .And(x => x.IsActive == true)
                .And(x => x.Scheduled >= filter.StartDate.Date)
                .And(x => x.Scheduled <= filter.EndDate.LastInstantOfDay());

            //// Optional filters
            if (!filter.Organisation.GetValueOrDefault().IsEmpty() && filter.Patient.GetValueOrDefault().IsEmpty())
            {
                query.JoinQueryOver<Patient>(x => x.Patient)
                    .JoinQueryOver<Taxon>(x => x.Taxon)
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
            else if (!filter.IncludeCalendarTasks)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .Where(x => x.ScheduleType == ScheduleType.Action);
            }

            return query;
        }
        
        #endregion
    }
}