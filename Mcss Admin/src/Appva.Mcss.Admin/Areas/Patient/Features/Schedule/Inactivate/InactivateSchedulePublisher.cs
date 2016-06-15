// <copyright file="InactivateSchedulePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InactivateSchedulePublisher : RequestHandler<InactivateSchedule, ListSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateSchedulePublisher"/> class.
        /// </summary>
        public InactivateSchedulePublisher(IAuditService auditing, IPersistenceContext persistence)
        {
            this.auditing    = auditing;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListSchedule Handle(InactivateSchedule message)
        {
            var patient       = this.persistence.Get<Patient> (message.Id);
            var schedule      = this.persistence.Get<Schedule>(message.ScheduleId);
            schedule.IsActive = false;
            schedule.UpdatedAt = DateTime.Now;
            this.persistence.Update(schedule);
            this.auditing.Update(
                schedule.Patient,
                "inaktiverade lista {0} (REF: {1}).", 
                schedule.ScheduleSettings.Name, 
                schedule.Id);
            //// We also need to inactivate the sequences.
            var sequences = this.persistence.QueryOver<Sequence>().Where(x => x.Schedule.Id == schedule.Id).List();
            foreach (var sequence in sequences)
            {
                if (sequence.IsActive)
                {
                    sequence.IsActive  = false;
                    sequence.UpdatedAt = DateTime.Now;
                    this.auditing.Delete(sequence.Patient, "inaktiverade insats {0} för lista {1} (REF: {2}).", sequence.Name, schedule.ScheduleSettings.Name, sequence.Id);
                }
            }
            return new ListSchedule
            {
                Id = patient.Id
            };
        }

        #endregion
    }
}