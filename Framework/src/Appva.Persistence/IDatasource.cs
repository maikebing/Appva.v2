// <copyright file="IDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;

    #endregion

    /// <summary>
    /// The data source for which the database connection will be made.
    /// </summary>
    public interface IDatasource
    {
        /// <summary>
        /// Attempts to connect to the data source and establish a database connection.
        /// </summary>
        void Connect();
    }
}