// <copyright file="DeletePreparationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.Controllers;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Mcss.Admin.Application.Auditing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeletePreparationPublisher : RequestHandler<DeletePreparation, SchemaPreparation>
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
        /// Initializes a new instance of the <see cref="DeletePreparationPublisher"/> class.
        /// </summary>
        public DeletePreparationPublisher(IAuditService auditing, IPersistenceContext persistence)
        {
            this.auditing       = auditing;
            this.persistence    = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override SchemaPreparation Handle(DeletePreparation message)
        {
            var patient        = this.persistence.Get<Patient>(message.Id);
            var sequence       = this.persistence.Get<PreparedSequence>(message.PreparedSequenceId);
            sequence.IsActive  = false;
            sequence.UpdatedAt = message.StartDate.Date.AddDays(-1);
            this.persistence.Update(sequence);
            var tasks = this.persistence.QueryOver<PreparedTask>()
                .Where(x => x.PreparedSequence.Id == sequence.Id)
                  .And(x => x.Date >= message.StartDate)
                .List();
            foreach (var task in tasks)
            {
                this.auditing.Delete(
                    "tog bort iordningsställande {0} [REF:{1}] {2:yyyy-MM-dd}{3}", 
                    task.PreparedSequence.Name, 
                    task.PreparedSequence.Id,
                    task.Date,
                    task.PreparedBy == null ? string.Empty : " iordningsställt av " + task.PreparedBy.FullName);
                this.persistence.Delete(task);
            }
            return new SchemaPreparation
            {
                Id         = patient.Id,
                ScheduleId = sequence.Schedule.Id,
                StartDate  = message.StartDate
            };
        }

        #endregion
    }
}