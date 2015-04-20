using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Transform;
using NHibernate.Criterion;
using Appva.Core.Extensions;
using Appva.Mcss.Infrastructure;
using Appva.Mcss.Infrastructure.Mvc;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Business;
using Appva.Cqrs;

namespace Appva.Mcss.Web.Controllers
{

    public class CreateChartCommand<T> : IRequest<List<object[]>> where T : IReportFilter<Task, Task>
    {

        [AutoWired]
        public TaxonomyService TaxonomyService { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public T Filter { get; set; }

        public override void Execute()
        {
            Result = GenerateChartData(QueryAndFilter());
        }

        private IList<Task> QueryAndFilter()
        {
            var query = Session.QueryOver<Task>()
                .Where(x => x.Active == true)
                .And(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= StartDate)
                .And(x => x.Scheduled <= EndDate.Latest())
                .OrderBy(x => x.Scheduled)
                .Asc;
            Filter.Filter(query);
            return query.List();
        }

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

    }

    internal class ChartData
    {
        public double OnTime { get; set; }
        public double NotOnTime { get; set; }
        public ChartData() { }
        public ChartData(double onTime, double notOnTime)
        {
            OnTime = onTime;
            NotOnTime = notOnTime;
        }
    }

}