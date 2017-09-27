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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
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
        void Save(Medication entity);
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
            IMedicationRepository medicationRepository)
        {
            this.ehmClient              = ehmClient;
            this.identity               = identity;
            this.patientService         = patientService;
            this.sequenceRepository     = sequenceRepository;
            this.medicationRepository   = medicationRepository;
        }

        #endregion

        #region IMedicationService members

        /// <inheritdoc />
        public async Task<IList<Medication>> List(Guid patientId)
        {
            var patient = this.patientService.Get(patientId);
            var account = this.identity.PrincipalId;
            //// TODO: Insert auth for eHM here
            var user = new User();

            var ordinations = await this.ehmClient.ListOrdinations(patient.PersonalIdentityNumber.ToString(), user);

            return MedicationTransformer.From(ordinations)
                .OrderByDescending(x => x.EndsAt.GetValueOrDefault(DateTime.MaxValue))
                .ThenByDescending(x => x.Article.Name).ToList();
        }

        /// <inheritdoc />
        public async Task<Medication> Find(long id, Guid patientId)
        {
            var patient = this.patientService.Get(patientId);
            var account = this.identity.PrincipalId;
            //// TODO: Insert auth for eHM here
            var user = new User();

            var ordinations = await this.ehmClient.ListOrdinations(patient.PersonalIdentityNumber.ToString(), user);
            var ordination = ordinations.FirstOrDefault(x => x.Id == id);

            return MedicationTransformer.From(ordination);
        }

        /// <inheritdoc />
        public Dictionary<long, SequenceMedicationCompareModel> GetSequenceInformationFor(IList<Medication> medications)
        {
            var extracted = medications.SelectMany(x => x.PreviousMedications).ToList();
            var all = medications.Concat(extracted).ToList();
            var ordinationIds = medications.SelectMany(x => x.PreviousMedications
                                            .Select(y => y.OrdinationId))
                                            .Concat(medications.Select(x => x.OrdinationId)).ToList();
            var sequences = sequenceRepository.List(ordinationsIds: ordinationIds);

            var retval = new Dictionary<long, SequenceMedicationCompareModel>();

            foreach(var m in medications)
            {
                var history = new List<Sequence>();
                foreach(var h in m.PreviousMedications)
                {
                    history.AddRange(sequences.Where(x => x.Medications.First(y => y.OrdinationId == m.OrdinationId) != null).ToList());
                }
                //var sequence = 
                var model = new SequenceMedicationCompareModel {
                    Sequence = sequences.FirstOrDefault(x => x.Medications.Any(y => y.OrdinationId == m.OrdinationId)),
                    History = history.Distinct().ToList()
                };
                retval.Add(m.OrdinationId,model);
            }
            return retval;
        }

        /// <inheritdoc />
        public void Save(Medication entity)
        {
            this.medicationRepository.Save(entity);
        }


        #endregion
    }
}