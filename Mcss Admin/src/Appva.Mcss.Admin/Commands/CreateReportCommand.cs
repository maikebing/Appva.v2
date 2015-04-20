using System;
using System.Linq;
using System.Collections.Generic;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using NHibernate.Criterion;
using Appva.Mcss.Infrastructure;
using NHibernate.Transform;
using NHibernate;

namespace Appva.Mcss.Web.Controllers {

    public class CreateReportCommand<T> : Command<ReportViewModel> where T : IReportFilter<Task, Task> {
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public T Filter { get; set; }

        public override void Execute() {
            Page = Page ?? 1;
            PageSize = PageSize ?? 30;
            StartDate = (StartDate.HasValue) ? StartDate.Value : DateTimeExt.Now().AddDays(-DateTimeExt.Now().DaysInMonth());
            EndDate = (EndDate.HasValue) ? EndDate.Value.Latest() : DateTimeExt.Now().Latest();
            var span = EndDate.Value.Subtract(StartDate.Value).Days;
            var tasks = QueryAndFilter();
            var tasksWithinStartDateAndEndDate = tasks.Where(x => x.Scheduled >= StartDate.Value && x.Scheduled <= EndDate.Value).OrderBy(x => x.Scheduled).Desc;
            var dateSpan = GenerateReportSegment(tasksWithinStartDateAndEndDate.ToRowCountQuery());
            var total = tasksWithinStartDateAndEndDate.ToRowCountQuery().RowCount();
            var comparableDateSpan = GenerateReportSegment(tasks.ToRowCountQuery().Where(x => x.Scheduled >= StartDate.Value.AddDays(-span) && x.Scheduled <= EndDate.Value.AddDays(-span)));
            var items = tasksWithinStartDateAndEndDate.Skip(((Page.Value - 1) * PageSize.Value)).Take(PageSize.Value).List().Where(task => task != null).ToList();
            Result = new ReportViewModel() {
                StartDate = StartDate.Value,
                EndDate = EndDate.Value,
                TasksOnTime = dateSpan.TasksOnTime,
                TasksNotOnTime = dateSpan.TasksNotOnTime,
                ComparedDateSpanTasksOnTime = comparableDateSpan.TasksOnTime,
                ComparedDateSpanTasksNotOnTime = comparableDateSpan.TasksNotOnTime,
                AverageDifferenceInTime = dateSpan.AverageDifferenceInTime,
                ComparedAverageDifferenceInTime = comparableDateSpan.AverageDifferenceInTime,
                Search = new SearchViewModel<Task>() {
                    Items = items,
                    PageSize = PageSize.Value,
                    PageNumber = Page.Value,
                    TotalItemCount = total
                }
            };
        }

        private IQueryOver<Task, Task> QueryAndFilter() {
            var query = Session.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= StartDate.Value.AddDays(-EndDate.Value.Subtract(StartDate.Value).Days))
                .And(x => x.Scheduled <= EndDate.Value)
                .Fetch(x => x.Patient).Eager
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            Filter.Filter(query);
            return query;
        }

        private ReportSegment GenerateReportSegment(IQueryOver<Task, Task> tasks) {
            var percent = 100.00;
            var tasksOnTime = 0.0;
            var tasksNotOnTime = 0.0;
            var minutes = 0.0;
            tasksOnTime = tasks.ToRowCountQuery().Where(task => !task.Delayed).RowCount();
            tasksNotOnTime = tasks.ToRowCountQuery().Where(task => task.Delayed).RowCount();
            var signedTasksNotOnTime = tasks.ToRowCountQuery().Where(task => task.Delayed && task.IsCompleted).RowCount();
            var count = tasksOnTime + tasksNotOnTime;
            minutes = tasks.ToRowCountQuery().Where(task => task.Delayed && task.IsCompleted).Select(Projections.Sum(Projections.SqlProjection("datediff(n, Scheduled , this_.CompletedDate) as Minutes", new[] { "Minutes" }, new[] { NHibernateUtil.Double }))).SingleOrDefault<double>();
            //minutes = minutes - tasks.ToRowCountQuery().Where(task => task.Delayed && task.IsCompleted).Select(Projections.Sum(Projections.SqlProjection("RangeInMinutesAfter as Minutes", new[] { "Minutes" }, new[] { NHibernateUtil.Int32 }))).SingleOrDefault<int>();

            return new ReportSegment()
            {
                TasksOnTime = count > 0 ? Math.Round((tasksOnTime / count) * percent) : 0,
                TasksNotOnTime = count > 0 ? Math.Round((tasksNotOnTime / count) * percent) : 0,
                AverageDifferenceInTime = count > 0 ? Math.Round(minutes / count) : 0
            };
        }

        private class ReportSegment {
            public double TasksOnTime { get; set; }
            public double TasksNotOnTime { get; set; }
            public double AverageDifferenceInTime { get; set; }
        }

    }

}