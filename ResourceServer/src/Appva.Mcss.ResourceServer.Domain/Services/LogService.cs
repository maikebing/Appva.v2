// <copyright file="LogService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ILogService : IService
    {
        /// <summary>
        /// Logs post-action to Api
        /// </summary>
        /// <param name="message">The message to Log</param>
        /// <param name="userId">The current user id</param>
        /// <param name="patientId">The current patient id</param>
        /// <param name="url">The current URL</param>
        void ApiPost(string message, Guid userId, Guid patientId, string url);

        /// <summary>
        /// Logs post-action to Api
        /// </summary>
        /// <param name="message">The message to Log</param>
        /// <param name="user">The current user</param>
        /// <param name="patient">The current patient</param>
        /// <param name="url">The current URL</param>
        void ApiPost(string message, Account user, Patient patient, string url);

        /// <summary>
        /// Logs a get-action to Api
        /// </summary>
        /// <param name="message">The message to Log</param>
        /// <param name="userId">The current user id</param>
        /// <param name="patientId">The current patient id</param>
        /// <param name="url">The current URL</param>
        void ApiGet(string message, Guid userId, Guid patientId, string url);

        /// <summary>
        /// Logs a get-action to Api
        /// </summary>
        /// <param name="message">The message to Log</param>
        /// <param name="user">The current user</param>
        /// <param name="patient">The current patient</param>
        /// <param name="url">The current URL</param>
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
        private readonly ILogRepository logRepository;

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly IPatientRepository patientRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService" /> class.
        /// </summary>
        /// <param name="logRepository">The <see cref="ILogRepository"/></param>
        /// <param name="accountRepository">The <see cref="IAccountRepository"/></param>
        /// <param name="patientRepository">The <see cref="IPatientRepository"/></param>
        public LogService(
            ILogRepository logRepository, 
            IAccountRepository accountRepository, 
            IPatientRepository patientRepository)
        {
            this.logRepository = logRepository;
            this.accountRepository = accountRepository;
            this.patientRepository = patientRepository;
        }

        #endregion

        #region Implementation.

        /// <inheritdoc />
        public void ApiPost(string message, Guid userId, Guid patientId, string url)
        {
            var user = this.accountRepository.Get(userId);
            var patient = this.patientRepository.Get(patientId);
            this.Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Write);
        }

        /// <inheritdoc />
        public void ApiPost(string message, Account user, Patient patient, string url)
        {
            this.Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Write);
        }

        /// <inheritdoc />
        public void ApiGet(string message, Guid userId, Guid patientId, string url)
        {
            var user = this.accountRepository.Get(userId);
            var patient = this.patientRepository.Get(patientId);
            this.Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Read);
        }

        /// <inheritdoc />
        public void ApiGet(string message, Account user, Patient patient, string url)
        {
            this.Log(message, LogLevel.Info, user, patient, url, SystemType.ResourceApi, LogType.Read);
        }

        #endregion

        #region Private members.

        /// <summary>
        /// Creates a log in log-database
        /// </summary>
        /// <param name="message">The message to Log</param>
        /// <param name="level">The log level</param>
        /// <param name="user">The current user</param>
        /// <param name="patient">The current patient</param>
        /// <param name="url">The current URL</param>
        /// <param name="system">The system</param>
        /// <param name="logType">The log type</param>
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
            this.logRepository.Save(log);
        }

        #endregion
    }
}