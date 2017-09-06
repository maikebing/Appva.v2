// <copyright file="MobileGrandIdClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System;
    using Appva.GrandId;
    using Appva.Http;

    #endregion

    /// <summary>
    /// The mobile Grand ID client.
    /// </summary>
    public interface IMobileGrandIdClient : IGrandIdClient
    {
    }

    /// <summary>
    /// The <see cref="IMobileGrandIdClient"/> implementation.
    /// </summary>
    public sealed class MobileGrandIdClient : GrandIdClient, IMobileGrandIdClient
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileGrandIdClient"/> class.
        /// </summary>
        /// <param name="options">The rest options.</param>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="credentials">The grand id credentials.</param>
        /// <param name="handler">Optional http message handler.</param>
        public MobileGrandIdClient(IRestOptions options, Uri baseAddress, GrandIdCredentials credentials)
            : base(options, baseAddress, credentials)
        {
        }

        #endregion
    }
}