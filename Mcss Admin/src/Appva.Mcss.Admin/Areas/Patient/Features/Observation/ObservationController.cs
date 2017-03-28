// <copyright file="ObservationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Domain;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/observations")]
    public sealed class ObservationController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IObservationRepository"/>.
        /// </summary>
        private readonly IObservationRepository observationRepository;

        /// <summary>
        /// The <see cref="IPatientRepository"/>.
        /// </summary>
        private readonly IPatientRepository patientRepository;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        /// <summary>
        /// The <see cref="ITaxonRepository"/>.
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        /// <summary>
        /// The <see cref="ISignatureRepository"/>.
        /// </summary>
        private readonly ISignatureRepository signatureRepository;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationController"/> class.
        /// </summary>
        public ObservationController(
            IObservationRepository observationRepository, 
            IPatientRepository patientRepository,
            IPatientTransformer patientTransformer,
            ITaxonRepository taxonRepository,
            ISignatureRepository signatureRepository,
            IAccountRepository accountRepository)
        {
            this.observationRepository = observationRepository;
            this.patientRepository     = patientRepository;
            this.patientTransformer    = patientTransformer;
            this.taxonRepository       = taxonRepository;
            this.signatureRepository   = signatureRepository;
            this.accountRepository     = accountRepository;
        }

        #endregion

        #region Routes.

        #region List.

        [Route, HttpGet]
        [PermissionsAttribute(Permissions.Inventory.ReadValue)]
        public ActionResult List(Guid id)
        {
            var patient      = this.patientRepository.Load(id);
            var observations = this.observationRepository.ListByPatient(id);
            return this.View(new ListObservations
            {
                Patient      = this.patientTransformer.ToPatient(patient),
                Observations = observations
            });
        }

        #endregion

        #region Create.

        [HttpGet, Route("new")]
        [Hydrate]
        [PermissionsAttribute(Permissions.Inventory.ReadValue)]
        public ActionResult Create(Guid id)
        {
            return this.View(new CreateObservation
            {
                Id = id
            });
        }

        [HttpPost, Route("new")]
        [Validate, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Inventory.ReadValue)]
        public ActionResult Create(CreateObservation model)
        {
            var patient  = this.patientRepository.Load(model.Id);
            var category = this.taxonRepository.List("ORG").First();
            this.observationRepository.Save(Observation.New(patient, model.Name, model.Description, category));
            
            var signator = this.accountRepository.List().First();
            this.signatureRepository.Save(Signature.New(category, signator, new List<SignedData>
                {
                    SignedData.New(Base64Binary.New<dynamic>(new { Id = signator.Id }))
                }));
            return this.RedirectToAction("List", new { Id = model.Id });
        }

        #endregion

        #endregion
    }
}