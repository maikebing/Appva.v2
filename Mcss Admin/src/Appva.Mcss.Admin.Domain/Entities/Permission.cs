// <copyright file="Permission.cs" company="Appva AB">
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Permission : Entity<Permission>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="name">The permission name</param>
        /// <param name="description">The permission description</param>
        /// <param name="resource">The resource</param>
        /// <param name="action">The action</param>
        /// <param name="permissionContext">The context</param>
        public Permission(string name, string description, string resource, int sort = 0, bool isVisible = true)
        {
            this.Name = name;
            this.Description = description;
            this.Resource = resource;
            this.Sort = sort;
            this.IsVisible = isVisible;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Permission()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The permission policy name.
        /// E.g. "Create Users"
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The permission policy description.
        /// E.g. "A user assigned this permission can
        /// create users".
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The resource, e.g. user.
        /// </summary>
        public virtual string Resource
        {
            get;
            protected set;
        }

        /// <summary>
        /// The sort.
        /// </summary>
        public virtual int Sort
        {
            get;
            protected set;
        }

        /// <summary>
        /// The visibility.
        /// </summary>
        public virtual bool IsVisible
        {
            get;
            protected set;
        }

        /// <summary>
        /// The roles.
        /// </summary>
        public virtual IList<Role> Roles
        {
            get;
            protected set;
        }

        #endregion
    }
}