// <copyright file="CommonClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Cqrs;
    using Appva.Mvc.Html.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CommonClient : Idg
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
        /// The client redirection endpoint, e.g.
        /// a callback URL.
        /// </summary>
        public string RedirectionEndpoint
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the client is public or
        /// confidential.
        /// </summary>
        public Tickable IsConfidential
        {
            get;
            set;
        }

        /// <summary>
        /// The client authorization grants.
        /// </summary>
        public IList<Tickable> AuthorizationGrants
        {
            get;
            set;
        }

        /// <summary>
        /// The client scopes.
        /// </summary>
        public IList<Tickable> Scopes
        {
            get;
            set;
        }

        /// <summary>
        /// The client tenants.
        /// </summary>
        public IList<Tickable> Tenants
        {
            get;
            set;
        }
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The response class</typeparam>
    public class CommonClient<T> : CommonClient, IRequest<T> where T : class
    {
    }
}