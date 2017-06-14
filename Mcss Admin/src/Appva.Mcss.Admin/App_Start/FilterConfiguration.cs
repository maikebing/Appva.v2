// <copyright file="FilterConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// The MVC filter configuration.
    /// </summary>
    internal static class FilterConfiguration
    {
        /// <summary>
        /// Registers all global MVC controller filters.
        /// </summary>
        /// <param name="filters">The <see cref="GlobalFilterCollection"/></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //// FIXME: These should be added by IIS.         
            filters.Add(new ContentSecurityPolicyAttribute("default-src 'self' style-src 'self' 'unsafe-eval' 'unsafe-inline' connect-src 'self' support.appva.jp;"));
            filters.Add(new ContentTypeOptionsAttribute());
            filters.Add(new StrictTransportSecurityAttribute());
            //// Remove the ability to go back into the web application after a user is signed out by pressing back.
            filters.Add(new NoBrowserCacheAttribute());
        }
    }
}
