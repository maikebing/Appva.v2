// <copyright file="Permission.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Permission authorization: A subject can exercise a permission only if the permission is 
    /// authorized for the subject's active role. With rules 1 and 2, this rule ensures that 
    /// users can exercise only permissions for which they are authorized.
    /// See <a href="http://en.wikipedia.org/wiki/Role-based_access_control">RBAC</a>.
    /// Combined, the resource, action, context is unique.
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
        public Permission(string name, string description, string resource, PermissionAction action, PermissionContext permissionContext)
        {
            this.Name = name;
            this.Description = description;
            this.Resource = resource;
            this.Action = action;
            this.Context = permissionContext;
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
        /// The action for the resource, e.g. view.
        /// </summary>
        public virtual PermissionAction Action
        {
            get;
            protected set;
        }

        /// <summary>
        /// The permission policy context.
        /// </summary>
        public virtual PermissionContext Context
        {
            get;
            protected set;
        }

        #endregion
    }
}