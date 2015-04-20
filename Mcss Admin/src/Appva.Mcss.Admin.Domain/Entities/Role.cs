// <copyright file="Role.cs" company="Appva AB">
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
    public class Role : AggregateRoot<Role>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="Role"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

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

        #endregion
    }
}