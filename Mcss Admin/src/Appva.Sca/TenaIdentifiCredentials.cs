// <copyright file="TenaIdentifiCredentials.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Sca
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaIdentifiCredentials 
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiCredentials"/> class.
        /// </summary>
        public TenaIdentifiCredentials(string clientId, string clientSecret)
        {
            this.ClientId       = clientId;
            this.ClientSecret   = clientSecret;
        }

        #endregion

        #region Properties 
        
        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret
        {
            get;
            private set;
        }

        #endregion

        #region Calculated properties.

        public string BasicAuthorizationHeader
        {
            get
            {
                Requires.NotNullOrEmpty(this.ClientId,      "ClientId");
                Requires.NotNullOrEmpty(this.ClientSecret,  "ClientSecret");

                var content       = string.Format("{0}:{1}", this.ClientId, this.ClientSecret);
                var bytes         = System.Text.Encoding.UTF8.GetBytes(content);
                var base64Encoded = Convert.ToBase64String(bytes);

                return base64Encoded;
            }
        }

        #endregion
    }
}