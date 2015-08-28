// <copyright file="Attribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Utilities
{
    #region Imports.

    using System;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class AttributeUtilities
    {
        /// <summary>
        /// Returns the attribute value.
        /// </summary>
        /// <typeparam name="T">The attribute type</typeparam>
        /// <param name="assembly">The assembly</param>
        /// <param name="value">The attribute value to retrieve</param>
        /// <returns>The string value</returns>
        public static string Find<T>(Assembly assembly, Func<T, string> value) where T : Attribute
        {
            try
            {
                var attribute = Attribute.GetCustomAttribute(assembly, typeof(T)) as T;
                if (attribute == null)
                {
                    return string.Empty;
                }
                return value.Invoke(attribute);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}