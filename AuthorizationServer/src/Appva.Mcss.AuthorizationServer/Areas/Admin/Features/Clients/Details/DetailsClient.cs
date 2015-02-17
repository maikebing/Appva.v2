// <copyright file="DetailsClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DetailsClient : Idg
    {
        /// <summary>
        /// Whether the client is active or not.
        /// </summary>
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The client meta data.
        /// </summary>
        public MetaData Resource
        {
            get;
            set;
        }

        /// <summary>
        /// The client name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The client description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The client avatar.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
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
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret. Users are authenticated, while apps are authorized (the app is 
        /// allowed to use or access the resources). The client secret protects a service from 
        /// given out tokens to rogue apps.
        /// </summary>
        public string Secret
        {
            get;
            set;
        }

        /// <summary>
        /// Client credentials, which entitles the client in possession of a client password to 
        /// use the HTTP Basic authentication scheme as defined in [RFC2617] to authenticate with
        /// the authorization server.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The access token lifetime in minutes.
        /// </summary>
        public int AccessTokenLifetime
        {
            get;
            set;
        }

        /// <summary>
        /// The refresh token lifetime in minutes.
        /// </summary>
        public int RefreshTokenLifetime
        {
            get;
            set;
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
        public string RedirectionEndpoint
        {
            get;
            set;
        }

        /// <summary>
        /// The clients ability to authenticate securely with the authorization 
        /// server (i.e., ability to maintain the confidentiality of their client credentials).
        /// </summary>
        public bool IsPublic
        {
            get;
            set;
        }

        /// <summary>
        /// Credential representing the resource owner's authorization (to access its protected 
        /// resources) used by the client to obtain an access token.
        /// </summary>
        public AuthorizationGrantType AuthorizationGrant
        {
            get;
            set;
        }

    }
}