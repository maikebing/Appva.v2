// <copyright file="IPersistenceUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Persistence unit interface.
    /// </summary>
    public interface IPersistenceUnit
    {
        /// <summary>
        /// The identifier for multi tenancy.
        /// </summary>
        string Id
        {
            get;
        }

        /// <summary>
        /// The database connection string.
        /// </summary>
        string ConnectionString
        {
            get;
        }

        /// <summary>
        /// Assembly of which the NHibernate entities resides.
        /// </summary>
        string Assembly
        {
            get;
        }

        /// <summary>
        /// Persistence unit properties, i.e. NHibernate properties.
        /// </summary>
        IDictionary<string, string> Properties
        {
            get;
        }
    }
}