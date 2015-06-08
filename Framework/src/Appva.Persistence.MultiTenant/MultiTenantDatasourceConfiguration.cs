// <copyright file="MultiTenantDatasourceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core;

    #endregion

    /// <summary>
    /// A multi tenant data source configuration.
    /// </summary>
    public interface IMultiTenantDatasourceConfiguration : IDatasourceConfiguration, IConfigurableResource
    {
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
    /// A <see cref="IMultiTenantDatasourceConfiguration"/> configuration.
    /// </summary>
    public sealed class MultiTenantDatasourceConfiguration : IMultiTenantDatasourceConfiguration
    {
        #region IMultiTenantDatasourceConfiguration Members.

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