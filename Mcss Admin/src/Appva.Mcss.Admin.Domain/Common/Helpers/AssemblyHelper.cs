// <copyright file="AssemblyHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Common
{
    #region Imports.

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class AssemblyHelper
    {
        /// <summary>
        /// The current assembly;
        /// </summary>
        public static readonly Assembly Assembly = Assembly.GetAssembly(typeof(AssemblyHelper));
        
        /// <summary>
        /// The string type representation to type cache.
        /// </summary>
        public static readonly ConcurrentDictionary<string, Type> Cache = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Resolves the type.
        /// </summary>
        /// <param name="type">The type to rsolve.</param>
        /// <returns>The type; or if not found null.</returns>
        public static Type Resolve(string type)
        {
            Type retval;
            if (Cache.TryGetValue(type, out retval))
            {
                return retval;
            }
            retval = TryResolve(type);
            if (retval == null)
            {
                return retval;
            }
            Cache.TryAdd(type, retval);
            return retval;
        }
        private static Type TryResolve(string typeName)
        {
            Type type;
            if (typeName.StartsWith("Appva.Mcss.Domain.Unit."))
            {
                type = Assembly.GetType(typeName, false, true);
                if (type != null)
                {
                    return type;
                }
            }
            var assemblies = new[] {
                Assembly.FullName.Substring(0, Assembly.FullName.IndexOf(",")),
                "Appva.Mcss.Domain.Unit"
            };
            foreach (var assembly in assemblies)
            {
                type = Type.GetType(assembly + "." + typeName, false, true);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}