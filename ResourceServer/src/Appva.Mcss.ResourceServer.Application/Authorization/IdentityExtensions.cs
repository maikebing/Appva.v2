// <copyright file="IdentityExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Authorization
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Returns the user id.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentity"/></param>
        /// <returns>The user id</returns>
        public static object Id(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var user = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).SingleOrDefault();
            return new Guid(user.Value);
        }

        /// <summary>
        /// Returns the device id
        /// </summary>
        /// <param name="identity">The <see cref="IIdentity"/></param>
        /// <returns>The device id</returns>
        public static object Device(this IIdentity identity)
        {
            var claimsIdentitiy = identity as ClaimsIdentity;
            var device = claimsIdentitiy.Claims.Where(x => x.Type == AppvaClaimTypes.Device ).SingleOrDefault();    
            return new Guid(device.Value);
        }

        /// <summary>
        /// Returns the tenant id.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentity"/></param>
        /// <returns>The tenant id</returns>
        public static object Tenant(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var tenant = claimsIdentity.Claims.Where(x => x.Type == AppvaClaimTypes.Tenant).SingleOrDefault();
            return new Guid(tenant.Value);
        }
    }
}