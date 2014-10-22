// <copyright file="Constants.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application
{
    #region Imports.

    using System.Diagnostics.CodeAnalysis;

    #endregion
    
    /// <summary>
    /// Availible scopes for the resource server.
    /// </summary>
    public static class Scope
    {
        /// <summary>
        /// Resource read-only scope.
        /// </summary>
        public const string ReadOnly = "https://appvaapis.se/auth/resource.readonly";

        /// <summary>
        /// Resource read-write scope.
        /// </summary>
        public const string ReadWrite = "https://appvaapis.se/auth/resource";

        /// <summary>
        /// Resource admin-only scope.
        /// </summary>
        public const string AdminOnly = "https://appvaapis.se/auth/resource.adminonly";
    }

    /// <summary>
    /// Available settings for the resource server.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public static class Settings
    {
        /// <summary>
        /// The history length.
        /// </summary>
        public const string AllowedHistorySize = "MCSS.Device.Security.HistoryLength";
    }

    /// <summary>
    /// Extra claim types for the resource server.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public static class AppvaClaimTypes
    {
        /// <summary>
        /// The client identifier claim schema.
        /// </summary>
        public const string Client = "https://schemas.appva.se/identity/claims/client";

        /// <summary>
        /// The tenant identifier claim schema.
        /// </summary>
        public const string Tenant = "https://schemas.appva.se/identity/claims/tenant";

        /// <summary>
        /// The audience identifier claim schema.
        /// </summary>
        public const string Audience = "https://schemas.appva.se/identity/claims/tenant";

        /// <summary>
        /// The device identifier claim schema
        /// </summary>
        public const string Device = "https://schemas.appva.se/identity/claims/device";
    }
}