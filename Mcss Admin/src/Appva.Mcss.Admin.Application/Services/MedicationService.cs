// <copyright file="MedicationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Ehm;
    using Appva.Ehm.Models;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IMedicationService : IService
    {
        /// <summary>
        /// List the medications for the given patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<IList<Medication>> List(Guid patientId);

        /// <summary>
        /// Returns a medication by its is id and the patient
        /// </summary>
        /// <param name="id">The medication-id</param>
        /// <param name="patientId">The patient for the medication</param>
        /// <returns></returns>
        Task<Medication> Find(long id, Guid patientId);

        /// <summary>
        /// Gets the sequence information for medications.
        /// </summary>
        /// <param name="medications">The medications.</param>
        /// <returns></returns>
        Dictionary<long, SequenceMedicationCompareModel> GetSequenceInformationFor(IList<Medication> medications);

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Medication SaveOrUpdate(Medication entity);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class MedicationService : IMedicationService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IEhmService"/>
        /// </summary>
        private readonly IEhmClient ehmClient;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="IPatientService"/>
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ISequenceRepository"/>
        /// </summary>
        private readonly ISequenceRepository sequenceRepository;

        /// <summary>
        /// The <see cref="IMedicationRepository"/>
        /// </summary>
        private readonly IMedicationRepository medicationRepository;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationService"/> class.
        /// </summary>
        public MedicationService(
            IEhmClient ehmClient, 
            IIdentityService identity, 
            IPatientService patientService,
            ISequenceRepository sequenceRepository,
            IMedicationRepository medicationRepository,
            IAccountService accountService,
            ITaxonomyService taxonomyService,
            ISettingsService settings)
        {
            this.ehmClient              = ehmClient;
            this.identity               = identity;
            this.patientService         = patientService;
            this.sequenceRepository     = sequenceRepository;
            this.medicationRepository   = medicationRepository;
            this.accountService         = accountService;
            this.taxonomyService        = taxonomyService;
            this.settings               = settings;
        }

        #endregion

        #region IMedicationService members

        /// <inheritdoc />
        public async Task<IList<Medication>> List(Guid patientId)
        {
            var patient = this.patientService.Get(patientId);
            var account = this.identity.PrincipalId;

            var ordinations = await this.ehmClient.ListOrdinations(patient.PersonalIdentityNumber.ToString(), GetUser());

            return MedicationTransformer.From(ordinations)
                .OrderByDescending(x => x.EndsAt.GetValueOrDefault(DateTime.MaxValue))
                .ThenBy(x => x.Article.Name).ToList();
        }

        

        /// <inheritdoc />
        public async Task<Medication> Find(long id, Guid patientId)
        {
            var patient = this.patientService.Get(patientId);
            var account = this.identity.PrincipalId;

            var ordinations = await this.ehmClient.ListOrdinations(patient.PersonalIdentityNumber.ToString(), GetUser());
            var ordination = ordinations.FirstOrDefault(x => x.Id == id);

            return MedicationTransformer.From(ordination);
        }

        /// <inheritdoc />
        public Dictionary<long, SequenceMedicationCompareModel> GetSequenceInformationFor(IList<Medication> medications)
        {
            var extracted = medications.SelectMany(x => x.PreviousMedications).ToList();
            var all = medications.Concat(extracted).ToList();
            var ordinationIds = medications.Select(y => y.OrdinationId).ToList();
            foreach (var m in medications.Where(x => x.PreviousMedications.Count > 0))
            {
                ordinationIds.AddRange(m.PreviousMedications.Select(x => x.OrdinationId).ToList());
                ordinationIds.AddRange(m.PreviousMedications.Where(x => x.HistoricalOrdinationId != null).Select(x => x.HistoricalOrdinationId.GetValueOrDefault()).ToList());
            }
            var sequences = sequenceRepository.List(ordinationsIds: ordinationIds);

            var retval = new Dictionary<long, SequenceMedicationCompareModel>();

            foreach(var m in medications)
            {
                var history = new List<Sequence>();
                foreach(var h in m.PreviousMedications)
                {
                    var s = sequences.Where(x => x.Medications.Any(y => y.OrdinationId == h.OrdinationId || y.OrdinationId == h.HistoricalOrdinationId)).ToList();
                    if (s != null && s.Count > 0)
                    {
                        history.AddRange(s);
                    }
                }
                var model = new SequenceMedicationCompareModel {
                    Sequence = sequences.FirstOrDefault(x => x.Medications.Any(y => y.OrdinationId == m.OrdinationId)),
                    History = history.Distinct().ToList()
                };
                retval.Add(m.OrdinationId,model);
            }
            return retval;
        }

        /// <inheritdoc />
        public Medication SaveOrUpdate(Medication entity)
        {
            var med = this.medicationRepository.Find(entity.OrdinationId);
            if (med == null)
            {
                this.medicationRepository.Save(entity);
                return entity;
            }
            return this.UpdateMedication(med, entity);
        }

        #endregion

        #region Private members

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
        private User GetUser()
        {
            var account = this.accountService.Find(this.identity.PrincipalId);
            var ehmAttr = this.settings.Find<TenantAttributes>(ApplicationSettings.EhmTenantUserAttributes);
            return new User
            {
                PrescriberCode   = this.identity.Principal.PrescriberCode(),
                LegitimationCode = this.identity.Principal.LegitimationCode(),
                Adress           = ehmAttr.Adress,
                City             = ehmAttr.City,
                FirstName        = account.FirstName,
                LastName         = account.LastName,
                Phone            = ehmAttr.Phone,
                WorkplaceCode    = ehmAttr.WorkplaceCode,
                Zip              = ehmAttr.Zip,
                Workplace        = ehmAttr.Workplace,
                OrganizationId   = ehmAttr.OrganizationId,
                UserId           = account.Id
            };
        }

        private Medication UpdateMedication(Medication medication, Medication updates)
        {
            medication.Article                = updates.Article;
            medication.CanceledAt             = updates.CanceledAt;
            medication.CancellationComment    = updates.CancellationComment;
            medication.CancellationReason     = updates.CancellationReason;
            medication.CancellationReasonCode = updates.CancellationReasonCode;
            medication.DiscontinuedAt         = updates.DiscontinuedAt;
            medication.DiscontinuedComment    = updates.DiscontinuedComment;
            medication.DiscontinuedType       = updates.DiscontinuedType;
            medication.DosageScheme           = updates.DosageScheme;
            medication.DosageText1            = updates.DosageText1;
            medication.DosageText2            = updates.DosageText2;
            medication.LastExpiditedAmount    = updates.LastExpiditedAmount;
            medication.LastExpiditedAt        = updates.LastExpiditedAt;
            medication.LastExpiditedNplPackId = updates.LastExpiditedNplPackId;
            medication.NumbersOfExpiditions   = updates.NumbersOfExpiditions;
            medication.OrdinationCreatedAt    = updates.OrdinationCreatedAt;
            medication.OrdinationStartsAt     = updates.OrdinationStartsAt;
            medication.OrdinationValidUntil   = updates.OrdinationValidUntil;
            medication.Prescriber             = updates.Prescriber;
            medication.Purpose                = updates.Purpose;
            medication.RemainingExpiditions   = updates.RemainingExpiditions;
            medication.Status                 = updates.Status;
            medication.TreatmentEndsAt        = updates.TreatmentEndsAt;
            medication.TreatmentStartsAt      = updates.TreatmentStartsAt;
            medication.Type                   = updates.Type;

            this.medicationRepository.Update(medication);

            return medication;
        }

        #endregion
    }
}