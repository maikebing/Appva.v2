﻿// <copyright file="Route.cs" company="Appva AB">
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
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.Core.Contracts.Permissions;
    using Appva.Html.Infrastructure;
    using Appva.Html.Infrastructure.Internal;
    using Appva.Mcss.Admin;

    #endregion

    public interface IRoute
    {
        bool IsValid
        {
            get;
        }
        AuthorizationState State
        {
            get;
        }
        string Url
        {
            get;
        }
        RouteKey Key
        {
            get;
        }
    }
    public enum AuthorizationState
    {
        UnAuthorized,
        Authorized
        
    }
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class Route : IRoute
    {
        #region Variables.

        public const string Area = "area";
        public const string Controller = "controller";
        public const string Action = "action";

        private readonly object action;
        private readonly string controller;
        private readonly string area;
        private readonly RouteValueDictionary values;
        private readonly string url;
        private readonly AuthorizationState state;
        private readonly string name;
        private readonly HttpVerbs method;
        private readonly RouteKey key;

        #endregion

        #region Constructors.

        public Route(HtmlHelper htmlHelper, RouteValueDictionary values)
        {
            Argument.Guard.Contains("values", Action,     values);
            Argument.Guard.Contains("values", Controller, values);
            this.action     = values[Action]     as string;
            this.controller = values[Controller] as string;
            this.area       = values[Area]       as string;
            this.method     = HttpVerbs.Post;
            this.key        = this.GenerateKey();
            this.state      =  PermissionManager.Instance.IsAuthorized(htmlHelper, this.key) ? 
                               AuthorizationState.Authorized : AuthorizationState.UnAuthorized;
            if (this.state == AuthorizationState.Authorized)
            {
                var path = htmlHelper.RouteCollection.GetVirtualPathForArea(htmlHelper.ViewContext.RequestContext, values);
                this.url = path == null ? null : path.VirtualPath;
            }
        }

        #endregion

        public bool IsValid
        {
            get
            {
                return true;
            }
        }
        public AuthorizationState State
        {
            get
            {
                return this.state;
            }
        }
        public string Url
        {
            get
            {
                return this.url;
            }
        }
        public RouteKey Key
        {
            get
            {
                return this.key;
            }
        }

        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   actionName:
        //     The name of the action method to invoke.
        //
        //   routeValues:
        //     An object that contains the parameters for a route. You can use routeValues
        //     to provide the parameters that are bound to the action method parameters.
        //     The routeValues parameter is merged with the original route values and overrides
        //     them.
        public static Route New<T>(HtmlHelper htmlHelper, Expression<Action<T>> action) where T : Controller
        {
            return new Route(htmlHelper, ExpressionHelpers.GetRouteValuesFromExpression(action));
        }

        public static Route New(HtmlHelper htmlHelper, string action, object parameters)
        {
            return New(htmlHelper, action, null, null, parameters);
        }

        private static Route New(HtmlHelper htmlHelper, string action, string controller, string area, object parameters)
        {
            action     = action     ?? RouteHelpers.ActionName(htmlHelper.ViewContext);
            controller = controller ?? RouteHelpers.ControllerName(htmlHelper.ViewContext);
            area       = area       ?? RouteHelpers.AreaName(htmlHelper.ViewContext);
            var routes = new RouteValueDictionary 
            { 
                { Action,      action     ?? RouteHelpers.ActionName(htmlHelper.ViewContext     )},
                { Controller,  controller ?? RouteHelpers.ControllerName(htmlHelper.ViewContext )}
            };
            if (area != null)
            {
                routes.Add(Area, area);
            }
            return new Route(htmlHelper, routes.Merge(HtmlHelper.AnonymousObjectToHtmlAttributes(parameters)));
        }

        private RouteKey GenerateKey()
        {
            var inner = string.Join(".", this.action ?? "*", this.controller ?? "*", this.area ?? "*", this.method).ToLowerInvariant().Replace("controller", "");
            return new RouteKey(inner);
        }
        

    }
}