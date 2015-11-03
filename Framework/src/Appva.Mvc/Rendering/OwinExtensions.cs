// <copyright file="OwinExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using Microsoft.Owin;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// The tenant header name.
        /// </summary>
        public const string TenantHeader = "Tenant-Name";

        /// <summary>
        /// Returns the current tenant name from header.
        /// </summary>
        /// <param name="context">The <see cref="IOwinContext"/></param>
        /// <returns>The current tenant name</returns>
        public static string TenantName(this IOwinContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            string[] tenantName;
            if (! context.Request.Headers.TryGetValue(TenantHeader, out tenantName))
            {
                return string.Empty;
            }
            return tenantName[0].ToString();
        }
    }
}