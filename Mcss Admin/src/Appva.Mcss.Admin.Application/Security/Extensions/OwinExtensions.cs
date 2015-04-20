// <copyright file="OwinExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.Owin;

    #endregion

    /// <summary>
    /// Extension helpers for Owin.
    /// </summary>
    internal static class OwinExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ClaimsPrincipal Principal(this IOwinContext context)
        {
            return context.Request.User as ClaimsPrincipal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public static bool IsUserNull(this IOwinContext context, AuthenticationType authenticationType)
        {
            if (authenticationType == null)
            {
                return true;
            }
            if (context == null || context.Request == null || context.Request.User == null)
            {
                return true;
            }
            if (! context.Request.User.Identity.AuthenticationType.Equals(authenticationType.Value))
            {
                return true;
            }
            return false;
        }
    }
}