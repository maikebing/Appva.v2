// <copyright file="Role.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
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
    public class Role : AggregateRoot<Role>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Sorting property.
        /// </summary>
        public virtual int Weight
        {
            get;
            set;
        }

        /// <summary>
        /// The system name used by MSCC.
        /// </summary>
        public virtual string MachineName
        {
            get;
            set;
        }

        /// <summary>
        /// Property telling if the Role is Deletabel
        /// </summary>
        public virtual bool IsDeletable
        {
            get;
            set;
        }

        /// <summary>
        /// Property telling if the Role is visible
        /// </summary>
        public virtual bool IsVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Accounts within this role.
        /// </summary>
        public virtual IList<Account> Accounts
        {
            get;
            set;
        }

        /// <summary>
        /// List of <see cref="Setting"/> defining permissions for this role
        /// </summary>
        public virtual IList<Permission> Permissions
        {
            get;
            set;
        }

        public virtual IList<ScheduleSettings> ScheduleSettings
        {
            get;
            set;
        }

        /// <summary>
        /// The roles the role can access.
        /// </summary>
        public virtual IList<Role> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// The delegations the role can access.
        /// </summary>
        public virtual IList<Taxon> Delegations
        {
            get;
            set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="key">The unique internal machine key</param>
        /// <param name="name">The public role name</param>
        /// <param name="description">The role description</param>
        /// <param name="sort">The role sort order, defaults to 0 (highest)</param>
        /// <param name="isVisible">Optional public visibility of the role, defaults to true</param>
        /// <param name="isDeletable">Optional public deletability of the roles, defaults to true</param>
        /// <returns>A new <see cref="Role"/> instance</returns>
        public static Role CreateNew(string key, string name, string description, int sort = 0, bool isVisible = true, bool isDeletable = true)
        {
            return new Role { MachineName = key, Name = name, Description = description, Weight = sort, IsVisible = isVisible, IsDeletable = isDeletable };
        }

        #endregion
    }
}