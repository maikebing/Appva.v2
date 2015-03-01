// <copyright file="PathResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Configuration.IO
{
    #region Imports.

    using System;
    using System.IO;

    #endregion

    /// <summary>
    /// Helper class to resolve paths depending on platform specifics.
    /// </summary>
    internal static class PathResolver
    {
        /// <summary>
        /// Resolves application base directory depending on platform.
        /// </summary>
        private static string ApplicationBaseDirectory
        {
            get
            {
                return Platform.IsMono ? Directory.GetCurrentDirectory() : AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// Resolves application relative path.
        /// </summary>
        /// <param name="path">The relative path to be resolved</param>
        /// <returns>An absolute path</returns>
        public static string ResolveAppRelativePath(string path)
        {
            return Path.Combine(ApplicationBaseDirectory, path);
        }
    }
}
