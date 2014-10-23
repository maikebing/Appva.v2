// <copyright file="Principal.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class Principal
    {
        /// <summary>
        /// Returns an anonymous claims principal.
        /// </summary>
        public static ClaimsPrincipal Anonymous
        {
            get
            {
                return Create(null, new Claim(ClaimTypes.Name, string.Empty));
            }
        }

        /// <summary>
        /// Creates a <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="authenticationType"></param>
        /// <param name="claims"></param>
        /// <returns>A new <see cref="ClaimsPrincipal"/></returns>
        public static ClaimsPrincipal Create(string authenticationType, params Claim[] claims)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> CreateRoles(IList<string> roles)
        {
            return roles == null ? new Claim[] { } : new List<Claim>(from r in roles select new Claim(ClaimTypes.Role, r)).ToArray();
        }
    }
}