// <copyright file="SithsIdentity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Identity
{
    #region Imports.

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Representation of a SITHS identity from Authify.
    /// </summary>
    public sealed class SithsIdentity
    {
        /// <summary>
        /// The Authify ID from the Authify Federation Authentication Server. 
        /// </summary>
        /// <remarks>
        /// The Authify ID is unique per customer, secret and API key.
        /// </remarks>
        [JsonProperty("uid")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// The identity provider (IdP) which the user account first successfully 
        /// authenticated against.
        /// </summary>
        [JsonProperty("idp")]
        public string IdentityProvider
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a list of active identity providers (IdP).
        /// </summary>
        [JsonProperty("item")]
        public string IdentityProviders
        {
            get;
            set;
        }

        /// <summary>
        /// The authenticated state, either 'login' or 'logout'.
        /// <list type="bullet">
        ///   <item><c>login</c> means that the user is NOT logged in via Authify</item>
        ///   <item><c>logout</c> means that the user is logged in via Authify</item>
        /// </list>
        /// </summary>
        [JsonProperty("state")]
        public string AuthenticationState
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Unknown and undocumented feature!
        /// <externalLink>
        ///     <linkText>"Our well-documented API"</linkText>
        ///     <linkUri>
        ///         http://www.authify.com/news/8/59/Authify-Client---An-API-for-all-logins.html
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [JsonProperty("mapuid")]
        public string MapId
        {
            get;
            set;
        }

        /// <summary>
        /// Local user ID.
        /// </summary>
        [JsonProperty("luid")]
        public string LocalUserId
        {
            get;
            set;
        }

        /// <summary>
        /// Identity provider (IdP) user ID - in this case a HSA ID.
        /// </summary>
        [JsonProperty("idpuid")]
        public string HsaId
        {
            get;
            set;
        }

        /// <summary>
        /// The full human name.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The E-mail address.
        /// </summary>
        [JsonProperty("email")]
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Extra unknown data.
        /// </summary>
        [JsonProperty("extra")]
        public string Extra
        {
            get;
            set;
        }
    }
}