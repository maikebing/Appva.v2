// <copyright file="Role.cs" company="Appva AB">
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
    /// Role assignment: A subject can exercise a permission only if the subject has 
    /// selected or been assigned a role.
    /// See <a href="http://en.wikipedia.org/wiki/Role-based_access_control">RBAC</a>
    /// </summary>
    public class Role : Entity<Role>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="key">The role key</param>
        /// <param name="name">The friendly role name</param>
        /// <param name="description">The role description</param>
        /// <param name="permissions">The role permissions</param>
        /// <param name="parent">Optional parent role</param>
        public Role(string key, string name, string description, ICollection<Permission> permissions, Role parent = null)
        {
            this.Key = key;
            this.Name = name;
            this.Description = description;
            this.Permissions = permissions;
            this.Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Role()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The role key.
        /// E.g. superuser, management_user.
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// The role name.
        /// E.g. "Super User".
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The role description.
        /// E.g. "A subject assigned this role may do whatever
        /// it wants."
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The role parent if any.
        /// Use parents to construct role hierarchies which
        /// will inherit permissions.
        /// </summary>
        public virtual Role Parent
        {
            get;
            protected set;
        }

        /// <summary>
        /// The set of permissions assigned this role.
        /// </summary>
        public virtual ICollection<Permission> Permissions
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users assigned to the role.
        /// </summary>
        public virtual ICollection<User> Users
        {
            get;
            protected set;
        }

        #endregion
    }
}