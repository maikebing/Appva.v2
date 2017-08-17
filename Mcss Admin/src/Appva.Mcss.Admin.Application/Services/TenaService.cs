// <copyright file="TenaService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// The <see cref="ITenaService"/>.
    /// </summary>
    public interface ITenaService : IService
    {
        /// <summary>
        /// Get the request URI.
        /// </summary>
        /// <returns>The request URI.</returns>
        string GetRequestUri();

        /// <summary>
        /// If the provided external id is unique.
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasUniqueExternalId(string externalId);

        /// <summary>
        /// Get data from the TENA API.
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="KeyValuePair{HttpResponseMessage, string}"/>.</returns>
        KeyValuePair<HttpResponseMessage, string> GetDataFromTena(string externalId);

        /// <summary>
        /// Post data to the TENA API.
        /// </summary>
        /// <param name="patientId">The Patient ID.</param>
        /// <param name="periodId">The Observation Period ID.</param>
        /// <returns>Returns a <see cref="KeyValuePair{HttpResponseMessage, string}"/>.</returns>
        KeyValuePair<HttpResponseMessage, string> PostDataToTena(Guid patientId, Guid periodId);

        /// <summary>
        /// Creates a new ObserverPeriod.
        /// </summary>
        /// <param name="patient">The patient in this context.</param>
        /// <param name="startdate">Starting date for the period</param>
        /// <param name="enddate">Ending date for the period</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool CreateTenaObservervationPeriod(Patient patient, DateTime startdate, DateTime enddate);

        /// <summary>
        /// Validate the Starting date against previous periods
        /// </summary>
        /// <param name="patientId">The patientId in this context.</param>
        /// <param name="startdate">Starting date for the period</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasConflictingDate(Guid patientId, DateTime startdate);

        /// <summary>
        /// Get a specific Period from Database
        /// </summary>
        /// <param name="periodId">Period ID</param>
        /// <returns>Returns a <see cref="TenaObservationPeriod"/>observation period</returns>
        TenaObservationPeriod GetTenaObservationPeriod(Guid periodId);
    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenaRepository"/>.
        /// </summary>
        private readonly ITenaRepository repository;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="ITenaRepository"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public TenaService(ITenaRepository repository, ISettingsService settingsService) // , IPersistenceContext context
        {
            this.repository = repository;
            this.settingsService = settingsService;
        }

        #endregion

        #region ITenaService members.

        /// <inheritdoc />
        public string GetRequestUri()
        {
            return this.settingsService.Find(ApplicationSettings.TenaSettings).RequestUri;
        }

        /// <inheritdoc />
        public bool HasUniqueExternalId(string externalId)
        {
            return this.repository.HasUniqueExternalId(externalId);
        }

        /// <inheritdoc />
        public KeyValuePair<HttpResponseMessage, string> GetDataFromTena(string externalId)
        {
            HttpResponseMessage response = null;
            string content = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", this.GetCredentials());
                response = System.Threading.Tasks.Task.Run(() => client.GetAsync(this.GetRequestUri() + externalId)).Result;
                content = response.StatusCode == HttpStatusCode.OK ? response.Content.ReadAsStringAsync().Result : string.Empty;
            }

            return new KeyValuePair<HttpResponseMessage, string>(response, content);
        }

        /// <inheritdoc />
        public bool CreateTenaObservervationPeriod(Patient patient, DateTime startdate, DateTime enddate)
        {
            if (this.repository.HasConflictingDate(patient.Id, startdate))
            {
                return false;
            }

            this.repository.CreateNewTenaObserverPeriod(patient, startdate, enddate);

            return true;            
        }

        /// <inheritdoc />
        public TenaObservationPeriod GetTenaObservationPeriod(Guid periodId)
        {
            return this.repository.GetTenaPeriod(periodId);
        }
        
        /// <inheritdoc />
        public bool HasConflictingDate(Guid patientId, DateTime startdate)
        {
            return this.repository.HasConflictingDate(patientId, startdate);
        }
        
        /// <inheritdoc />
        public KeyValuePair<HttpResponseMessage, string> PostDataToTena(Guid patientId, Guid periodId)
        {
            var measurements = this.repository.GetTenaPeriod(periodId).TenaObservationItems;
            var tenaId = this.repository.GetTenaId(patientId);
            HttpResponseMessage response = null;
            string content = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://tenaidentifistage.sca.com/api/resident/");
                


            }

            // TODO: Insert logic here, work in progress
            // Make a POST call to TenaAPI with a Observation Period and its measurements as JSON


            return new KeyValuePair<HttpResponseMessage, string>(response, content);
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private string GetCredentials()
        {
            var settings = this.settingsService.Find(ApplicationSettings.TenaSettings);
            var credentials = System.Text.Encoding.UTF8.GetBytes(settings.ClientId + ":" + settings.ClientSecret);
            return Convert.ToBase64String(credentials);
        }
        
        #endregion
    }
}
