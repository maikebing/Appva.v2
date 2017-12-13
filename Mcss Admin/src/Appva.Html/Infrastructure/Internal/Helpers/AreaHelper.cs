// <copyright file="AreaHelper.cs" company="Appva AB">
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

    internal static class RouteHelpers
    {
        public const string Area       = "area";
        public const string Controller = "controller";
        public const string Action     = "action";

        public static string ActionName(ViewContext context)
        {
            object action;
            if (context.RouteData.Values.TryGetValue(Action, out action))
            {
                return action as string;
            }
            return null;
        }

        public static string ControllerName(ViewContext context)
        {
            object controller;
            if (context.RouteData.Values.TryGetValue(Controller, out controller))
            {
                return controller as string;
            }
            return null;
        }

        public static string AreaName(ViewContext context)
        {
            return AreaName(context.RouteData);
        }

        private static string AreaName(RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue(Area, out area))
            {
                return area as string;
            }
            return AreaName(routeData.Route);
        }

        private static string AreaName(RouteBase route)
        {
            var routeWithArea = route as IRouteWithArea;
            if (routeWithArea != null)
            {
                return routeWithArea.Area;
            }
            var castRoute = route as System.Web.Routing.Route;
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens[Area] as string;
            }
            return null;
        }
    }
}