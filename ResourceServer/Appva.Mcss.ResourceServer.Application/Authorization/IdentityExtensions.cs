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
        /// <param name="principal">The <see cref="IPrincipal"/></param>
        /// <returns>The user id</returns>
        public static object Id(this IIdentity principal)
        {
            var identity = principal as ClaimsIdentity;
            return new Guid(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}