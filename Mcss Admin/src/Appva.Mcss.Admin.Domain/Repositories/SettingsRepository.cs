// <copyright file="SettingsRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISettingsRepository : IRepository<Setting>,
        IListRepository<Setting>, ISaveRepository<Setting>
    {
        /// <summary>
        /// Returns a single <see cref="Setting"/> instance
        /// by <see cref="Setting.Key"/>.
        /// </summary>
        /// <param name="key">The unique setting identifier</param>
        /// <returns>A <see cref="Setting"/> instance or null if not found</returns>
        Setting Find(string key);

        /// <summary>
        /// Returns a collection <see cref="Setting"/> instance
        /// by <see cref="Setting.Namespace"/>.
        /// </summary>
        /// <param name="theNamespace">The namespace</param>
        /// <returns>A colelction of <see cref="Setting"/> instances</returns>
        IList<Setting> FindByNamespace(string theNamespace);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SettingsRepository : Repository<Setting>, ISettingsRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public SettingsRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region ISettingsRepository Members.

        /// <inheritdoc />
        public Setting Find(string key)
        {
            var retval = this.Context.QueryOver<Setting>()
                .Where(x => x.IsActive    == true)
                  .And(x => x.MachineName == key)
                .OrderBy(x => x.Namespace).Asc
                .List();
            if (retval.Count != 1)
            {
                return null;
            }
            return retval.First();
        }

        /// <inheritdoc />
        public IList<Setting> FindByNamespace(string theNamespace)
        {
            return this.Context
                .QueryOver<Setting>()
                    .Where(x => x.IsActive  == true)
                      .And(x => x.Namespace == theNamespace)
                .OrderBy(x => x.Namespace).Asc
                .List();
        }

        #endregion

        #region IListRepository<Setting> Members.

        /// <inheritdoc />
        public IList<Setting> List()
        {
            return this.Context
                .QueryOver<Setting>()
                    .Where(x => x.IsActive == true)
                .OrderBy(x => x.Namespace).Asc
                .Take(int.MaxValue)
                .List();
        }

        #endregion
    }
}