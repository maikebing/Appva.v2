// <copyright file="ClaimsPrincipalExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// FIXME: Return a tenant object
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static Guid TenantId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(Core.Resources.ClaimTypes.TenantId);
            if (claim == null)
            {
                //// Something is wrong then!
            }
            return new Guid(claim.Value);
        }

        /// <summary>
        /// FIXME: This should be implemented.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string TenantName(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(Core.Resources.ClaimTypes.TenantName);
            if (claim == null)
            {
                //// Something is wrong then!
            }
            return claim.Value;
        }
    }
}