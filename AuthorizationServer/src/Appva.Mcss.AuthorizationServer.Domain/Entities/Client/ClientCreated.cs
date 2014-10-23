// <copyright file="ClientCreated.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ClientCreated : IDomainEvent
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCreated"/> class.
        /// </summary>
        /// <param name="client">The client created</param>
        public ClientCreated(Client client)
        {
            this.Occurred = DateTime.Now;
            this.Version = 1;
            this.IsActive = client.IsActive;
            this.CreatedAt = client.CreatedAt;
            this.UpdatedAt = client.UpdatedAt;
            this.Slug = client.Slug;
            this.Name = client.Name;
            this.Description = client.Description;
            this.Image = client.Image;
            this.RedirectionEndpoint = client.RedirectionEndpoint;
            this.Identifier = client.Identifier;
            this.Secret = client.Secret;
            this.Password = client.Password;
            this.Type = client.Type;
            //this.AuthorizationGrant = client.AuthorizationGrant;
            //this.Tenants = tenants;
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public DateTime Occurred
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public int Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether the client is active or not.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            private set;
        }

        /// <summary>
        /// The client created at date time.
        /// </summary>
        public virtual DateTime CreatedAt
        {
            get;
            private set;
        }

        /// <summary>
        /// The client last updated at date time.
        /// </summary>
        public virtual DateTime UpdatedAt
        {
            get;
            private set;
        }

        /// <summary>
        /// The client slug.
        /// </summary>
        public virtual Slug Slug
        {
            get;
            private set;
        }

        /// <summary>
        /// The client name.
        /// </summary>
        public virtual string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The client description.
        /// </summary>
        public virtual string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// The client logotype if any.
        /// </summary>
        public virtual Image Image
        {
            get;
            private set;
        }

        /// <summary>
        /// The redirection endpoint (i.e. application callback).
        /// </summary>
        public virtual string RedirectionEndpoint
        {
            get;
            private set;
        }

        /// <summary>
        /// A client public identifier. 
        /// </summary>
        public virtual string Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public virtual string Secret
        {
            get;
            private set;
        }

        /// <summary>
        /// Client credentials.
        /// </summary>
        public virtual string Password
        {
            get;
            private set;
        }

        /// <summary>
        /// The client type.
        /// </summary>
        public virtual ClientType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// The authorization grant.
        /// </summary>
        public virtual IList<Guid> AuthorizationGrants
        {
            get;
            private set;
        }

        /// <summary>
        /// A single client can be connected to multiple <see cref="Tenant"/>.
        /// </summary>
        public virtual IList<Guid> Tenants
        {
            get;
            private set;
        }

        #endregion
    }
}