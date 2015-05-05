using System;
using System.Linq;
using System.Collections.Generic;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate;
using Appva.Cqrs;
using Appva.Persistence;
using Appva.Core.Utilities;

namespace Appva.Mcss.Web.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CreateReportCommand<T> : IRequest<ReportViewModel> where T : IReportFilter<Task, Task>
    {
        public DateTime? StartDate
        {
            get;
            set;
        }
        public DateTime? EndDate
        {
            get;
            set;
        }
        public int? Page
        {
            get;
            set;
        }
        public int? PageSize
        {
            get;
            set;
        }
        public T Filter
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CreateReportHandler<T> : RequestHandler<CreateReportCommand<T>, ReportViewModel>
         where T : IReportFilter<Task, Task>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateChartCommandHandler"/> class.
        /// </summary>
        public CreateReportHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<AccountQuickSearch, IEnumerable<object>> Members.

        /// <inheritdoc />
        public override ReportViewModel Handle(CreateReportCommand<T> message)
        {
            message.PageSize = message.PageSize ?? 30;
            message.StartDate = (message.StartDate.HasValue) ? message.StartDate.Value : DateTimeUtilities.Now().AddDays(-DateTime.Now.DaysInMonth());
            message.EndDate = (message.EndDate.HasValue) ? message.EndDate.Value.LastInstantOfDay() : DateTime.Now.LastInstantOfDay();
            var span = message.EndDate.Value.Subtract(message.StartDate.Value).Days;
            var tasks = QueryAndFilter(message);
            var tasksWithinStartDateAndEndDate = tasks.Where(x => x.Scheduled >= message.StartDate.Value && x.Scheduled <= message.EndDate.Value)
                .OrderBy(x => x.Scheduled).Desc;
            var dateSpan = this.GenerateReportSegment(tasksWithinStartDateAndEndDate.ToRowCountQuery());
            var total = tasksWithinStartDateAndEndDate.ToRowCountQuery().RowCount();
            var comparableDateSpan = this.GenerateReportSegment(tasks.ToRowCountQuery()
                .Where(x => x.Scheduled >= message.StartDate.Value.AddDays(-span) && x.Scheduled <= message.EndDate.Value.AddDays(-span)));
            var items = tasksWithinStartDateAndEndDate
                .Skip(((message.Page.Value - 1) * message.PageSize.Value)).Take(message.PageSize.Value).List().Where(task => task != null).ToList();
            return new ReportViewModel
            {
                StartDate = message.StartDate.Value,
                EndDate = message.EndDate.Value,
                TasksOnTime = dateSpan.TasksOnTime,
                TasksNotOnTime = dateSpan.TasksNotOnTime,
                ComparedDateSpanTasksOnTime = comparableDateSpan.TasksOnTime,
                ComparedDateSpanTasksNotOnTime = comparableDateSpan.TasksNotOnTime,
                AverageDifferenceInTime = dateSpan.AverageDifferenceInTime,
                ComparedAverageDifferenceInTime = comparableDateSpan.AverageDifferenceInTime,
                Search = new SearchViewModel<Task>
                {
                    Items = items,
                    PageSize = message.PageSize.Value,
                    PageNumber = message.Page.Value,
                    TotalItemCount = total
                }
            };
        }

        #endregion

        private IQueryOver<Task, Task> QueryAndFilter(CreateReportCommand<T> message)
        {
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= message.StartDate.Value.AddDays(-message.EndDate.Value.Subtract(message.StartDate.Value).Days))
                .And(x => x.Scheduled <= message.EndDate.Value)
                .Fetch(x => x.Patient).Eager
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            message.Filter.Filter(query);
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
            minutes = tasks.ToRowCountQuery().Where(task => task.Delayed && task.IsCompleted).Select(Projections.Sum(Projections.SqlProjection("datediff(n, Scheduled , this_.CompletedDate) as Minutes", new[] { "Minutes" }, new[] { NHibernateUtil.Double }))).SingleOrDefault<double>();
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
    }
}