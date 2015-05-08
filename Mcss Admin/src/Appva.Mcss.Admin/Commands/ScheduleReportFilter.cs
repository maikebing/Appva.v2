using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Appva.Core.Extensions;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Application.Models;

namespace Appva.Mcss.Web.Controllers
{
    public class ScheduleReportFilter : IReportingFilter
    {
        public Guid PatientId { get; set; }
        public Guid? ScheduleSettingsId { get; set; }


        #region IReportingFilter Members.

        /// <inheritdoc />
        public void Filter(IQueryOver<Task, Task> query)
        {
            query.Where(x => x.Patient.Id == PatientId);
            Schedule scheduleAlias = null;
            if (ScheduleSettingsId.HasValue)
            {
                query.Inner.JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .Where(() => scheduleAlias.ScheduleSettings.Id == ScheduleSettingsId);
            }
        }

        #endregion
    }

}