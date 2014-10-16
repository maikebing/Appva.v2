// <copyright file="IOAuth2Service.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.OAuth
{
    #region Imports.

    using DotNetOpenAuth.OAuth2;
    using DotNetOpenAuth.OAuth2.ChannelElements;
    using DotNetOpenAuth.OAuth2.Messages;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IOAuth2Service
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        bool IsAuthorizationValid(IAuthorizationDescription authorization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessTokenRequest"></param>
        /// <returns></returns>
        AuthorizationServerAccessToken MintAccessToken(IAccessTokenRequest accessTokenRequest);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <returns></returns>
        IClientDescription FindClient(string clientIdentifier);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessRequest"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(IAccessTokenRequest accessRequest, string userName, string password);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessRequest"></param>
        /// <returns></returns>
        AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest);
    }
}