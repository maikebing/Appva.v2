// <copyright file="TimelineController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Application;
    using Application.Authorization;
    using Core.Extensions;
    using Domain.Constants;
    using Domain.Repositories;
    using Domain.Services;
    using Transformers;

    #endregion

    /// <summary>
    /// Timeline endpoint.
    /// </summary>
    [RoutePrefix("v1/timeline")]
    public class TimelineController : ApiController
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="ITimelineService"/>.
        /// </summary>
        private readonly ITimelineService timelineService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IPatientRepository"/>
        /// </summary>
        private readonly IPatientRepository patientRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineController"/> class.
        /// </summary>
        /// <param name="timelineService">The <see cref="ITimelineService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="patientRepository">The <see cref="IPatientRepository"/></param>
        public TimelineController(ITimelineService timelineService, ISettingsService settingsService, IPatientRepository patientRepository)
        {
            this.timelineService = timelineService;
            this.settingsService = settingsService;
            this.patientRepository = patientRepository;
        }
        
        #endregion

        #region Routes.

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="date">TODO: date</param>
        /// <param name="id">TODO: id</param>
        /// <param name="groupingStrategy">TODO: groupingStrategy</param>
        /// <param name="statusIds">TODO: statusIds</param>
        /// <param name="typeIds">TODO: typeIds</param>
        /// <returns>TODO: returns</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{date:datetime}/patient/{id:guid}")]
        public IHttpActionResult ListByPatient([FromUri(Name = "date")] DateTime date, [FromUri(Name = "id")] Guid id, string groupingStrategy = "patient", [FromUri(Name = "status_ids")] List<string> statusIds = null, [FromUri(Name = "type_ids")] List<string> typeIds = null)
        {
            //// TODO: use isEmpty
            if (typeIds.IsNull() || typeIds.Count.Equals(0))
            {
                typeIds = new List<string> { "ordination", "calendar" };
            }
            var tasks = this.timelineService.FindByPatient(id, date, date.LastInstantOfDay(), typeIds);
            //// TODO: Patient varible is not used.
            var patient = this.patientRepository.Get(id);
            var patientsWithDelays = new List<Guid>();
            if (this.patientRepository.PatientHasAlarm(id))
            {
                patientsWithDelays.Add(id);
            }
            return this.Ok(TimelineTransformer.FixMe(date, groupingStrategy, tasks, this.settingsService.Get<int>(Settings.AllowedHistorySize, 7), patientsWithDelays));
        }

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="date">TODO: date</param>
        /// <param name="id">TODO: id</param>
        /// <param name="groupingStrategy">TODO: groupingStrategy</param>
        /// <param name="statusIds">TODO: statusIds</param>
        /// <param name="typeIds">TODO: typeIds</param>
        /// <returns>TODO: returns</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{date:datetime}/taxon/{id:guid}")]
        public IHttpActionResult ListByTaxon([FromUri(Name = "date")] DateTime date, [FromUri(Name = "id")] Guid id, string groupingStrategy = "taxon", [FromUri(Name = "status_ids")] List<string> statusIds = null, [FromUri(Name = "type_ids")] List<string> typeIds = null)
        {
            //// TODO: use isEmpty
            if (typeIds.IsNull() || typeIds.Count.Equals(0)) 
            {
                typeIds = new List<string>(this.settingsService.Get<string>(Settings.OverviewTimelineTaskTypes, "ordination").Split(';'));
            }
            var tasks = this.timelineService.FindByTaxon(id, date, date.LastInstantOfDay(), typeIds);
            var patients = this.patientRepository.Search(string.Empty, id, string.Empty);
            var patientsWithDelays = this.patientRepository.GetPatientsWithAlarm(patients);
            return this.Ok(TimelineTransformer.FixMe(date, groupingStrategy, tasks, this.settingsService.Get<int>(Settings.AllowedHistorySize, 7), patientsWithDelays));
        }
        
        #endregion
    }
}