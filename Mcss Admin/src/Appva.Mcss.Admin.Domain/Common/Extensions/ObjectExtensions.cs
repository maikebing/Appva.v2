// <copyright file="ObjectExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// Object extension helpers.
    /// </summary>
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Checks if an object is compatible with a given type. For example, the following 
        /// code can determine if an object is an instance of the MyObject type, or a type 
        /// that derives from MyObject.
        /// </summary>
        /// <typeparam name="T">The type to check compatibilty.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the object is compatile with the given type.</returns>
        public static bool Is<T>(this object obj)
        {
            return obj is T;
        }

        /// <summary>
        /// Checks if an object is compatible with a given type. For example, the following 
        /// code can determine if an object is an instance of the MyObject type, or a type 
        /// that derives from MyObject.
        /// </summary>
        /// <typeparam name="T">The type to check compatibilty.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the object is not compatile with the given type.</returns>
        public static bool IsNot<T>(this object obj)
        {
            return ! obj.Is<T>();
        }
    }
}