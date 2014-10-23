// <copyright file="PatientController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Web.Http;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Appva.Mcss.ResourceServer.Domain.Services;
    using Models;
    using Transformers;

    #endregion

    /// <summary>
    /// Patient endpoint.
    /// </summary>
    [RoutePrefix("v1/patient")]
    public class PatientController : ApiController
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="IPatientRepository"/>.
        /// </summary>
        private readonly IPatientRepository patientRepository;

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService deviceService;
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="patientRepository">The <see cref="IPatientRepository"/></param>
        /// <param name="deviceService">The <see cref="IDeviceService"/></param>
        public PatientController(IPatientRepository patientRepository, IDeviceService deviceService)
        {
            this.patientRepository = patientRepository;
            this.deviceService = deviceService;
        }
        
        #endregion

        #region Routes.

        /// <summary>
        /// Returns a hydrated <code>Patient</code>.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>A hydrated <code>Patient</code></returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{id:guid}")]
        public IHttpActionResult GetById(Guid id)
        {
            var patient = this.patientRepository.Get(id);
            if (patient == null) 
            {
                return this.NotFound();
            }
            var hasDelays = this.patientRepository.PatientHasAlarm(id); //// patient.Tasks.First(x => x.Active && x.Delayed && !x.DelayHandled && !x.IsCompleted) != null;
            return this.Ok(PatientTransformer.ToPatient(patient, hasDelays));
        }

        /// <summary>
        /// Returns a collection of hydrated <code>Patient</code> by query. Matches both name and unique identifier
        /// </summary>
        /// <param name="taxonId">Optional taxon id</param>
        /// <param name="query">Optional query</param>
        /// <param name="status">Optional status</param>
        /// <returns>Collection of <see cref="PatientModel"/></returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("search")]
        public IHttpActionResult Search([FromUri(Name = "taxon_id")] Guid? taxonId = null, [FromUri] string query = null, [FromUri] string status = null)
        {
            var taxon = this.deviceService.GetFilterTaxonIdForDevice((Guid)this.User.Identity.Device(), taxonId.GetValueOrDefault());
            var patients = this.patientRepository.Search(query, taxon, status);
            var patientsWithDelayedTask = this.patientRepository.GetPatientsWithAlarm(patients);
            return this.Ok(PatientTransformer.ToPatient(patients, patientsWithDelayedTask));
        }

        /// <summary>
        /// Lists 200 patients per page
        /// </summary>
        /// <param name="taxonId">Optional taxon id</param>
        /// <param name="status">Optional status</param>
        /// <param name="count">Optional per page, defaults to 200</param>
        /// <param name="page">Optional page, defaults to 0</param>
        /// <returns>Collection of <code>Patient</code></returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("list")]
        public IHttpActionResult List([FromUri(Name = "taxon_id")] Guid? taxonId = null, [FromUri] string status = null, int count = 200, int page = 0)
        {
            var taxon = this.deviceService.GetFilterTaxonIdForDevice((Guid)this.User.Identity.Device(), taxonId.GetValueOrDefault());
            var patients = this.patientRepository.Search(string.Empty, taxon, status, count: count, page: page);
            var patientsWithDelayedTask = this.patientRepository.GetPatientsWithAlarm(patients);
            return this.Ok(PatientTransformer.ToPatient(patients, patientsWithDelayedTask));
        }

        #endregion
    }
}