using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Appva.Core.Extensions;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.Controllers {

    public class FullReportFilter : IReportFilter<Task, Task> {
        public Guid? TaxonId { get; set; }
        public Guid? ScheduleSettingsId { get; set; }
        public ScheduleSettings ScheduleSettings { get; set; }
        public void Filter(IQueryOver<Task, Task> query) {
            Taxon taxonAlias = null;
            Schedule scheduleAlias = null;
            if (TaxonId.HasValue) {
                query.Inner.JoinAlias(x => x.Taxon, () => taxonAlias)
                    .WhereRestrictionOn(() => taxonAlias.Path)
                    .IsLike(TaxonId.Value.ToString(), MatchMode.Anywhere);
            }
            if (ScheduleSettingsId.HasValue) {
                query
                    .Inner.JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .Where(() => scheduleAlias.ScheduleSettings.Id == ScheduleSettingsId.Value);
                if (ScheduleSettings.ScheduleType == ScheduleType.Calendar) {
                    query.Where(x => x.CanRaiseAlert);
                }
            }
            else {
                query
                    .JoinQueryOver<Schedule>(x => x.Schedule)
                        .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                            .Where(x => x.ScheduleType == ScheduleType.Action);
            }
        }
    }

}