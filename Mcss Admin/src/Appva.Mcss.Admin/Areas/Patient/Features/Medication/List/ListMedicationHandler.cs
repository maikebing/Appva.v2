// <copyright file="ListMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Hip;
    using System.Threading.Tasks;
    using Appva.Hip.Exceptions;
    using System.Web;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Hip.Model;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListMedicationHandler : ContextRequestHandler<ListMedication,ListMedicationModel>
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
        /// Initializes a new instance of the <see cref="ListMedicationHandler"/> class.
        /// </summary>
        public ListMedicationHandler(IPersistenceContext persistence, IPatientTransformer transformer, IHipClient hipClient, HttpContextBase context)
            :base(context)
        {
            this.persistence = persistence;
            this.transformer = transformer;
            this.hipClient = hipClient;
        }

        #endregion

        #region RequestHandler overrides

        public override ListMedicationModel Handle(ListMedication message)
        {
            if(message.Page == 0)
            {
                message.Page = 1;
            }
            var patient = this.transformer.ToPatient(this.persistence.Get<Patient>(message.Id));
            var medications = this.GetDruglist(patient, message.Page, 10).Result;
            
            return new ListMedicationModel
            {
                Medications = medications.Content,
                Patient = patient,
                PageSize = 10,
                PageNumber = message.Page,
                TotalItemCount = medications.Status.FilterCount
            };
        }

        #endregion

        #region Private members

        private async Task<HipResponse<MedicationItem>> GetDruglist(PatientViewModel patient, int page = 1, int pageSize = 10)
        {
            try
            {
                return await this.hipClient.Medication.ListAsync("191212121212", page, pageSize).ConfigureAwait(false);
            }
            catch (MissingConsentException e)
            {
                this.Redirect("Create", "Consents", new { id = patient.Id, Refferer=this.CurrentUrl() });
                return null;
            }
        }

        #endregion
    }
}