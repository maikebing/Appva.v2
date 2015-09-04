// <copyright file="UriUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class UriUtils
    {
        /// <summary>
        /// The route matches internal key.
        /// </summary>
        private const string DirectRouteMatches = "MS_DirectRouteMatches";

        /// <summary>
        /// Returns the application URL path.
        /// </summary>
        /// <returns>The application URL path</returns>
        public static Uri ApplicationPath()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                return null;
            }
            var request = HttpContext.Current.Request;
            var host = request.Url.IsDefaultPort ? request.Url.Host : request.Url.Authority;
            host = string.Format("{0}://{1}", request.Url.Scheme, host);
            return (request.ApplicationPath == "/") ? new Uri(host) : new Uri(host + request.ApplicationPath);
        }

        /// <summary>
        /// Returns the route value data for a specific URL.
        /// </summary>
        /// <param name="url">The URL to extract the route data from</param>
        /// <returns>A <see cref="RouteValueDictionary"/></returns>
        public static RouteValueDictionary RouteValues(string url)
        {
            var query = string.Empty;
            var newUrl = new Uri(ApplicationPath(), url).ToString();
            var index = newUrl.IndexOf('?');
            if (index != -1)
            {
                query = newUrl.Substring(index + 1);
                newUrl = newUrl.Substring(0, index);
            }
            var context = new HttpContext(new HttpRequest(null, newUrl, query), new HttpResponse(new StringWriter()));
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
            if (routeData != null && routeData.Values.ContainsKey(DirectRouteMatches))
            {
                routeData = ((IEnumerable<RouteData>) routeData.Values["MS_DirectRouteMatches"]).First();
            }
            var values = routeData.Values;
            if (! values.ContainsKey("area"))
            {
                values.Add("area", routeData.DataTokens["area"] as string);
            }
            //// Any route with ? is also added.
            foreach (var key in context.Request.Params.AllKeys)
            {
                if (! values.ContainsKey(key))
                {
                    values.Add(key, context.Request.Params.Get(key));
                }
            }
            return values;
        }
    }
}