// <copyright file="AuditService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Auditing
{
    #region Imports.

    using System;
    using System.Security.Claims;
    using System.Web;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IAuditService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Create(string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Create(Patient patient, string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Read(string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Read(Patient patient, string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Update(string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Update(Patient patient, string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Delete(string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Delete(Patient patient, string format, params object[] args);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuditService : IAuditService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="HttpContextBase"/>.
        /// </summary>
        private readonly HttpContextBase context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditService"/> class.
        /// </summary>
        public AuditService(IPersistenceContext persistence, HttpContextBase context)
        {
            this.persistence = persistence;
            this.context = context;
        }

        #endregion

        #region IAuditService Members.

        /// <inheritdoc />
        public void Create(string format, params object[] args)
        {
            this.CreateEventLog(LogType.Write, null, format, args);
        }

        /// <inheritdoc />
        public void Create(Patient patient, string format, params object[] args)
        {
            this.CreateEventLog(LogType.Write, patient, format, args);
        }

        /// <inheritdoc />
        public void Read(string format, params object[] args)
        {
            this.CreateEventLog(LogType.Read, null, format, args);
        }

        /// <inheritdoc />
        public void Read(Patient patient, string format, params object[] args)
        {
            this.CreateEventLog(LogType.Read, patient, format, args);
        }

        /// <inheritdoc />
        public void Update(string format, params object[] args)
        {
            this.CreateEventLog(LogType.Write, null, format, args);
        }

        /// <inheritdoc />
        public void Update(Patient patient, string format, params object[] args)
        {
            this.CreateEventLog(LogType.Write, patient, format, args);
        }

        /// <inheritdoc />
        public void Delete(string format, params object[] args)
        {
            this.CreateEventLog(LogType.Write, null, format, args);
        }

        /// <inheritdoc />
        public void Delete(Patient patient, string format, params object[] args)
        {
            this.CreateEventLog(LogType.Write, patient, format, args);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="patient"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        private void CreateEventLog(LogType type, Patient patient, string format, params object[] args)
        {
            Account account = null;
            var prepend = string.Empty;
            var identity = this.context.User as ClaimsPrincipal;
            if (identity != null && identity.Identity != null && identity.Identity.IsAuthenticated)
            {
                var id = new Guid(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                account = persistence.Get<Account>(id);
                prepend = string.Format("Användare {0} ", account.FullName);
            }
            var message = prepend + string.Format(format, args);
            persistence.Save<Log>(
                new Log
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Level = LogLevel.Info,
                    Message = message,
                    Type = type,
                    Route = this.context.Url(),
                    IpAddress = this.context.RemoteIP(),
                    Account = account,
                    Patient = patient,
                    System = SystemType.Web
                });
        }

        #endregion
    }
}