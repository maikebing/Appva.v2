// <copyright file="LogService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ILogService : IService
    {
        /// <summary>
        /// Logs post-action to Api
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        /// <param name="patientId"></param>
        /// <param name="url"></param>
        void ApiPost(string message, Guid userId, Guid patientId, string url);

        /// <summary>
        /// Logs post-action to Api
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <param name="patient"></param>
        /// <param name="url"></param>
        void ApiPost(string message, Account user, Patient patient, string url);

        /// <summary>
        /// Logs a get-action to Api
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        /// <param name="patientId"></param>
        /// <param name="url"></param>
        void ApiGet(string message, Guid userId, Guid patientId, string url);

        /// <summary>
        /// Logs a get-action to Api
        /// </summary>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <param name="patient"></param>
        /// <param name="url"></param>
        void ApiGet(string message, Account user, Patient patient, string url);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class LogService : ILogService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly ILogRepository LogRepository;

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly IAccountRepository AccountRepository;

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly IPatientRepository PatientRepository;


        #endregion

        #region Constructor.

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="logRepository"></param>
        /// <param name="accountRepository"></param>
        /// <param name="patientRepository"></param>
        public LogService(
            ILogRepository logRepository, 
            IAccountRepository accountRepository, 
            IPatientRepository patientRepository)
        {
            this.LogRepository = logRepository;
            this.AccountRepository = accountRepository;
            this.PatientRepository = patientRepository;
        }

        #endregion

        #region Implementation.

        /// <inheritdoc />
        public void ApiPost(string message, Guid userId, Guid patientId, string url)
        {
            var user = this.AccountRepository.Get(userId);
            var patient = this.PatientRepository.Get(patientId);
            Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Write);
        }

        /// <inheritdoc />
        public void ApiPost(string message, Account user, Patient patient, string url)
        {
            Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Write);
        }

        /// <inheritdoc />
        public void ApiGet(string message, Guid userId, Guid patientId, string url)
        {
            var user = this.AccountRepository.Get(userId);
            var patient = this.PatientRepository.Get(patientId);
            Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Read);
        }

        /// <inheritdoc />
        public void ApiGet(string message, Account user, Patient patient, string url)
        {
            Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Read);
        }

        #endregion

        #region Private members.

        /// <summary>
        /// Creates a log in log-database
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="user"></param>
        /// <param name="patient"></param>
        /// <param name="url"></param>
        /// <param name="system"></param>
        /// <param name="logType"></param>
        private void Log(string message, LogLevel level, Account user, Patient patient, string url, SystemType system, LogType logType)
        {
            var log = new Log
            {
                Account = user,
                Level = level,
                Message = message,
                Patient = patient,
                Route = url,
                System = system,
                Type = logType
            };
            LogRepository.Save(log);
        }

        #endregion


    }
}