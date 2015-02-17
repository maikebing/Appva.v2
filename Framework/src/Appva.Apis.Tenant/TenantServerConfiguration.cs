// <copyright file="TenantServerConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer
{
    #region Imports.

    using System;
    using Appva.Core.Configuration;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantServerConfiguration : ITenantServerConfiguration, IConfigurableResource
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantServerConfiguration"/> class.
        /// </summary>
        public TenantServerConfiguration()
        {
        }

        #endregion

        #region ITenantServerConfiguration Members

        /// <inheritdoc />
        public Uri Uri
        {
            get;
            set;
        }

        #endregion
    }
}