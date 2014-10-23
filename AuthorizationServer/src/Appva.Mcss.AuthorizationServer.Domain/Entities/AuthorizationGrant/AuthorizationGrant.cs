// <copyright file="AuthorizationGrant.cs" company="Appva AB">
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
    /// An authorization grant is a credential representing the resource
    /// owner's authorization (to access its protected resources) used by the
    /// client to obtain an access token.  This specification defines four
    /// grant types -- authorization code, implicit, resource owner password
    /// credentials, and client credentials -- as well as an extensibility
    /// mechanism for defining additional types.
    /// </summary>
    public class AuthorizationGrant : Entity<Client>
    {
        #region Constants.

        /// <summary>
        /// Authorization code grant key.
        /// </summary>
        public const string AuthorizationCode = "AuthorizationCode";

        /// <summary>
        /// Implicit grant key.
        /// </summary>
        public const string Implicit = "Implicit";

        /// <summary>
        /// Resource owner password credentials grant key.
        /// </summary>
        public const string ResourceOwnerPasswordCredentials = "ResourceOwnerPasswordCredentials";

        /// <summary>
        /// Client credentials grant key.
        /// </summary>
        public const string ClientCredentials = "ClientCredentials";
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationGrant"/> class.
        /// </summary>
        /// <param name="key">The grant key</param>
        /// <param name="description">The grant description</param>
        public AuthorizationGrant(string key, string description)
        {
            this.Key = key;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationGrant"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected AuthorizationGrant()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The authorization grant key. Oauth2 supports four grants:
        /// 
        /// AuthorizationCode
        /// The authorization code is obtained by using an authorization server
        /// as an intermediary between the client and resource owner.  Instead of
        /// requesting authorization directly from the resource owner, the client
        /// directs the resource owner to an authorization server (via its
        /// user-agent as defined in [RFC2616]), which in turn directs the
        /// resource owner back to the client with the authorization code.
        /// 
        /// Implicit
        /// The implicit grant is a simplified authorization code flow optimized 
        /// for clients implemented in a browser using a scripting language such
        /// as JavaScript.  In the implicit flow, instead of issuing the client
        /// an authorization code, the client is issued an access token directly
        /// (as the result of the resource owner authorization).  The grant type
        /// is implicit, as no intermediate credentials (such as an authorization
        /// code) are issued (and later used to obtain an access token).
        /// 
        /// ResourceOwnerPasswordCredentials
        /// The resource owner password credentials (i.e., username and password)
        /// can be used directly as an authorization grant to obtain an access
        /// token. The credentials should only be used when there is a high
        /// degree of trust between the resource owner and the client (e.g., the
        /// client is part of the device operating system or a highly privileged
        /// application), and when other authorization grant types are not
        /// available (such as an authorization code).
        /// 
        /// ClientCredentials
        /// The client credentials (or other forms of client authentication) can
        /// be used as an authorization grant when the authorization scope is
        /// limited to the protected resources under the control of the client,
        /// or to protected resources previously arranged with the authorization
        /// server.  Client credentials are used as an authorization grant
        /// typically when the client is acting on its own behalf (the client is
        /// also the resource owner) or is requesting access to protected
        /// resources based on an authorization previously arranged with the
        /// authorization server.
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// A description of the grant.
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        #endregion
    }
}