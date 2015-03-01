// <copyright file="ObjectExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Extensions
{
    /// <summary>
    /// <see cref="object"/> extension helper methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks whether or not the <see cref="T"/> is null.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be checked for nullity</param>
        /// <returns>True if the object is null</returns>
        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        /// <summary>
        /// Checks whether or not the <see cref="T"/> is not null.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be checked for nullity</param>
        /// <returns>True if the object is not null</returns>
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return ! obj.IsNull();
        }

        /// <summary>
        /// Checks whether or not the <see cref="T"/> not equals the other.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be checked for equality</param>
        /// <param name="other">The other object to be checked against</param>
        /// <returns>True if the two objects are not equal</returns>
        public static bool NotEquals<T>(this T obj, T other) where T : class
        {
            return ! obj.Equals(other);
        }

        /// <summary>
        /// Checks whether or not the current object is of type T.
        /// </summary>
        /// <typeparam name="T">The type to check against</typeparam>
        /// <param name="obj">The object to be checked</param>
        /// <returns>True if the object is of type T</returns>
        public static bool Is<T>(this object obj) where T : class
        {
            return obj is T;
        }

        /// <summary>
        /// Checks whether or not the current object is not of type T.
        /// </summary>
        /// <typeparam name="T">The type to check against</typeparam>
        /// <param name="obj">The object to be checked</param>
        /// <returns>True if the object is of type T</returns>
        public static bool IsNot<T>(this object obj) where T : class
        {
            return ! obj.Is<T>();
        }
    }
}
