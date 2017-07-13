// <copyright file="PipeUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class PipeUtils
    {
        public static bool IsAnywhereInPipe(string pathToCheck, string path)
        {
            if (path.StartsWith(pathToCheck))
            {
                return true;
            }
            var id = path.Contains(".") ? path.Substring(0, path.LastIndexOf('.')) : path;
            return pathToCheck.Split('.').Contains(id);
        }
    }
}