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
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Exception;

    #endregion

    /// <summary>
    /// The <see cref="ITenaService"/>.
    /// </summary>
    public interface ITenaService : IService
    {
        /// <summary>
        /// If the provided external id is unique.
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasUniqueExternalId(string externalId);

        /// <summary>
        /// Lists the tena observation period.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        IList<TenaObservation> ListTenaObservationPeriod(Guid patientId);

        /// <summary>
        /// Creates a new ObserverPeriod.
        /// </summary>
        /// <param name="patient">The patient in this context.</param>
        /// <param name="startdate">Starting date for the period</param>
        /// <param name="enddate">Ending date for the period</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        TenaObservation CreateTenaObservervationPeriod(Patient patient, DateTime startdate, DateTime enddate);

        /// <summary>
        /// Updates the tena pbservation period.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="startsAt">The starts at.</param>
        /// <param name="endsAt">The ends at.</param>
        /// <param name="instruction">The instruction.</param>
        /// <returns></returns>
        TenaObservation UpdateTenaObservationPeriod(Guid id, DateTime startsAt, DateTime endsAt, string instruction);

        /// <summary>
        /// Determines whether the specified patient identifier is unique.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="startdate">The startdate.</param>
        /// <param name="enddate">The enddate.</param>
        /// <param name="ignorePeriodId">The ignore period identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified patient identifier is unique; otherwise, <c>false</c>.
        /// </returns>
        bool IsUnique(Guid patientId, DateTime startdate, DateTime enddate, Guid? ignorePeriodId);

        /// <summary>
        /// Get a specific Period from Database
        /// </summary>
        /// <param name="periodId">Period ID</param>
        /// <returns>Returns a <see cref="TenaObservation"/>observation period</returns>
        TenaObservation GetTenaObservationPeriod(Guid periodId);

        /// <summary>
        /// Get data from the TENA API.
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="GetResidentModel"/>.</returns>
        Task<Resident> GetResidentAsync(string externalId);

        /// <summary>
        /// Post Period to the TENA API.
        /// </summary>
        /// <param name="periodId">The Observation Period ID.</param>
        /// <returns>Returns a list of <see cref="GetManualEventModel"/>.</returns>
        Task<List<ManualEventResult>> UploadAllEventsFor(Guid periodId);

        /// <summary>
        /// Uploads all events for.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        Task<List<ManualEventResult>> UploadAllEventsFor(TenaObservation period);
    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenaObservationPeriodRepository"/>.
        /// </summary>
        private readonly ITenaObservationPeriodRepository repository;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IApiService"/>.
        /// </summary>
        private readonly ITenaIdentifiClient identifiClient;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService audit;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="ITenaObservationPeriodRepository"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="identifiClient">The <see cref="IApiService"/>.</param>
        public TenaService(
            ITenaObservationPeriodRepository repository, 
            ISettingsService settingsService, 
            ITenaIdentifiClient identifiClient,
            IAuditService audit)
        {
            this.repository      = repository;
            this.settingsService = settingsService;
            this.audit           = audit;
            this.identifiClient  = identifiClient;
        }

        #endregion

        #region ITenaService members.

        /// <inheritdoc />
        public bool HasUniqueExternalId(string externalId)
        {
            return this.repository.HasUniqueExternalId(externalId);
        }

        public IList<TenaObservation> ListTenaObservationPeriod(Guid patientId)
        {
            return this.repository.ListTenaPeriods(patientId);
        }

        /// <inheritdoc />
        public TenaObservation CreateTenaObservervationPeriod(Patient patient, DateTime startdate, DateTime enddate)
        {
            /*if ( ! this.IsUnique(patient.Id, startdate, enddate, null))
            {
                throw new NotUniqueException("The given date is not unique");
            }*/
            var period = new TenaObservation(startdate, enddate, patient, "TENA Identifi", "För registrering av toalettbesök, läckage utanför produkten och ev avföring i produkten");
            this.repository.Save(period);

            this.audit.Create(patient, "skapade TENA Identifi mätperiod {0} - {1} (ref, {2})", startdate, enddate, period.Id);
            return period;
        }

        /// <inheritdoc />
        public TenaObservation UpdateTenaObservationPeriod(Guid id, DateTime startsAt, DateTime endsAt, string instruction)
        {
            var period = this.repository.Get(id);
            period.Update(period.Name, instruction, startsAt, endsAt);
            this.repository.Update(period);

            return period;
        }

        /// <inheritdoc />
        public TenaObservation GetTenaObservationPeriod(Guid periodId)
        {
            return this.repository.GetTenaPeriod(periodId);
        }
        
        /// <inheritdoc />
        public bool IsUnique(Guid patientId, DateTime startdate, DateTime enddate, Guid? ignorePeriodId)
        {
            return this.repository.IsUnique(patientId, startdate, enddate, ignorePeriodId);
        }

        #endregion

        #region API communication members.

        /// <inheritdoc />
        public async Task<Resident> GetResidentAsync(string externalId)
        {
            if (this.HasUniqueExternalId(externalId) == false)
            {
                return new Resident
                {
                    Message = "Detta ID är redan registrerat på en boende."
                };
            }

            return await this.identifiClient.GetResidentAsync(externalId, this.GetCredentials());
        }

        /// <inheritdoc />
        public async Task<List<ManualEventResult>> UploadAllEventsFor(Guid periodId)
        {
            var period = this.repository.GetTenaPeriod(periodId);
            var retval = await this.UploadAllEventsFor(period);
            return retval;
        }

        /// <inheritdoc />
        public async Task<List<ManualEventResult>> UploadAllEventsFor(TenaObservation period)
        {
            var measurements = period.Items;
            if (measurements.IsEmpty())
            {
                return null;
            }
            return await this.identifiClient.PostManualEventsAsync(this.ConvertDataToTenaModel(measurements), this.GetCredentials());
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private List<ManualEvent> ConvertDataToTenaModel(IList<ObservationItem> tenaObservationItemsList)
        {
            var tenaApiList = new List<ManualEvent>();
            foreach (var item in tenaObservationItemsList)
            {
                //// FIX: Must convert to utc-time before sending to TENA.
                var createdAt = new DateTime(item.CreatedAt.Ticks, DateTimeKind.Local);
                tenaApiList.Add(new ManualEvent
                {
                    Id         = item.Id.ToString(),
                    EventType  = item.Measurement.ToString(),
                    ResidentId = item.Observation.Patient.TenaId,
                    Timestamp  = createdAt.ToUtc(),
                    Active     = item.IsActive
                });
            }
            return tenaApiList;
        }

        /// <inheritdoc />
        private TenaIdentifiCredentials GetCredentials()
        {
            var settings = this.settingsService.Find(ApplicationSettings.TenaSettings);
            return new TenaIdentifiCredentials(settings.ClientId, settings.ClientSecret);
        }
        #endregion
    }
}
