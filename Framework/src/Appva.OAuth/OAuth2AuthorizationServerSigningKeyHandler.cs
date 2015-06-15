// <copyright file="OAuth2AuthorizationServerSigningKeyHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.OAuth
{
    #region Imports.

    using System;
    using System.Security.Cryptography;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// Responsible for providing the authorization server key to 
    /// use in signing the token.
    /// </summary>
    public interface IOAuth2AuthorizationServerSigningKeyHandler : IDisposable
    {
        /// <summary>
        /// Returns the authorization server token signing key. 
        /// </summary>
        RSACryptoServiceProvider TokenSigningKey
        {
            get;
        }
    }

    /// <summary>
    /// <see cref="IOAuth2AuthorizationServerSigningKeyHandler"/> implementation.
    /// </summary>
    public sealed class OAuth2AuthorizationServerSigningKeyHandler : IOAuth2AuthorizationServerSigningKeyHandler
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="RSACryptoServiceProvider"/>.
        /// </summary>
        private readonly RSACryptoServiceProvider cspProvider;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2AuthorizationServerSigningKeyHandler"/> class.
        /// </summary>
        public OAuth2AuthorizationServerSigningKeyHandler()
        {
            //// No op!
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2AuthorizationServerSigningKeyHandler"/> class.
        /// </summary>
        /// <param name="key">The base64 signing key</param>
        public OAuth2AuthorizationServerSigningKeyHandler(string key)
        {
            this.cspProvider = new RSACryptoServiceProvider();
            this.cspProvider.FromXmlString(key.FromBase64().ToUtf8());
        }

        #endregion

        #region Public Properties.

        /// <inheritdoc />
        public RSACryptoServiceProvider TokenSigningKey
        {
            get
            {
                return this.cspProvider;
            }
        }

        #endregion

        #region IDisposable Members.

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.cspProvider != null)
            {
                this.cspProvider.Dispose();
            }
        }

        #endregion
    }
}