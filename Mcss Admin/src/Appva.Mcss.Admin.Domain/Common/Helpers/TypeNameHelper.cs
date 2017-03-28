// <copyright file="TypeNameHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class TypeNameHelper
    {
        /// <summary>
        /// Returns the string representation of the assembly qualified name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The assembly qualified name; or null if obj is null.</returns>
        public static string GetSimpleTypeName(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return obj.GetType().AssemblyQualifiedName;
        }

        /// <summary>
        /// Returns the type by type name.
        /// </summary>
        /// <param name="typeName">The simple type name.</param>
        /// <returns>The type.</returns>
        public static Type GetType(string typeName)
        {
            return Type.GetType(typeName);
        }
    }
}