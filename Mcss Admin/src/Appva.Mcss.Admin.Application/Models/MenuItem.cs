// <copyright file="MenuItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using Appva.Core.Contracts.Permissions;

    #endregion

    /// <summary>
    /// Represents a menu item, a.k.a a menu link.
    /// </summary>
    public interface IMenuItem
    {
        #region Properties

        /// <summary>
        /// The menu item text, e.g. "The road to no where".
        /// </summary>
        string Label
        {
            get;
        }

        /// <summary>
        /// The menu item action route.
        /// </summary>
        string Action
        {
            get;
        }

        /// <summary>
        /// The menu item controller route.
        /// </summary>
        string Controller
        {
            get;
        }

        /// <summary>
        /// The menu item area route.
        /// </summary>
        string Area
        {
            get;
        }

        /// <summary>
        /// The menu item css class, set on the 'li' element.
        /// </summary>
        string ListItemCssClass
        {
            get;
        }

        /// <summary>
        /// The menu item css class, set on the 'a' element.
        /// </summary>
        string AnchorCssClass
        {
            get;
        }

        /// <summary>
        /// The menu item permission resource/key if any.
        /// </summary>
        IPermission Permission
        {
            get;
        }

        /// <summary>
        /// Menu item children.
        /// </summary>
        IList<IMenuItem> Children
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Whether or not the menu item is selected.
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="controller">The controller</param>
        /// <param name="area">The area</param>
        /// <returns>bool</returns>
        bool IsSelected(string action, string controller, string area);

        #endregion
    }

    /// <summary>
    /// A <see cref="IMenuItem"/> implementation.
    /// </summary>
    public sealed class MenuItem : IMenuItem
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        /// <param name="label">The link label</param>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">The area route</param>
        /// <param name="isSelected">Whether or not the menu item is selected</param>
        /// <param name="listItemCssClass"><c>{LI}</c> css class</param>
        /// <param name="anchorCssClass"><c>{A}</c> css class</param>
        /// <param name="children">A collection of child items</param>
        public MenuItem(string label, string action, string controller, string area, string listItemCssClass, string anchorCssClass, IPermission permission, IList<IMenuItem> children)
        {
            this.Label = label;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.ListItemCssClass = listItemCssClass;
            this.AnchorCssClass = anchorCssClass;
            this.Permission = permission;
            this.Children = children;
        }

        #endregion

        #region Public Properties.


        /// <inheritdoc />
        public string Label
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Action
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Controller
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Area
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string ListItemCssClass
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string AnchorCssClass
        {
            get;
            private set;
        }

        /// <summary>
        /// The menu item permission resource/key if any.
        /// </summary>
        public IPermission Permission
        {
            get;
            private set;
        }

        /// <summary>
        /// The menu item child collection.
        /// </summary>
        public IList<IMenuItem> Children
        {
            get;
            private set;
        }

        #endregion

        #region IMenuItem implementation.

        /// <inheritdoc />
        public bool IsSelected(string action, string controller, string area)
        {
            if (this.Children != null)
            {
                foreach (var c in this.Children)
                {
                    return c.IsSelected(action, controller, area);
                }
            }
            return this.Action.ToNullSafeLower() == action.ToNullSafeLower()
                && this.Controller.ToNullSafeLower() == controller.ToNullSafeLower()
                && this.Area.ToNullSafeLower() == area.ToNullSafeLower();
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> struct.
        /// </summary>
        /// <param name="label">The link label</param>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">The area route</param>
        /// <param name="isSelected">Whether or not the menu item is selected</param>
        /// <param name="listItemCssClass"><c>{LI}</c> css class</param>
        /// <param name="anchorCssClass"><c>{A}</c> css class</param>
        /// <param name="children">A collection of child items</param>
        /// <returns>A new <see cref="MenuItem"/> instance</returns>
        public static MenuItem CreateNew(string label, string action, string controller, string area, string listItemCssClass, string anchorCssClass, IPermission permission, IList<IMenuItem> children = null)
        {
            children = children == null ? new List<IMenuItem>() : children;
            return new MenuItem(label, action, controller, area, listItemCssClass, anchorCssClass, permission, children);
        }

        #endregion
    }
}