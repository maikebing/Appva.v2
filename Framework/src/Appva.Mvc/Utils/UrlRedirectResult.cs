// <copyright file="UrlRedirectResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System.Web.Routing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UrlRedirectResult
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRedirectResult"/> class.
        /// </summary>
        /// <param name="values">The route value dictionary</param>
        private UrlRedirectResult(RouteValueDictionary values)
            : this(values["action"] as string, values["controller"] as string, values["area"] as string, values)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRedirectResult"/> class.
        /// </summary>
        /// <param name="action">The action route name</param>
        /// <param name="controller">The controller route name</param>
        /// <param name="area">The area route name</param>
        /// <param name="values">The route value dictionary</param>
        private UrlRedirectResult(string action, string controller, string area, RouteValueDictionary values = null)
        {
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.Values = this.RemoveRouteValues(values);
            if (! this.Values.ContainsKey("area"))
            {
                this.Values.Add("area", this.Area);
            }
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The action name.
        /// </summary>
        public string Action
        {
            get;
            private set;
        }

        /// <summary>
        /// The controller name.
        /// </summary>
        public string Controller
        {
            get;
            private set;
        }

        /// <summary>
        /// The area name.
        /// </summary>
        public string Area
        {
            get;
            private set;
        }

        /// <summary>
        /// The route values.
        /// </summary>
        public RouteValueDictionary Values
        {
            get;
            private set;
        }

        #endregion

        #region Public static Functions.

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRedirectResult"/> class.
        /// </summary>
        /// <param name="values">The route value dictionary</param>
        /// <returns>A new <see cref="UrlRedirectResult"/> instance</returns>
        public static UrlRedirectResult CreateNew(RouteValueDictionary values)
        {
            return new UrlRedirectResult(values);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRedirectResult"/> class.
        /// </summary>
        /// <param name="action">The action route name</param>
        /// <param name="controller">The controller route name</param>
        /// <param name="area">The area route name</param>
        /// <param name="values">The route value dictionary</param>
        /// <returns>A new <see cref="UrlRedirectResult"/> instance</returns>
        public static UrlRedirectResult CreateNew(string action, string controller, string area, RouteValueDictionary values = null)
        {
            return new UrlRedirectResult(action, controller, area, values);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Removes any information containing action or controller.
        /// </summary>
        /// <param name="values">The route values</param>
        /// <returns>A new or altered <see cref="RouteValueDictionary"/></returns>
        private RouteValueDictionary RemoveRouteValues(RouteValueDictionary values)
        {
            if (values == null)
            {
                return new RouteValueDictionary();
            }
            values.Remove("action");
            values.Remove("controller");
            return values;
        }

        #endregion
    }
}