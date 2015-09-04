// <copyright file="HttpUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Http
{
    #region Imports.

    using System;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class HttpUtils
    {
        /// <summary>
        /// Returns the full redirect <c>url</c>.
        /// </summary>
        /// <param name="redirectPath">The redirect path</param>
        /// <returns>The full redirect <c>url</c></returns>
        public static Uri RedirectPath(string redirectPath)
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                return null;
            }
            var request = HttpContext.Current.Request;
            var host = string.Format("{0}://{1}", request.Url.Scheme, request.Url.IsDefaultPort ? request.Url.Host : request.Url.Authority);
            return (request.ApplicationPath == "/") ? new Uri(new Uri(host), redirectPath) : new Uri(new Uri(host + request.ApplicationPath), redirectPath);
        }
    }
}