// <copyright file="IOAuth2Service.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.OAuth
{
    #region Imports.

    using DotNetOpenAuth.OAuth2;
    using DotNetOpenAuth.OAuth2.ChannelElements;
    using DotNetOpenAuth.OAuth2.Messages;

    #endregion

    /// <summary>
    /// The OAuth 2 service interface.
    /// </summary>
    public interface IOAuth2Service
    {
        /// <summary>
        /// Whether or not the authorization is valid.
        /// </summary>
        /// <param name="authorization">The authorization</param>
        /// <returns>True if valid</returns>
        bool IsAuthorizationValid(IAuthorizationDescription authorization);

        /// <summary>
        /// Mints the access token.
        /// </summary>
        /// <param name="accessTokenRequest">The request</param>
        /// <returns>A minted access token</returns>
        AuthorizationServerAccessToken MintAccessToken(IAccessTokenRequest accessTokenRequest);
        
        /// <summary>
        /// Returns a client description by the client identifier.
        /// </summary>
        /// <param name="clientIdentifier">The identifier</param>
        /// <returns>A new instance of a client</returns>
        IClientDescription FindClient(string clientIdentifier);
        
        /// <summary>
        /// Authorize the resource owner crediential grant.
        /// </summary>
        /// <param name="accessRequest">The request</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The response</returns>
        AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(IAccessTokenRequest accessRequest, string userName, string password);
        
        /// <summary>
        /// Authorize the client credentials grant.
        /// </summary>
        /// <param name="accessRequest">The request</param>
        /// <returns>The response</returns>
        AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest);
    }
}