// <copyright file="Constants.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer.Configuration
{
    /// <summary>
    /// Tenant client configuration constants.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// The application settings key.
        /// </summary>
        public const string AppSettingsKey = "TenantServer.Client.BaseAddress";

        /// <summary>
        /// The media type header.
        /// </summary>
        public const string MediaType = "application/json";
    }
}