// <copyright file="Platform.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Configuration.IO
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Helper class for platform specific checks.
    /// </summary>
    internal static class Platform
    {
        /// <summary>
        /// Whether or not the application runs in Mono.
        /// </summary>
        private static readonly Lazy<bool> IsRunningInMono = new Lazy<bool>(() => Type.GetType("Mono.Runtime") != null);

        /// <summary>
        /// Whether or not the application runs in Mono.
        /// </summary>
        public static bool IsMono
        {
            get
            {
                return IsRunningInMono.Value;
            }
        }
    }
}
