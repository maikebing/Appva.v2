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
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Entities.Medication;
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationService"/> class.
        /// </summary>
        public MedicationService(IEhmClient ehmClient, IIdentityService identity, IPatientService patientService)
        {
            this.ehmClient      = ehmClient;
            this.identity       = identity;
            this.patientService = patientService;
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

            return MedicationTransformer.From(ordinations);
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

        #endregion
    }
}