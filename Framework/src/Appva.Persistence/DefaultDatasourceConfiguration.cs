// <copyright file="DefaultDatasourceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core;

    #endregion

    /// <summary>
    /// Default single database source configuration.
    /// </summary>
    public interface IDefaultDatasourceConfiguration : IDatasourceConfiguration, IConfigurableResource
    {
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
        /// <remarks>Optional</remarks>
        IDictionary<string, string> Properties
        {
            get;
        }
    }

    /// <summary>
    /// Default single database source configuration implementation of 
    /// <see cref="IDefaultDatasourceConfiguration"/>.
    /// </summary>
    public sealed class DefaultDatasourceConfiguration : IDefaultDatasourceConfiguration
    {
        #region IDefaultDatasourceConfiguration Members.

        /// <inheritdoc />
        public string ConnectionString
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Assembly
        {
            get;
            set;
        }

        /// <inheritdoc />
        public IDictionary<string, string> Properties
        {
            get;
            set;
        }

        #endregion
    }
}