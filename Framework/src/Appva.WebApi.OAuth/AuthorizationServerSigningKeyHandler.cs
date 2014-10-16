// <copyright file="AuthorizationServerSigningKeyHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.OAuth
{
    #region Imports.

    using System.Configuration;
    using System.Security.Cryptography;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// Responsible for providing the key to verify the token came 
    /// from the authorization server.
    /// </summary>
    public interface IAuthorizationServerSigningKeyHandler
    {
        /// <summary>
        /// Returns the authorization server token signing key. 
        /// </summary>
        RSACryptoServiceProvider PublicKey
        {
            get;
        }
    }

    /// <summary>
    /// <see cref="IOAuth2AuthorizationServerSigningKeyHandler"/> implementation.
    /// </summary>
    public sealed class AuthorizationServerSigningKeyHandler : IAuthorizationServerSigningKeyHandler
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="RSACryptoServiceProvider"/>.
        /// </summary>
        private readonly RSACryptoServiceProvider cspProvider;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationServerSigningKeyHandler"/> class.
        /// </summary>
        public AuthorizationServerSigningKeyHandler()
            : this(ConfigurationManager.AppSettings.Get("AuthorizationServerPublicKey"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationServerSigningKeyHandler"/> class.
        /// </summary>
        /// <param name="key">The base64 signing key</param>
        public AuthorizationServerSigningKeyHandler(string key)
        {
            this.cspProvider = new RSACryptoServiceProvider();
            this.cspProvider.FromXmlString(key.FromBase64().ToUtf8());
        }

        #endregion

        #region Public Properties.

        /// <inheritdoc />
        public RSACryptoServiceProvider PublicKey
        {
            get
            {
                return this.cspProvider;
            }
        }

        #endregion
    }
}