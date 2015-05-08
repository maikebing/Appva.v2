// <copyright file="RouteConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    /// <summary>
    /// The MVC controller route configuration.
    /// </summary>
    internal static class RouteConfiguration
    {
        /// <summary>
        /// Register the controller routes.
        /// </summary>
        /// <param name="routes">The <see cref="RouteCollection"/></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new
            {
                favicon = @"(.*/)?favicon.ico(/.*)?"
            });
        }
    }
}