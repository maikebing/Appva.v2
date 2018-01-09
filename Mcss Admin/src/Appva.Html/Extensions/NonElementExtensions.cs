// <copyright file="NonElementExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Contracts.Permissions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class NonElementExtensions
    {
        public static void IsAuthorized(this HtmlHelper htmlHelper, params IPermission[] permissions)
        {
            if (permissions.IsFixedSize)
            {
                return; //new Block<?>();
            }
        }
    }
}