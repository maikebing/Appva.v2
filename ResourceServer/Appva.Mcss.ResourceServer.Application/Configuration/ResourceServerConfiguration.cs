// <copyright file="ResourceServerConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Core.Configuration;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ResourceServerConfiguration : IConfigurableResource
    {
        /// <summary>
        /// Whether or not the environment is in production.
        /// </summary>
        public bool IsProduction
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not SSL is required or not.
        /// </summary>
        public bool IsSslRequired
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not to skip access token authorization
        /// and scope verification.
        /// </summary>
        public bool SkipTokenAndScopeAuthorization
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant server url.
        /// </summary>
        public Uri TenantServerUri
        {
            get;
            set;
        }
    }
}