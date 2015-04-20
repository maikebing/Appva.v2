using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Appva.Core.Extensions;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.Controllers {
    public class DelegationReportFilter : IReportFilter<Task, Task> {
        public Guid? AccountId { get; set; }
        public Guid? TaxonId { get; set; }
        public Guid? ScheduleSettingsId { get; set; }
        public void Filter(IQueryOver<Task, Task> query) {
            Sequence sequenceAlias = null;
            Schedule scheduleAlias = null;
            if (AccountId.HasValue) {
                query.Where(x => x.CompletedBy.Id == AccountId.Value);
            }
            if (ScheduleSettingsId.HasValue) {
                query.Inner.JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .Where(() => scheduleAlias.ScheduleSettings.Id == ScheduleSettingsId.Value);
            }
            if (TaxonId.HasValue) {
                query.Inner.JoinAlias(x => x.Sequence, () => sequenceAlias)
                    .Where(x => x.Taxon.Id == TaxonId.Value);
            }
        }
    }
}