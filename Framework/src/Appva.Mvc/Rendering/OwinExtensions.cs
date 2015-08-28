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

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Resources;
    using Microsoft.Owin;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string TenantName(this IOwinContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            string[] tenantName;
            if (!context.Request.Headers.TryGetValue(ClaimTypes.TenantName, out tenantName))
            {
                return string.Empty;
            }
            return tenantName[0].ToString();
        }
    }
}