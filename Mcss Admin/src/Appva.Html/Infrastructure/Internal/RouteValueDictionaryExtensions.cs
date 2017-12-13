// <copyright file="RouteValueDictionaryExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure.Internal
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class RouteValueDictionaryExtensions
    {
        public static RouteValueDictionary Merge(this RouteValueDictionary lhs, RouteValueDictionary rhs)
        {
            return new RouteValueDictionary(lhs.Union(rhs).ToDictionary(x => x.Key, x => x.Value));
        }
    }
}