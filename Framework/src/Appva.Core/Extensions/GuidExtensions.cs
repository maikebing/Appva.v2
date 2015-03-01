// <copyright file="GuidExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Extensions
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// <see cref="Guid"/> extension helper methods.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Checks whether or not the <see cref="Guid"/> is null or empty.
        /// </summary>
        /// <param name="guid">The guid to be checked</param>
        /// <returns>True if the <see cref="Guid"/> is null or empty</returns>
        public static bool IsEmpty(this Guid? guid)
        {
            return guid == null || Guid.Empty.Equals(guid.Value);
        }

        /// <summary>
        /// Checks whether or not the <see cref="Guid"/> is not null or empty.
        /// </summary>
        /// <param name="guid">The guid to be checked</param>
        /// <returns>True if the <see cref="Guid"/> is not null or empty</returns>
        public static bool IsNotEmpty(this Guid? guid)
        {
            return ! guid.IsEmpty();
        }

        /// <summary>
        /// Checks whether or not the <see cref="Guid"/> has an empty default value
        /// 00000000-0000-0000-0000-000000000000.
        /// </summary>
        /// <param name="guid">The guid to be checked</param>
        /// <returns>True if the <see cref="Guid"/> has an empty default value</returns>
        public static bool IsEmpty(this Guid guid)
        {
            return Guid.Empty.Equals(guid);
        }

        /// <summary>
        /// Checks whether or not the <see cref="Guid"/> not has an empty default value
        /// 00000000-0000-0000-0000-000000000000.
        /// </summary>
        /// <param name="guid">The guid to be checked</param>
        /// <returns>True if the <see cref="Guid"/> has a non default value</returns>
        public static bool IsNotEmpty(this Guid guid)
        {
            return Guid.Empty != guid;
        }
    }
}
