// <copyright file="FilterConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mvc;

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
            //// filters.Add(new TimingAttribute());
        }
    }
}
