// <copyright file="IPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using NHibernate;

    #endregion

    /// <summary>
    /// Persistence context (<see cref="ISession"/>) provider and resolver interface. 
    /// </summary>
    public interface IPersistenceContextAwareResolver
    {
        /// <summary>
        /// Returns the <see cref="IDatasource"/>.
        /// </summary>
        IDatasource Datasource
        {
            get;
        }

        /// <summary>
        /// Returns the current <see cref="ISessionFactory"/>.
        /// </summary>
        /// <returns>An <see cref="ISessionFactory"/> instance</returns>
        ISessionFactory Resolve();

        /// <summary>
        /// Creates a new <see cref="IPersistenceContext"/> instance.
        /// </summary>
        /// <returns>A new <see cref="IPersistenceContext"/> instance</returns>
        IPersistenceContext CreateNew();
    }
}