// <copyright file="TenaService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

using System.Threading.Tasks;
using Appva.Core.Extensions;
using Appva.Http;

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Appva.Apis.Http;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Sca;
    using Sca.Models;
    using Newtonsoft.Json;

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
        /// <returns>Returns a <see cref="KeyValuePair{HttpResponseMessage, string}"/>.</returns>
        GetResidentModel GetDataFromTena(string externalId);

        /// <summary>
        /// Post data to the TENA API.
        /// </summary>
        /// <param name="patientId">The Patient ID.</param>
        /// <param name="periodId">The Observation Period ID.</param>
        /// <returns>Returns a <see cref="KeyValuePair{HttpResponseMessage, string}"/>.</returns>
        List<GetManualEventModel> PostDataToTena(Guid periodId);

        Task<IHttpResponseMessage<GetResidentModel>> GetResidentAsync(string externalId);
        Task<IHttpResponseMessage<List<GetManualEventModel>>> PostManualEventAsync(Guid periodId);
    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="getEndpoint"/>.
        /// </summary>
        private const string getEndpoint = "https://tenaidentifistage.sca.com/api/resident/";

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
        /// <param name="apiService">The <see cref="IApiService"/>.</param>
        public TenaService(ITenaRepository repository, ISettingsService settingsService)
        {
            this.repository = repository;
            this.settingsService = settingsService;
            //this.apiService = apiService;
            this.apiService = new ApiService(new Uri(GetSettings().BaseAddress), GetSettings().ClientId, GetSettings().ClientSecret);
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



        #region API communcation
        // TODO: Clean up above methods.

        /// <inheritdoc />
        public GetResidentModel GetDataFromTena(string externalId)
        {
            return this.apiService.GetResident(externalId);

            //return apiService.GetResident(externalId);
        }

        /// <inheritdoc />
        public List<GetManualEventModel> PostDataToTena(Guid periodId)
        {
            // TODO: Exceptionhandling
            var measurements = this.repository.GetTenaPeriod(periodId).TenaObservationItems;

            if (measurements.IsNull())
            {
                return new List<GetManualEventModel>()
                {
                    new GetManualEventModel()
                    {
                        Id = "0000",
                        ImportResult = "EmptyList"
                    }
                };
            }
            return apiService.PostManualEvent(this.ConvertDataToTenaModel(measurements));
        }

        public async Task<IHttpResponseMessage<GetResidentModel>> GetResidentAsync(string externalId)
        {
            return await this.apiService.GetResidentAsync(externalId);
        }

        public async Task<IHttpResponseMessage<List<GetManualEventModel>>> PostManualEventAsync(Guid periodId)
        {
            var testDataLista = new List<PostManualEventModel>
            {
                new PostManualEventModel
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "toilet",
                    ResidentId = "8L2vJIUo",
                    Timestamp = DateTime.UtcNow.AddHours(5).ToString(),
                    Active = true
                },
                new PostManualEventModel
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "feces",
                    ResidentId = "8L2vJIUo",
                    Timestamp = DateTime.UtcNow.AddHours(5).ToString(),
                    Active = true
                }
            };

            var measurements = this.repository.GetTenaPeriod(periodId).TenaObservationItems;
            if (measurements.IsEmpty())
            {
                // Funkar detta? Detta skapar ett exception som bubblar? upp till ytan på något vis...
                return null;
            }
            // posta en lista med mötvärden och eventuellt hantera svaret. Returnera något pegagogiskt för användaren.
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
