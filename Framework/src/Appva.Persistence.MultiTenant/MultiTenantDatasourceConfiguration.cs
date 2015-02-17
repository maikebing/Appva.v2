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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
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