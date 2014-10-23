// <copyright file="PermissionContext.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// A context.
    /// </summary>
    public class PermissionContext : Entity<PermissionContext>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionContext"/> class.
        /// </summary>
        /// <param name="key">The context key</param>
        /// <param name="name">The name of the context</param>
        /// <param name="description">The description of the context</param>
        public PermissionContext(string key, string name, string description)
        {
            this.Key = key;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionContext"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected PermissionContext()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The permission context key. 
        /// E.g. oauth, web, device.
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// The friendly context name, e.g. OAuth context.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        #endregion
    }
}