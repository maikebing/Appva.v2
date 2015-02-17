// <copyright file="ITenantServerConfiguration.cs" company="Appva AB">
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
    public interface ITenantServerConfiguration : IConfigurableResource
    {
        /// <summary>
        /// The tenant server base url.
        /// </summary>
        Uri Uri
        {
            get;
        }
    }
}