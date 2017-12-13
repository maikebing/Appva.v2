// <copyright file="Tag.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    public sealed class RouteKey
    {
        #region Variables.

        private readonly string value;

        #endregion
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteKey"/> class.
        /// </summary>
        public RouteKey(string value)
        {
            this.value = value;
        }

        #endregion

        public string Value
        {
            get
            {
                return this.value;
            }
        }
        public static RouteKey New(string value)
        {
            return new RouteKey(value);
        }
        public static RouteKey New(string areaName, string controllerName, string actionName)
        {
            var area       = string.IsNullOrWhiteSpace(areaName) ? "*" : areaName;
            var controller = string.IsNullOrWhiteSpace(controllerName) ? "*" : controllerName;
            var action     = string.IsNullOrWhiteSpace(actionName) ? "*" : actionName;
            return new RouteKey(string.Join(".", area, controller, action));
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Tag
    {
        #region Variables.

        private readonly string name;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="tag"/> is empty ("") or contains only whitespace.
        /// </exception>
        public Tag(string name)
        {
            this.name = name;
        }

        #endregion

        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public static Tag New(string name)
        {
            return new Tag(name);
        }



    }
}