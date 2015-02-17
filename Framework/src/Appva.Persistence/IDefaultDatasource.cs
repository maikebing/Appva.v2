// <copyright file="IDefaultDatasource.cs" company="Appva AB">
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
    using NHibernate;

    #endregion

    /// <summary>
    /// Default single database <see cref="IDatasource"/>.
    /// </summary>
    public interface IDefaultDatasource : IDatasource
    {
        /// <summary>
        /// The <see cref="ISessionFactory"/> instance.
        /// </summary>
        ISessionFactory SessionFactory
        {
            get;
        }
    }
}