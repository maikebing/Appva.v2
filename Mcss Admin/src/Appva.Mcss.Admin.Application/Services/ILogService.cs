// <copyright file="ILogService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Repository;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ILogService : IService
    {
        PageableSet<LogModel> List(DateTime? cursor = null, int page = 1, int pageSize = 100);

        #region Old stuff

        //// TODO: Remove this?
        void Info(string message, Account account, Patient patient);
        void Info(string message, Account account, Patient patient, LogType type);
        void Info(string message, Account account, LogType type);

        #endregion
    }

    public class LogService : ILogService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ILogRepository"/>
        /// </summary>
        private readonly ILogRepository logRepository;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService audit;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="logRepository"></param>
        /// <param name="audit"></param>
        public LogService(ILogRepository logRepository, IAuditService audit, IIdentityService identity)
        {
            this.logRepository = logRepository;
            this.audit = audit;
            this.identity = identity;
        }

        #endregion

        #region ILogService Members

        public PageableSet<LogModel> List(DateTime? cursor = null, int page = 1, int pageSize = 100)
        {
            
            var retval = this.logRepository.List(cursor, page, pageSize);
            this.audit.Read("Användare {0} läste logg mellan {1} och {2}", 
                this.identity.PrincipalId, 
                ((List<LogModel>)retval.Entities).FirstOrDefault().CreatedAt, 
                ((List<LogModel>)retval.Entities).Last().CreatedAt);

            return retval;
        }

        #region Old

        public void Info(string message, Account account, Patient patient, LogType type)
        {
            return;
        }

        public void Info(string message, Account account, LogType type)
        {
            return;
        }

        public void Info(string message, Account account, Patient patient)
        {
            return;
        }

        #endregion

        #endregion
    }
}