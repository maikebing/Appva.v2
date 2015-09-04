// <copyright file="DetailsMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Hip;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mvc;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
using Appva.Hip.Model;
using Appva.Mcss.Web.ViewModels;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DetailsMedicationHandler : ContextRequestHandler<DetailsMedication, DetailsMedicationModel>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IHipClient"/>
        /// </summary>
        private readonly IHipClient hipClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsMedicationHandler"/> class.
        /// </summary>
        public DetailsMedicationHandler(HttpContextBase context, IPersistenceContext persistence, IPatientTransformer transformer, IHipClient hipClient):
            base(context)
        {
            this.persistence = persistence;
            this.transformer = transformer;
            this.hipClient = hipClient;
        }

        #endregion

        #region Overrides

        public override DetailsMedicationModel Handle(DetailsMedication message)
        {
            var patient = this.transformer.ToPatient(this.persistence.Get<Patient>(message.Id));
            var medication = this.GetMedicationAsync(patient, message.MedicationId).Result;

            return new DetailsMedicationModel
            {
                Patient = patient,
                Medication = medication.Content,
                MedicationLastChanged = medication.TimeStamp
            };
        }

        #endregion

        #region Private members

        private async Task<ResponseItem<MedicationItem>> GetMedicationAsync(PatientViewModel patient, string id)
        {
            var response = await this.hipClient.Medication.GetAsync("191212121212", id).ConfigureAwait(false);

            return response;
        }

        #endregion
    }
}