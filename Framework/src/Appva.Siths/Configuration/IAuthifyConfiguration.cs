// <copyright file="IAuthifyConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Configuration
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// The Authify configuration settings.
    /// </summary>
    public interface IAuthifyConfiguration
    {
        /// <summary>
        /// Returns the Authify API server address.
        /// </summary>
        Uri ServerAddress
        {
            get;
        }

        /// <summary>
        /// Returns the Authify API key.
        /// </summary>
        string Key
        {
            get;
        }

        /// <summary>
        /// Returns the Authify API secret.
        /// </summary>
        string Secret
        {
            get;
        }

        /// <summary>
        /// Returns the Authify redirect URL.
        /// </summary>
        Uri RedirectUrl
        {
            get;
        }

        /// <summary>
        /// Returns the Authify IdP.
        /// </summary>
        string IdentityProvider
        {
            get;
        }

        /// <summary>
        /// Return the Authify reseller ID.
        /// </summary>
        string ResellerId
        {
            get;
        }
    }
}