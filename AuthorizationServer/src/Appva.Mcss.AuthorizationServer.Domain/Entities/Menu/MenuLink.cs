// <copyright file="MenuLink.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

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
        /// <param name="label">The link label text</param>
        /// <param name="url">The relative link url</param>
        /// <param name="sort">The link order</param>
        /// <param name="cssClass">The CSS class</param>
        /// <param name="menu">The associated menu</param>
        /// <param name="parent">Optional parent</param>
        /// <param name="permission">Optional permission</param>
        public MenuLink(string label, string url, int sort, string cssClass, Menu menu, MenuLink parent = null, Permission permission = null)
        {
            this.Label = label;
            this.Url = url;
            this.Sort = sort;
            this.CssClass = cssClass;
            this.Menu = menu;
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
        /// The menu link label.
        /// E.g. "The road to no where"
        /// </summary>
        public virtual string Label
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu link URL.
        /// E.g. /controller/action
        /// </summary>
        public virtual string Url
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu link order.
        /// E.g. 1, 2, 3 ... 100.
        /// </summary>
        public virtual int Sort
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu link CSS class, set on the LI element.
        /// E.g. "special-menu-link-item".
        /// </summary>
        public virtual string CssClass
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu link items' menu.
        /// </summary>
        public virtual Menu Menu
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu link parent link if any.
        /// </summary>
        public virtual MenuLink Parent
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu link permission if any.
        /// </summary>
        public virtual Permission Permission
        {
            get;
            protected set;
        }

        #endregion
    }
}