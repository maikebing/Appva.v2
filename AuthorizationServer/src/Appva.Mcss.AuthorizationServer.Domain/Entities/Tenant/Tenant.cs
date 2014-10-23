// <copyright file="Tenant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// A representation of a tenant, or in our bounded context more of a
    /// customer or consumer of the product.
    /// </summary>
    public class Tenant : Entity<Tenant>, IAggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="hostName"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="fileName"></param>
        /// <param name="connectionString"></param>
        /// <param name="tags"></param>
        public Tenant(string identifier, string hostName, string name, string description, string fileName, string connectionString, IList<Tag> tags = null)
            : this(identifier, hostName, name, description, new Image(fileName, null), new DatabaseConnection(connectionString), tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="hostName"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="image"></param>
        /// <param name="databaseConnection"></param>
        /// <param name="tags"></param>
        public Tenant(string identifier, string hostName, string name, string description, Image image, DatabaseConnection databaseConnection, IList<Tag> tags = null)
        {
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Slug = new Slug(name);
            this.Identifier = identifier;
            this.HostName = new Slug(hostName).Name;
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.DatabaseConnection = databaseConnection;
            this.Tags = tags;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Tenant()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether the tenant is active or not.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant created at date time.
        /// </summary>
        public virtual DateTime CreatedAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant last updated at date time.
        /// </summary>
        public virtual DateTime UpdatedAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant slug.
        /// </summary>
        public virtual Slug Slug
        {
            get;
            protected set;
        }

        /// <summary>
        /// A unique identifier, e.g. a certificate thumbprint or serial
        /// number, a text string etc.
        /// </summary>
        public virtual string Identifier
        {
            get;
            protected set;
        }

        /// <summary>
        /// A unique user friendly host name which may be used to
        /// identify a tenant by host name.
        /// </summary>
        public virtual string HostName
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant name.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant description.
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant logotype if any.
        /// </summary>
        public virtual Image Image
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant database connection.
        /// </summary>
        public virtual DatabaseConnection DatabaseConnection
        {
            get;
            protected set;
        }

        /// <summary>
        /// The tenant tags.
        /// </summary>
        public virtual IList<Tag> Tags
        {
            get;
            protected set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Activates the tenant.
        /// </summary>
        public virtual Tenant Activate()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        /// Inactivates the tenant.
        /// </summary>
        public virtual Tenant Inactivate()
        {
            this.IsActive = false;
            return this;
        }

        #endregion
    }
}