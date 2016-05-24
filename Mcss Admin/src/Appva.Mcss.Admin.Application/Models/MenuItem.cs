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
    public interface IMenuItem : ICloneable
    {
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
        /// Whether or not the menu item is selected.
        /// </summary>
        bool IsSelected
        {
            get;
            set;
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
            set;
        }
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
        /// <param name="id">The link id</param>
        /// <param name="label">The link label</param>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">The area route</param>
        /// <param name="isSelected">Whether or not the menu item is selected</param>
        /// <param name="listItemCssClass">Optional <c>{LI}</c> css class</param>
        /// <param name="anchorCssClass">Optional <c>{A}</c> css class</param>
        /// <param name="sort">Optional link sorting</param>
        /// <param name="parentId">Optional parent link id, if any</param>
        /// <param name="permission">Option permission, if any</param>
        public MenuItem(Guid id, string label, string action, string controller, string area, bool isSelected, string listItemCssClass = null, string anchorCssClass = null, int sort = 0, Guid? parentId = null, IPermission permission = null)
        {
            this.Id = id;
            this.Label = label;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.IsSelected = isSelected;
            this.ListItemCssClass = listItemCssClass;
            this.AnchorCssClass = anchorCssClass;
            this.Sort = sort;
            this.ParentId = parentId;
            this.Permission = permission;
        }

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
        /*public MenuItem(string label, string action, string controller, string area, bool isSelected, string listItemCssClass, string anchorCssClass, IList<IMenuItem> children)
        {
            this.Label = label;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.IsSelected = isSelected;
            this.ListItemCssClass = listItemCssClass;
            this.AnchorCssClass = anchorCssClass;
            this.Children = children;
        }*/

        #endregion

        #region Public Properties.

        /// <summary>
        /// The menu item id.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }

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
        /// Whether or not the menu item is selected.
        /// </summary>
        public bool IsSelected
        {
            get;
            set;
        }

        /// <summary>
        /// The menu item order, e.g. 1, 2, 3 ... 100.
        /// </summary>
        public int Sort
        {
            get;
            private set;
        }

        /// <summary>
        /// The menu item parent link if any.
        /// </summary>
        public Guid? ParentId
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
            set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        /// <param name="id">The link id</param>
        /// <param name="label">The link label</param>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">The area route</param>
        /// <param name="isSelected">Whether or not the menu item is selected</param>
        /// <param name="listItemCssClass">Optional <c>{LI}</c> css class</param>
        /// <param name="anchorCssClass">Optional <c>{A}</c> css class</param>
        /// <param name="sort">Optional link sorting</param>
        /// <param name="parentId">Optional parent link id, if any</param>
        /// <param name="permission">Option permission, if any</param>
        /// <returns>A new <see cref="MenuItem"/> instance</returns>
        /*public static MenuItem CreateNew(Guid id, string label, string action, string controller, string area, bool isSelected, string listItemCssClass = null, string anchorCssClass = null, int sort = 0, Guid? parentId = null, IPermission permission = null)
        {
            return new MenuItem(id, label, action, controller, area, isSelected, listItemCssClass, anchorCssClass, sort, parentId, permission);
        }*/

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
        /*public static MenuItem CreateNew(string label, string action, string controller, string area, bool isSelected, string listItemCssClass, string anchorCssClass, IList<IMenuItem> children)
        {
            return new MenuItem(label, action, controller, area, isSelected, listItemCssClass, anchorCssClass, children);
        }*/

        /// <summary>
        /// Returns whether or not a menu item is selected.
        /// </summary>
        /// <param name="item">The <see cref="MenuItem"/> to check</param>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">The area route</param>
        /// <returns>True if the item is selected</returns>
        public static bool IsSelectedItem(IMenuItem item, string action, string controller, string area)
        {
            return item.Action.ToNullSafeLower() == action.ToNullSafeLower()
                && item.Controller.ToNullSafeLower() == controller.ToNullSafeLower()
                && item.Area.ToNullSafeLower() == area.ToNullSafeLower();
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Sets the current menu item as selected.
        /// </summary>
        public void MarkAsSelected()
        {
            this.IsSelected = true;
        }

        #endregion

        #region ICloneable members

        /// <inheritdoc />
        public object Clone()
        {
            return new MenuItem(this.Label, this.Action, this.Controller, this.Area, this.ListItemCssClass, this.AnchorCssClass, this.Permission, this.Children);
        }

        #endregion
    }
}