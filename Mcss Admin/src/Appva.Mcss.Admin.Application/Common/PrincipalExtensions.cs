// <copyright file="PrincipalExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class PrincipalExtensions
    {
        public const string LocationsClaimType     = "https://schemas.appva.se/2015/04/practitioner/locations";
        public const string LocationPathsClaimType = "https://schemas.appva.se/2015/04/practitioner/locations/paths";

        public const string PrescriberCodeClaimType = "https://schemas.appva.se/2015/04/practitioner/prescriber-code";
        public const string LegitimationCodeClaimType = "https://schemas.appva.se/2015/04/practitioner/legitimation-code";

        public static Guid Id(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .SingleOrDefault();
            if (claim == null)
            {
                return Guid.Empty;
            }
            return new Guid(claim.Value);
        }

        public static Guid Location(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).Claims
                .Where(x => x.Type == LocationsClaimType)
                .SingleOrDefault();
            if (claim == null)
            {
                return Guid.Empty;
            }
            return new Guid(claim.Value);
        }

        public static string LocationPath(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).Claims
                .Where(x => x.Type == LocationPathsClaimType)
                .SingleOrDefault();
            if (claim == null)
            {
                return string.Empty;
            }
            return claim.Value;
        }

        public static string PrescriberCode(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).Claims
                .Where(x => x.Type == PrescriberCodeClaimType)
                .SingleOrDefault();
            if (claim == null)
            {
                return string.Empty;
            }
            return claim.Value;
        }

        public static string LegitimationCode(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).Claims
                .Where(x => x.Type == LegitimationCodeClaimType)
                .SingleOrDefault();
            if (claim == null)
            {
                return string.Empty;
            }
            return claim.Value;
        }
    }
}