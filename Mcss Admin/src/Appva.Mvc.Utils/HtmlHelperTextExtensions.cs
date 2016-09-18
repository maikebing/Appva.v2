// <copyright file="Text.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class HtmlHelperTextExtensions
    {
        #region Static members

        public static string Text([NotNull] this HtmlHelper helper, string key)
        {
            var text = helper.ViewContext.HttpContext.GetGlobalResourceObject("Language", key);
            if (text == null)
            {
                return key;
            }

            return text.ToString();
        }

        #endregion
    }
}