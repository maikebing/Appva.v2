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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeletePreparationPublisher : RequestHandler<DeletePreparation, SchemaPreparation>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePreparationPublisher"/> class.
        /// </summary>
        public DeletePreparationPublisher(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override SchemaPreparation Handle(DeletePreparation message)
        {
            var patient = this.patientService.Get(message.Id);
            var sequence = this.persistence.Get<PreparedSequence>(message.PreparedSequenceId);
            sequence.IsActive = false;
            sequence.UpdatedAt = message.StartDate.Date.AddDays(-1);
            this.persistence.Update(sequence);
            var tasks = this.persistence.QueryOver<PreparedTask>()
                .Where(x => x.PreparedSequence.Id == sequence.Id)
                .And(x => x.Date >= message.StartDate)
                .List();
            foreach (var task in tasks)
            {
                this.persistence.Delete(task);
            }
            return new SchemaPreparation
            {
                Id = patient.Id,
                ScheduleId = sequence.Schedule.Id,
                StartDate = message.StartDate
            };
        }

        #endregion
    }
}