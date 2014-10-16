// <copyright file="OAuth2Client.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.OAuth
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OAuth2;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class OAuth2Client : IClientDescription
    {
        #region Variables.

        /// <summary>
        /// A client public identifier. The authorization server issues the registered client 
        /// a client identifier -- a unique string representing the registration
        /// information provided by the client.  The client identifier is not a
        /// secret; it is exposed to the resource owner and MUST NOT be used
        /// alone for client authentication.  The client identifier is unique to
        /// the authorization server.
        /// The syntax is {Id}.{generated value}.
        /// </summary>
        private readonly string identifier;

        /// <summary>
        /// The client name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The client secret. Users are authenticated, while apps are authorized (the app is 
        /// allowed to use or access the resources). The client secret protects a service from 
        /// given out tokens to rogue apps.
        /// </summary>
        private readonly string secret;

        /// <summary>
        /// The redirection endpoint (i.e. application callback).
        /// After completing its interaction with the resource owner, the
        /// authorization server directs the resource owner's user-agent back to
        /// the client.  The authorization server redirects the user-agent to the
        /// client's redirection endpoint previously established with the
        /// authorization server during the client registration process or when
        /// making the authorization request..
        /// </summary>
        private readonly Uri callback;

        /// <summary>
        /// The client scopes.
        /// </summary>
        private readonly HashSet<string> scopes;

        /// <summary>
        /// The clients ability to authenticate securely with the authorization 
        /// server (i.e., ability to maintain the confidentiality of their client credentials).
        /// </summary>
        private readonly ClientType clientType;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2Client"/> class.
        /// </summary>
        /// <param name="identifier">The client identifier</param>
        /// <param name="name">The client name</param>
        /// <param name="secret">The client secret</param>
        /// <param name="callback">The client callback</param>
        /// <param name="scopes">The client scopes</param>
        /// <param name="clientType">The client type</param>
        public OAuth2Client(string identifier, string name, string secret, string callback, HashSet<string> scopes, int clientType)
        {
            this.identifier = identifier;
            this.name = name;
            this.secret = secret;
            this.callback = callback.IsEmpty() ? null : new Uri(callback);
            this.scopes = scopes;
            this.clientType = (ClientType) clientType;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the client identifier.
        /// </summary>
        public string Identifier
        {
            get
            {
                return this.identifier;
            }
        }

        /// <summary>
        /// Returns the client name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Returns the client scopes.
        /// </summary>
        public HashSet<string> Scopes
        {
            get
            {
                return this.scopes;
            }
        }

        #endregion

        #region IClientDescription Members

        /// <inheritdoc />
        public ClientType ClientType
        {
            get
            {
                return this.clientType;
            }
        }

        /// <inheritdoc />
        public Uri DefaultCallback
        {
            get
            {
                return this.callback;
            }
        }

        /// <inheritdoc />
        public bool HasNonEmptySecret
        {
            get
            {
                return this.secret.IsEmpty();
            }
        }

        /// <inheritdoc />
        public bool IsCallbackAllowed(Uri callback)
        {
            return this.callback.IsNull() || this.callback.Equals(callback);
        }

        /// <inheritdoc />
        public bool IsValidClientSecret(string secret)
        {
            return MessagingUtilities.EqualsConstantTime(secret, this.secret);
        }

        #endregion
    }
}