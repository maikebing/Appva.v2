// <copyright file="AuthorizationServerHost.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using Appva.OAuth;
    using DotNetOpenAuth.Messaging.Bindings;
    using DotNetOpenAuth.OAuth2;
    using DotNetOpenAuth.OAuth2.ChannelElements;
    using DotNetOpenAuth.OAuth2.Messages;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthorizationServerHost : IAuthorizationServerHost
    {
        #region Variables.

        private readonly IOAuth2Service oauthService;
        private readonly ICryptoKeyStore cryptoKeyStore;
        private readonly INonceStore nonceKeyStore;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationServer"/> class.
        /// </summary>
        /// <param name="oauthService"></param>
        /// <param name="cryptoKeyStore"></param>
        /// <param name="nonceKeyStore"></param>
        public AuthorizationServerHost(
            IOAuth2Service oauthService,
            ICryptoKeyStore cryptoKeyStore,
            INonceStore nonceKeyStore)
        {
            this.oauthService = oauthService;
            this.cryptoKeyStore = cryptoKeyStore;
            this.nonceKeyStore = nonceKeyStore;
        }

        #endregion

        #region Implementation of IAuthorizationServerHost

        /// <inheritdoc />
        public ICryptoKeyStore CryptoKeyStore
        {
            get
            {
                return this.cryptoKeyStore;
            }
        }

        /// <inheritdoc />
        public INonceStore NonceStore
        {
            get
            {
                return this.nonceKeyStore;
            }
        }

        /// <inheritdoc />
        public AccessTokenResult CreateAccessToken(IAccessTokenRequest accessTokenRequestMessage)
        {
            var accessToken = this.oauthService.MintAccessToken(accessTokenRequestMessage);
            return new AccessTokenResult(accessToken);
        }

        /// <inheritdoc />
        public IClientDescription GetClient(string clientIdentifier)
        {
            return this.oauthService.FindClient(clientIdentifier);
        }

        /// <inheritdoc />
        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            return this.oauthService.IsAuthorizationValid(authorization);
        }

        /// <inheritdoc />
        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(string userName, string password, IAccessTokenRequest accessRequest)
        {
            return this.oauthService.CheckAuthorizeResourceOwnerCredentialGrant(accessRequest, userName, password);
        }

        /// <inheritdoc />
        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            return this.oauthService.CheckAuthorizeClientCredentialsGrant(accessRequest);
        }

        #endregion
    }
}