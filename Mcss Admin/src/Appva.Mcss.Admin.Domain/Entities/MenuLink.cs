// <copyright file="MenuLink.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents a single menu link item in a menu list.
    /// </summary>
    public class MenuLink : Entity<MenuLink>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLink"/> class.
        /// </summary>
        /// <param name="menu">The associated menu</param>
        /// <param name="label">The link text</param>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">Optional area route</param>
        /// <param name="sort">Optional link sorting order</param>
        /// <param name="listItemCssClass">Optional <c>{LI}</c> css class</param>
        /// <param name="anchorCssClass">Optional <c>{A}</c> css class</param>
        /// <param name="parent">Optional parent</param>
        /// <param name="permission">Optional permission</param>
        public MenuLink(Menu menu, string label, string action, string controller, string area = null, int sort = 0, string listItemCssClass = null, string anchorCssClass = null, MenuLink parent = null, Permission permission = null)
        {
            this.Menu = menu;
            this.Label = label;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.Sort = sort;
            this.ListItemCssClass = listItemCssClass;
            this.AnchorCssClass = anchorCssClass;
            this.Parent = parent;
            this.Permission = permission;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLink"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected MenuLink()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The menu item text, e.g. "The road to no where".
        /// </summary>
        public virtual string Label
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item action route.
        /// </summary>
        public virtual string Action
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item controller route.
        /// </summary>
        public virtual string Controller
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item area route.
        /// </summary>
        public virtual string Area
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item sort order.
        /// </summary>
        public virtual int Sort
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item css class, set on the 'li' element.
        /// </summary>
        public virtual string ListItemCssClass
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item css class, set on the 'a' element.
        /// </summary>
        public virtual string AnchorCssClass
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item menu.
        /// </summary>
        public virtual Menu Menu
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item parent link if any.
        /// </summary>
        public virtual MenuLink Parent
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu item permission if any.
        /// </summary>
        public virtual Permission Permission
        {
            get;
            protected set;
        }

        #endregion
    }
}