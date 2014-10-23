// <copyright file="Client.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Core.Extensions;
    using Cryptography;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// An application making protected resource requests on behalf of the
    /// resource owner and with its authorization.  The term "client" does
    /// not imply any particular implementation characteristics (e.g.,
    /// whether the application executes on a server, a desktop, or other
    /// devices).
    /// </summary>
    public class Client : Entity<Client>, IAggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="imageFileName"></param>
        /// <param name="imageMimeType"></param>
        /// <param name="accessTokenLifetime"></param>
        /// <param name="refreshTokenLifetime"></param>
        /// <param name="redirectionEndpoint"></param>
        /// <param name="isPublic"></param>
        /// <param name="authorizationGrants"></param>
        /// <param name="tenants"></param>
        /// <param name="scopes"></param>
        public Client(string name, string description, string imageFileName, string imageMimeType, int accessTokenLifetime, int refreshTokenLifetime, string redirectionEndpoint, bool isPublic, IList<AuthorizationGrant> authorizationGrants, IList<Tenant> tenants, IList<Scope> scopes)
            : this(name, description, new Image(imageFileName, imageMimeType), null, null, null, accessTokenLifetime, refreshTokenLifetime, redirectionEndpoint, (isPublic) ? ClientType.Public : ClientType.Confidential, authorizationGrants, tenants, scopes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="image"></param>
        /// <param name="identifier"></param>
        /// <param name="secret"></param>
        /// <param name="password"></param>
        /// <param name="accessTokenLifetime"></param>
        /// <param name="refreshTokenLifetime"></param>
        /// <param name="redirectionEndpoint"></param>
        /// <param name="type"></param>
        /// <param name="authorizationGrants"></param>
        /// <param name="tenants"></param>
        /// <param name="scopes"></param>
        public Client(string name, string description, Image image, string identifier, string secret, string password, int accessTokenLifetime, int refreshTokenLifetime, string redirectionEndpoint, ClientType type, IList<AuthorizationGrant> authorizationGrants, IList<Tenant> tenants, IList<Scope> scopes)
        {
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Slug = new Slug(name);
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.RedirectionEndpoint = redirectionEndpoint;
            this.Identifier = identifier ?? Hash.Random(16).ToUrlSafeBase64();
            this.Secret = secret ?? Hash.Random(16).ToUrlSafeBase64();
            this.Password = password ?? Hash.Random(32).ToUrlSafeBase64();
            this.AccessTokenLifetime = accessTokenLifetime;
            this.RefreshTokenLifetime = refreshTokenLifetime;
            this.Type = type;
            this.AuthorizationGrants = authorizationGrants;
            this.Tenants = tenants;
            this.Scopes = scopes;
            this.RegisterEvent(new ClientCreated(this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Client()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether the client is active or not.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client created at date time.
        /// </summary>
        public virtual DateTime CreatedAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client last updated at date time.
        /// </summary>
        public virtual DateTime UpdatedAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client slug.
        /// </summary>
        public virtual Slug Slug
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client name.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client description.
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client logotype if any.
        /// </summary>
        public virtual Image Image
        {
            get;
            protected set;
        }

        /// <summary>
        /// A client public identifier. The authorization server issues the registered client 
        /// a client identifier -- a unique string representing the registration
        /// information provided by the client.  The client identifier is not a
        /// secret; it is exposed to the resource owner and MUST NOT be used
        /// alone for client authentication.  The client identifier is unique to
        /// the authorization server.
        /// The syntax is {Id}.{generated value}.
        /// </summary>
        public virtual string Identifier
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client secret. Users are authenticated, while apps are authorized (the app is 
        /// allowed to use or access the resources). The client secret protects a service from 
        /// given out tokens to rogue apps.
        /// </summary>
        public virtual string Secret
        {
            get;
            protected set;
        }

        /// <summary>
        /// Client credentials, which entitles the client in possession of a client password to 
        /// use the HTTP Basic authentication scheme as defined in [RFC2617] to authenticate with
        /// the authorization server.
        /// </summary>
        public virtual string Password
        {
            get;
            protected set;
        }

        /// <summary>
        /// The access token lifetime in minutes.
        /// Zero (0) for no expiration.
        /// </summary>
        public virtual int AccessTokenLifetime
        {
            get;
            protected set;
        }

        /// <summary>
        /// The refresh token lifetime in minutes.
        /// Zero (0) for no expiration.
        /// </summary>
        public virtual int RefreshTokenLifetime
        {
            get;
            protected set;
        }

        /// <summary>
        /// The redirection endpoint (i.e. application callback).
        /// After completing its interaction with the resource owner, the
        /// authorization server directs the resource owner's user-agent back to
        /// the client.  The authorization server redirects the user-agent to the
        /// client's redirection endpoint previously established with the
        /// authorization server during the client registration process or when
        /// making the authorization request..
        /// </summary>
        public virtual string RedirectionEndpoint
        {
            get;
            protected set;
        }

        /// <summary>
        /// The clients ability to authenticate securely with the authorization 
        /// server (i.e., ability to maintain the confidentiality of their client credentials).
        /// </summary>
        public virtual ClientType Type
        {
            get;
            protected set;
        }

        /// <summary>
        /// Credential representing the resource owner's authorization (to access its protected 
        /// resources) used by the client to obtain an access token.
        /// </summary>
        public virtual IList<AuthorizationGrant> AuthorizationGrants
        {
            get;
            protected set;
        }

        /// <summary>
        /// Whether or not the client is connected to multiple tenants.
        /// </summary>
        public virtual bool IsGlobal
        {
            get;
            protected set;
        }

        /// <summary>
        /// A single client can be connected to multiple <see cref="Tenant"/>.
        /// </summary>
        public virtual IList<Tenant> Tenants
        {
            get;
            protected set;
        }

        /// <summary>
        /// A single client can be connected to multiple <see cref="Scope"/>.
        /// </summary>
        public virtual IList<Scope> Scopes
        {
            get;
            protected set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Activates the client.
        /// </summary>
        public virtual Client Activate()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        /// Inactivates the client.
        /// </summary>
        public virtual Client Inactivate()
        {
            this.IsActive = false;
            return this;
        }

        #endregion
    }
}