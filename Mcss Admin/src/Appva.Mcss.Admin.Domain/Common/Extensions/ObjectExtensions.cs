// <copyright file="ObjectExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Object extension helpers.
    /// </summary>
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Returns if <paramref name="obj"/> is null.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if null; false otherwise.</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Casts the <paramref name="obj"/> to a <paramref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="obj">The object to cast.</param>
        /// <returns>T or null.</returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="obj"/> is not compatible or of the same type as the given type.
        /// </exception>
        public static T Cast<T>(this object obj)
        {
            if (obj.IsNot<T>())
            {
                throw new ArgumentException("obj is not the same type as this instance.", "obj");
            }
            return (T)obj;
        }

        /// <summary>
        /// Checks if an object is compatible with a given type.
        /// </summary>
        /// <typeparam name="T">The type to check compatibilty.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the object is compatile with the given type.</returns>
        public static bool Is<T>(this object obj)
        {
            return obj is T;
        }

        /// <summary>
        /// Checks if an object is not compatible with a given type.
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