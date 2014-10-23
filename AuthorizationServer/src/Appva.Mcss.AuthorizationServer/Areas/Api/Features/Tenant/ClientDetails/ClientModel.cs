// <copyright file="ClientModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Api.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Models;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class ClientModel : IdentityAndSlug
    {
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
        /// The client logotype.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}