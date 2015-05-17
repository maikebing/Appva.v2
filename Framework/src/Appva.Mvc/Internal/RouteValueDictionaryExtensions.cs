// <copyright file="RouteValueDictionaryExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Internal
{
    #region Imports.

    using System.Web.Routing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class RouteValueDictionaryExtensions
    {
        /// <summary>
        /// Adds or prepends a css class to a route value dictionary.
        /// </summary>
        /// <param name="dictionary">The <see cref="RouteValueDictionary"/></param>
        /// <param name="classes">The css classes to add</param>
        public static void AddClass(this RouteValueDictionary dictionary, params string[] classes)
        {
            if (classes == null)
            {
                return;
            }
            foreach (var clazz in classes)
            {
                if (dictionary.ContainsKey("class"))
                {
                    dictionary["class"] += " " + clazz;
                }
                else
                {
                    dictionary.Add("class", clazz);
                }
            }
        }
    }
}