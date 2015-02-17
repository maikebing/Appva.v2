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
    public class Tenant : AggregateRoot<Tenant>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="identifier">A unique identifier, e.g. a certificate thumbprint or serial
        /// number, a text string etc</param>
        /// <param name="hostName">A unique user friendly host name which may be used to
        /// identify a tenant by host name</param>
        /// <param name="name">The tenant name</param>
        /// <param name="description">The tenant description</param>
        /// <param name="fileName">The tenant logotype file path</param>
        /// <param name="mimeType">The tenant logotype mime type</param>
        /// <param name="connectionString">The tenant database connection string</param>
        /// <param name="tags">The tenant tags</param>
        public Tenant(string identifier, string hostName, string name, string description, string fileName, string mimeType, string connectionString, IList<Tag> tags = null)
            : this(identifier, hostName, name, description, new Image(fileName, mimeType), new DatabaseConnection(connectionString), tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="identifier">A unique identifier, e.g. a certificate thumbprint or serial
        /// number, a text string etc.</param>
        /// <param name="hostName">A unique user friendly host name which may be used to
        /// identify a tenant by host name</param>
        /// <param name="name">The tenant name</param>
        /// <param name="description">The tenant description</param>
        /// <param name="image">The tenant logotype</param>
        /// <param name="databaseConnection">The tenant database connection</param>
        /// <param name="tags">The tenant tags</param>
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

        public virtual void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || this.Name.Equals(name))
            {
                return;
            }
            this.Name = name;
            this.Slug = new Slug(name);
        }

        public virtual void UpdateIdentifier(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier) || this.Identifier.Equals(identifier))
            {
                return;
            }
            this.Identifier = identifier;
        }

        /// <summary>
        /// Updates the <see cref="Tenant"/> instance.
        /// </summary>
        /// <param name="identifier">A unique identifier, e.g. a certificate thumbprint or serial
        /// number, a text string etc</param>
        /// <param name="hostName">A unique user friendly host name which may be used to
        /// identify a tenant by host name</param>
        /// <param name="name">The tenant name</param>
        /// <param name="description">The tenant description</param>
        /// <param name="fileName">The tenant logotype file path</param>
        /// <param name="mimeType">The tenant logotype mime type</param>
        /// <param name="connectionString">The tenant database connection string</param>
        /// <param name="tags">The tenant tags</param>
        public virtual void Update(string identifier, string hostName, string name, string description, string fileName, string mimeType, string connectionString, IList<Tag> tags = null)
        {
            this.Update(identifier, hostName, name, description, new Image(fileName, mimeType), new DatabaseConnection(connectionString), tags);
        }

        /// <summary>
        /// Updates the <see cref="Tenant"/> instance.
        /// </summary>
        /// <param name="identifier">A unique identifier, e.g. a certificate thumbprint or serial
        /// number, a text string etc</param>
        /// <param name="hostName">A unique user friendly host name which may be used to
        /// identify a tenant by host name</param>
        /// <param name="name">The tenant name</param>
        /// <param name="description">The tenant description</param>
        /// <param name="image">The tenant logotype</param>
        /// <param name="databaseConnection">The tenant database connection</param>
        /// <param name="tags">The tenant tags</param>
        public virtual void Update(string identifier, string hostName, string name, string description, Image image, DatabaseConnection databaseConnection, IList<Tag> tags = null)
        {
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
        

        #endregion
    }
}