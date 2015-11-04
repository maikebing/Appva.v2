using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using Appva.Core.Extensions;
using NHibernate.Transform;
using Appva.Cqrs;
using Appva.Persistence;

namespace Appva.Mcss.Web.Controllers {

    public class SearchTaskCommand : IRequest<SearchViewModel<Task>>
    {
        public Guid PatientId { get; set; }
        public Guid ScheduleSettingsId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool FilterByAnomalies { get; set; }
        public bool FilterByNeedsBasis { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public OrderTasksBy Order { get; set; }
    }

    public sealed class SearchTaskHandler : RequestHandler<SearchTaskCommand, SearchViewModel<Task>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPatientHandler"/> class.
        /// </summary>
        public SearchTaskHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<PatientQuickSearch, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override SearchViewModel<Task> Handle(SearchTaskCommand message)
        {
            var scheduleSetting = this.persistence.Get<ScheduleSettings>(message.ScheduleSettingsId);
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.Patient.Id == message.PatientId)
                //.And(x => x.Inventory.Increased == null) // why is this not working, uncommented because of error?
                //.And(x => x.Inventory.RecalculatedLevel == null) // why is this not working, uncommented because of error?
                .And(x => x.Scheduled <= message.EndDate)
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());

            switch (message.Order)
            {
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
                    query = query.OrderBy(x => x.CompletedDate).Desc;
                    break;
            };

            if (message.StartDate.HasValue) {
                query.Where(x => x.Scheduled >= message.StartDate);
            }

            if (message.FilterByNeedsBasis)
            {
                query.Where(x => x.OnNeedBasis == true);
            }

            if (message.FilterByAnomalies)
            {
                query.Where(s => s.Status > 1 && s.Status < 5 || s.Delayed == true);
            }

            if (scheduleSetting.ScheduleType == ScheduleType.Calendar) {
                query.Where(x => x.CanRaiseAlert);
            }

            query.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                .Where(x => x.Id == message.ScheduleSettingsId);
            var items = query.Skip((message.PageNumber - 1) * message.PageSize).Take(message.PageSize).Future().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();

            return new SearchViewModel<Task>
            {
                Items = items,
                PageNumber = message.PageNumber,
                PageSize = message.PageSize,
                TotalItemCount = totalCount.Value
            };
        }

        #endregion
    }
}