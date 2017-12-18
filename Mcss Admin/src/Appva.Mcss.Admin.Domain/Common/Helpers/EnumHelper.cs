// <copyright file="EnumHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
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
    internal static class EnumHelper
    {
        public static T Parse<T>(string value) where T : struct
        {
            T result;
            Enum.TryParse<T>(value, true, out result);
            return result;
        }

        public static bool IsParsable<T>(string value) where T : struct
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            T result;
            return Enum.TryParse<T>(value, true, out result) && Enum.IsDefined(typeof(T), result);
        }
    }
}