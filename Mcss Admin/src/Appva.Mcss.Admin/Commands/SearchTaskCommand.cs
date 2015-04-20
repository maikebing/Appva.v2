using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using Appva.Core.Extensions;
using Appva.Mcss.Web.Mappers;
using Appva.Mcss.Infrastructure;
using NHibernate.Transform;

namespace Appva.Mcss.Web.Controllers {

    public class SearchTaskCommand : Command<SearchViewModel<Task>> {

        public Guid PatientId { get; set; }
        public Guid ScheduleSettingsId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool FilterByAnomalies { get; set; }
        public bool FilterByNeedsBasis { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public OrderTasksBy Order { get; set; }
        
        public override void Execute() {

            var scheduleSetting = Session.Get<ScheduleSettings>(ScheduleSettingsId);
            var query = Session.QueryOver<Task>()
                .Where(x => x.Patient.Id == PatientId)
                .And(x => x.Inventory.Increased == null)
                .And(x => x.Inventory.RecalculatedLevel == null)
                .And(x => x.Modified <= EndDate)
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());

            switch (Order) {
                case OrderTasksBy.Day:
                    query = query.OrderBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Medecin:
                    query = query.OrderBy(x => x.Name).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Scheduled:
                    query = query.OrderBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.SignedBy:
                    Account cb = null;
                    query = query.Left.JoinAlias(x => x.CompletedBy, () => cb).OrderBy(() => cb.LastName).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Status:
                    Taxon st = null;
                    query = query.OrderBy(x => x.Status).Asc.ThenBy(x => x.Scheduled).Desc;
                    query = query.Left.JoinAlias(x => x.StatusTaxon, () => st).OrderBy(() => st.Weight).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Time:
                    query = query.OrderBy(x => x.Modified).Desc;
                    break;
            };

            if (StartDate.HasValue) {
                query.Where(x => x.Modified >= StartDate);
            }

            if (FilterByNeedsBasis) {
                query.Where(x => x.OnNeedBasis == true);
            }

            if (FilterByAnomalies) {
                query.Where(s => s.Status > 1 && s.Status < 5 || s.Delayed == true);
            }

            if (scheduleSetting.ScheduleType == ScheduleType.Calendar) {
                query.Where(x => x.CanRaiseAlert);
            }

            query.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                .Where(x => x.Id == ScheduleSettingsId);
            
            

            var items = query.Skip((PageNumber - 1) * PageSize).Take(PageSize).Future().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();

            Result = new SearchViewModel<Task>() {
                Items = items,
                PageNumber = PageNumber,
                PageSize = PageSize,
                TotalItemCount = totalCount.Value
            };

        }

    }

}