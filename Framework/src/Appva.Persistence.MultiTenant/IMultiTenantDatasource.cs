// <copyright file="IMultiTenantDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System.Collections.Generic;
    using NHibernate;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMultiTenantDatasource : IDatasource
    {
        /// <summary>
        /// The key <see cref="ISessionFactory"/> map instance.
        /// </summary>
        IDictionary<string, ISessionFactory> SessionFactories
        {
            get;
        }

        /// <summary>
        /// Looks up the value for the key provided in the <see cref="ISessionFactory"/>
        /// key value store.
        /// </summary>
        /// <param name="key">The key stored</param>
        /// <returns>An <see cref="ISessionFactory"/> instance if found, else null</returns>
        ISessionFactory Lookup(string key);
    }
}