// <copyright file="HashCode.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
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
        /// Combines the hash codes for the list of objects.
        /// </summary>
        /// <param name="objects">
        /// A collection of object to combine hash code for.
        /// </param>
        /// <returns>
        /// A new combined hash code.
        /// </returns>
        public static int Combine(IEnumerable<object> objects)
        {
            var hash = 17;
            foreach (var obj in objects)
            {
                hash = hash * 23 + (obj != null ? obj.GetHashCode() : 0);
            }
            return hash;
        }
    }

}