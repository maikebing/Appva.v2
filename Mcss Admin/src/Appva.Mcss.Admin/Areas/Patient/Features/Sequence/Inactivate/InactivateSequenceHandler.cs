// <copyright file="InactivateSequenceHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InactivateSequenceHandler : RequestHandler<InactivateSequence, DetailsSchedule>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateSequenceHandler"/> class.
        /// </summary>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        /// <param name="context"The <see cref="IPersistenceContext"/>></param>
        public InactivateSequenceHandler(IAuditService auditing, IPersistenceContext context)
        {
            this.auditing = auditing;
            this.context  = context;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(InactivateSequence message)
        {
            var sequence = this.context.Get<Sequence>(message.SequenceId);
            sequence.IsActive  = false;
            sequence.UpdatedAt = DateTime.Now;
            this.context.Update(sequence);
            this.auditing.Delete(sequence.Patient, "inaktiverade insats {0} (REF: {1}).", sequence.Name, sequence.Id);
            return new DetailsSchedule
            {
                Id         = message.Id,
                ScheduleId = sequence.Schedule.Id
            };
        }

        #endregion
    }
}