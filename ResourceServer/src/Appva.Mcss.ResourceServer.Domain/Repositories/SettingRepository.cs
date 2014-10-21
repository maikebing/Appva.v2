// <copyright file="SettingRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// The settings repository.
    /// </summary>
    public interface ISettingRepository : IPagingAndSortingRepository<Setting>
    {
    }

    /// <summary>
    /// Implementation of <see cref="ISettingRepository"/>.
    /// </summary>
    public class SettingRepository : PagingAndSortingRepository<Setting>, ISettingRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public SettingRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion
    }
}