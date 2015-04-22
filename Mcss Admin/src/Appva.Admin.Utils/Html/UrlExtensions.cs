// <copyright file="UrlExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Admin.Utils.Html
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class UrlHelperExtensions
    {

        /// <summary>
        /// Helper method to return a formatted stylesheet url
        /// </summary>
        /// <param name="urlHelper">The url helper.</param>
        /// <param name="fileName">The file name.</param>
        public static string Stylesheet(this UrlHelper urlHelper, string fileName)
        {
            return urlHelper.Content(string.Format("~/Assets/css/{0}", fileName));
        }

        /// <summary>
        /// Helper method to return a formatted javascript url
        /// </summary>
        /// <param name="urlHelper">The url helper.</param>
        /// <param name="fileName">The file name.</param>
        public static string Script(this UrlHelper urlHelper, string fileName)
        {
            return urlHelper.Content(string.Format("~/Assets/js/{0}", fileName));
        }

        /// <summary>
        /// Imageses the specified URL helper.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="fileName">Name of the file.</param>
        public static string Image(this UrlHelper urlHelper, string fileName)
        {
            return urlHelper.Content(string.Format("~/Assets/i/{0}", fileName));
        }

        /// <summary>
        /// URL to TenantLogos
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="fileName">Name of the file.</param>
        public static string Logo(this UrlHelper urlHelper, string fileName)
        {
            return urlHelper.Content(string.Format("~/logo/{0}", fileName));
        }

        public static bool IsValidReturnUrl(this UrlHelper urlHelper, string returnUrl)
        {
            return (urlHelper.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"));
        }
    }
}