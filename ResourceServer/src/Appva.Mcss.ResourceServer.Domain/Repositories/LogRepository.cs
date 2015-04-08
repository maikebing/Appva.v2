// <copyright file="LogRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ILogRepository : IRepository<Log>
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class LogRepository : Repository<Log>, ILogRepository
    {
        #region Constructor

        public LogRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {

        }

        #endregion

    }
}