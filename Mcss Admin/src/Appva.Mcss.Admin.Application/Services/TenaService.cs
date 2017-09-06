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
    using System.Threading.Tasks;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Sca;
    using Sca.Models;

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

        /// <summary>
        /// Get data from the TENA API.
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="GetResidentModel"/>.</returns>
        Task<GetResidentModel> GetResidentAsync(string externalId);

        /// <summary>
        /// Post Period to the TENA API.
        /// </summary>
        /// <param name="periodId">The Observation Period ID.</param>
        /// <returns>Returns a list of <see cref="GetManualEventModel"/>.</returns>
        Task<List<GetManualEventModel>> PostManualEventAsync(Guid periodId);
    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="GetEndpoint"/>.
        /// </summary>
        private const string GetEndpoint = "https://tenaidentifistage.sca.com/api/resident/";

        /// <summary>
        /// The <see cref="token"/>.
        /// </summary>
        private static string token = string.Empty;

        /// <summary>
        /// The <see cref="ITenaRepository"/>.
        /// </summary>
        private readonly ITenaRepository repository;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IApiService"/>.
        /// </summary>
        private ApiService apiService; // readonly

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="ITenaRepository"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public TenaService(ITenaRepository repository, ISettingsService settingsService)
        {
            this.repository = repository;
            this.settingsService = settingsService;
            this.apiService = new ApiService(new Uri(this.GetSettings().BaseAddress), this.GetSettings().ClientId, this.GetSettings().ClientSecret);
        }

        #endregion

        #region ITenaService members.

        /// <inheritdoc />
        public string GetRequestUri()
        {
            return this.settingsService.Find(ApplicationSettings.TenaSettings).BaseAddress;
        }

        /// <inheritdoc />
        public bool HasUniqueExternalId(string externalId)
        {
            return this.repository.HasUniqueExternalId(externalId);
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

        #endregion

        #region API communication members.

        /// <inheritdoc />
        public async Task<GetResidentModel> GetResidentAsync(string externalId)
        {
            if (this.HasUniqueExternalId(externalId))
            {
                return await this.apiService.GetResidentAsync(externalId);
            }

            return new GetResidentModel
            {
                Message = "Användaren är redan registrerad."
            };
        }

        /// <inheritdoc />
        public async Task<List<GetManualEventModel>> PostManualEventAsync(Guid periodId)
        {
            var measurements = this.repository.GetTenaPeriod(periodId).TenaObservationItems;
            if (measurements.IsEmpty())
            {
                return null;
            }
            return await this.apiService.PostManualEventAsync(this.ConvertDataToTenaModel(measurements));
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private List<PostManualEventModel> ConvertDataToTenaModel(IList<TenaObservationItem> tenaObservationItemsList)
        {
            var tenaApiList = new List<PostManualEventModel>();
            foreach (var item in tenaObservationItemsList)
            {
                tenaApiList.Add(new PostManualEventModel
                {
                    Id = item.Id.ToString(),
                    EventType = item.Measurement,
                    ResidentId = item.TenaObservationPeriod.Patient.TenaId,
                    Timestamp = item.UpdatedAt.ToString(),
                    Active = item.IsActive
                });
            }
            return tenaApiList;
        }

        /// <inheritdoc />
        private Domain.VO.TenaConfiguration GetSettings()
        {
            return this.settingsService.Find(ApplicationSettings.TenaSettings);
        }

        #endregion
    }
}
