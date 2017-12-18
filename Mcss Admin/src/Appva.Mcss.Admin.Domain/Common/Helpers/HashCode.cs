// <copyright file="HashCode.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// A hash code helper.
    /// </summary>
    internal static class HashCode
    {
        /// <summary>
        /// The multiplier for each value.
        /// </summary>
        private const int HashCodeMultiplier = 37;

        /// <summary>
        /// The initial hash value.
        /// </summary>
        private const int HashCodeInitializer = 17;

        /// <summary>
        /// Combines the hash codes for the list of objects.
        /// </summary>
        /// <param name="objects">
        /// A collection of object to combine hash code for.
        /// </param>
        /// <returns>
        /// A new combined hash code.
        /// </returns>
        public static int Combine<T>(IEnumerable<T> objects)
        {
            unchecked
            {
                int hash = HashCodeInitializer;
                foreach (var obj in objects)
                {
                    hash = hash * HashCodeMultiplier + (obj != null ? obj.GetHashCode() : 0);
                }
                return hash;
            }
        }
    }
}