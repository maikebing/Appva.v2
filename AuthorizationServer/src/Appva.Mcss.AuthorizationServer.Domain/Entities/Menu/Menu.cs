// <copyright file="Menu.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents a menu container.
    /// </summary>
    public class Menu : Entity<Menu>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="key">The menu key</param>
        /// <param name="name">The friendly name</param>
        /// <param name="description">The description</param>
        public Menu(string key, string name, string description)
        {
            this.Key = key;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Menu()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The menu key. 
        /// E.g. top_header_menu, side_bar_menu.
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu friendly name.
        /// E.g. "Top header menu"
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The menu description.
        /// E.g. "Top header menu for non access policy links".
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The set of menu link items.
        /// </summary>
        public virtual IList<MenuLink> MenuLinks
        {
            get;
            protected set;
        }

        #endregion
    }
}