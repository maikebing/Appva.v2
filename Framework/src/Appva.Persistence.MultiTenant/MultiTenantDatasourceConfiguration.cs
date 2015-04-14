// <copyright file="MultiTenantDatasourceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Configuration;

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
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantDatasourceConfiguration"/> class.
        /// </summary>
        public MultiTenantDatasourceConfiguration()
        {
        }

        #endregion

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