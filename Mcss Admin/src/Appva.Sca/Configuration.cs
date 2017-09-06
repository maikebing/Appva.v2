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

    #endregion

    /// <summary>
    /// Configuration
    /// </summary>
    public class Configuration
    {
        #region Fields.

        /// <summary>
        /// ClientId.
        /// </summary>
        private string clientId; // = "EABE6751-2ABD-4311-A794-70A833D31C31"

        /// <summary>
        /// ClientSecret.
        /// </summary>
        private string clientSecret; // = "C5C8DAEB-6C07-423D-82CF-8177C8CB9604"

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        public Configuration(Uri baseAddress, string clientId, string clientSecret)
        {
            this.BaseAddress = baseAddress;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(this.clientId + ":" + this.clientSecret));
        }

        #endregion
    }
}
