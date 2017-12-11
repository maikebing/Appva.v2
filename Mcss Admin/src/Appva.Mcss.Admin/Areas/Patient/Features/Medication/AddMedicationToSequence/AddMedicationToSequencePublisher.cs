// <copyright file="AddMedicationToSequencePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMedicationToSequencePublisher : AsyncRequestHandler<AddMedicationToSequence, DetailsMedicationRequest>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IMedicationService"/>.
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService audit;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMedicationToSequencePublisher"/> class.
        /// </summary>
        /// <param name="sequenceService">The sequence service.</param>
        /// <param name="medicationService">The medication service.</param>
        public AddMedicationToSequencePublisher(
            ISequenceService sequenceService,
            IMedicationService medicationService,
            IAuditService audit)
        {
            this.sequenceService    = sequenceService;
            this.medicationService  = medicationService;
            this.audit              = audit;
        }

        #endregion

        #region NotificationHandler overrides.

        /// <inheritdoc />
        public override async Task<DetailsMedicationRequest> Handle(AddMedicationToSequence message)
        {
            var medication = await this.medicationService.Find(message.OrdinationId, message.Id);
            medication = this.medicationService.SaveOrUpdate(medication);

            var sequence = this.sequenceService.Find(message.SequenceId);
            sequence.Medications.Add(medication);
            this.sequenceService.Update(sequence);

            this.audit.Update(sequence.Patient, "lade till medicinering {0} (ref. {1}) till insats '{2}' på signeringslista (ref. {3}) utan ändringar", medication.Article.Name, medication.Id, sequence.Name, sequence.Id);

            return new DetailsMedicationRequest
            {
                Id           = message.Id,
                OrdinationId = message.OrdinationId
            };
        }

        #endregion
    }
}