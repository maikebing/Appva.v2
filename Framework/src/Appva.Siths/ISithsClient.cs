// <copyright file="ISithsClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Http;

    #endregion

    /// <summary>
    /// Authify SITHS client.
    /// </summary>
    public interface IAuthifyClient
    {
        /// <summary>
        /// Creates a valid external <c>Uri</c>.
        /// </summary>
        /// <returns>An external redirect authentication <c>Uri</c></returns>
        Uri ExternalLogin();


    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AuthifyClient : IAuthifyClient
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthifyClient"/> class.
        /// </summary>
        public AuthifyClient()
        {
        }

        #endregion
    }

    public sealed class AuthifyClientConfiguration
    {

    }
}