// <copyright file="Configuration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Configuration
    /// </summary>
    public class Configuration
    {
        #region Fields.

        internal Dictionary<string, string> CredList
        {
            get;
            private set;
        }

        /// <summary>
        /// Credentials.
        /// </summary>
        internal string Credentials
        {
            get;
            private set;
        }

        /// <summary>
        /// BaseAddress.
        /// </summary>
        internal Uri BaseAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has credentials.
        /// </summary>
        /// <value><c>true</c> if this instance has credentials; otherwise, <c>false</c>.</value>
        internal bool HasCredentials => this.Credentials != null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        internal Configuration(Uri baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        internal void SetCredentials(string credentials)
        {
            this.Credentials = credentials;
        }

        internal void SetCredentials(string tenant, string credentials)
        {
            if(this.CredList.ContainsKey(tenant))
            {
                this.CredList.Remove(tenant);
                this.CredList.Add(tenant, credentials);
            }
            else
            {
                this.CredList.Add(tenant, credentials);
            }
        }

        #endregion
    }
}
