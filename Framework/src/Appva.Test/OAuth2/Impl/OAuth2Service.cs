// <copyright file="OAuth2Service.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.OAuth2.Impl
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.OAuth;
    using DotNetOpenAuth.OAuth2;
    using DotNetOpenAuth.OAuth2.ChannelElements;
    using DotNetOpenAuth.OAuth2.Messages;
    using Appva.Core.Extensions;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class OAuth2Service : IOAuth2Service
    {
        #region Variables.

        /// <summary>
        /// The authorizations.
        /// </summary>
        private static IList<Authorization> Authorizations = new List<Authorization>();

        /// <summary>
        /// The clients.
        /// </summary>
        private static IList<OAuth2Client> Clients = new List<OAuth2Client>
        {
            new OAuth2Client(
                "MIIB0TCCATqgAwIBAgIQFMDT/jJFUZJLkt2V44vuUDANBgkqhkiG9w0BAQUFADAU", 
                "Test", 
                "secret", 
                null, 
                new HashSet<string>{ "https://test.api.resource" }, 
                (int) ClientType.Public)
        };

        /// <summary>
        /// The <see cref="IOAuth2AuthorizationServerSigningKeyHandler"/>.
        /// </summary>
        private readonly IOAuth2AuthorizationServerSigningKeyHandler tokenSigningKeyHandler;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2Service"/> class.
        /// </summary>
        public OAuth2Service(IOAuth2AuthorizationServerSigningKeyHandler tokenSigningKeyHandler)
        {
            this.tokenSigningKeyHandler = tokenSigningKeyHandler;
            
        }

        #endregion

        #region IOAuth2Service Members

        /// <inheritdoc />
        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            var createdAt = authorization.UtcIssued + TimeSpan.FromSeconds(10);
            var authorizations = Authorizations.Where(
                x => x.ClientId == authorization.ClientIdentifier
                && x.CreatedAt <= createdAt
                && (!x.ExpiresAt.HasValue || x.ExpiresAt.Value >= DateTime.UtcNow)
                && x.UserId == authorization.User
                ).ToList();
            if (! authorizations.Any())
            {
                return false;
            }
            var isApproved = false;
            foreach (var auth in authorizations)
            {
                isApproved = authorization.Scope.IsSubsetOf(auth.Scopes);
            }
            return isApproved;
        }

        /// <inheritdoc />
        public AuthorizationServerAccessToken MintAccessToken(IAccessTokenRequest accessTokenRequest)
        {
            var cspProvider = new RSACryptoServiceProvider();
            cspProvider.FromXmlString("PFJTQUtleVZhbHVlPjxNb2R1bHVzPjVFOEpmc2FScnNZdVpKVHNhTlJIcENsb3NvR0N1OFZtOCtuN0w5YlpGYzkyb3pYS1N0UFRtVWdCT0RUSlYzNzFDMU9hKzBBbyswdHMrQi9JYnR1RUdTeVNKWVh5U2FiKzdHYzRGNDUrSDJKVUFlOXpJcmJmVVFKdVprT25WRk1ISUJMY2RWaFJDNUd0L2JwWVdXeHk4RHcvSEV3WTdHb0Y1RHE0dDFabE9oYz08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPi95ajJSelB6TUdSaERGZEZOb25vUktRM0dxL2R6eVgwWHVsL09vdmY0V1JCalRIMU1zTFFWNHpram1vcUdzRGdSNS9ndDlwZUhLZE85OEFxMWF4S0t3PT08L1A+PFE+NVE5eUtvei9iRXB4aktDcUdXcGdYb0pKbUx5cmV0dllqbnUrZUx1YkZBdFJHNTFaS1c5Y2NjRGRReUw0cVl2VCt4RUtwVE9WOTRZMCs2dTE3Ly8xeFE9PTwvUT48RFA+MUN0Y2RpS1ZnbFRGZWZ5TFdlbGNrTTgzM3VJRC83N2dyNWRiS3ZTcVNnSVNtL1RCbmQ3dVhRNlQ4blFHNU00aThJdlowU2NzQUltQ21YSmFhK2hpbHc9PTwvRFA+PERRPlIwaCtRK1dyRzEwelp3c2N4Rm9KY1gva1BXY0Jpbk5lT2tFaUxscGIwV29RTEtMVCs0UVBaY3NhVUdsU3J4aTN1RnMrVDlqMDQ1bmRaVEN6cHVPbDdRPT08L0RRPjxJbnZlcnNlUT5hdWlDRklKcm1qZnhKNG1UczNyYjZzZ3R4RXZ6Ukx3R0RRckRWVGZXYWhmazIrSlB5ajhNQUtseE1xc2s2UU0zaDRsSEo4blVlME5hMkNaaVk4MzRiUT09PC9JbnZlcnNlUT48RD5PVldINHVobTZXY25lMmJTdjdpZmpTQkJCS0wxZThZbEJwZjUxcGV6eUlCaklMVi9hYThzY2svY3pvcWpEVy82aGErbk9mVncxVHFwME4zYWJjeCtyWUlqMXhIZEdpaWkyUGdzc0c2VUd2UERyN0t0VFpaRFFzb3RsSnc5RmM4bElnUkI3VXBmVGo4dnU1dklmSjlaaXJJdS9TYnFzRmFPa2c4VndzUnhUNUU9PC9EPjwvUlNBS2V5VmFsdWU+".FromBase64().ToUtf8());
            var accessToken = new AuthorizationServerAccessToken();
            accessToken.ResourceServerEncryptionKey = cspProvider;
            accessToken.AccessTokenSigningKey = tokenSigningKeyHandler.TokenSigningKey;
            accessToken.ExtraData.Add("test", "test");
            return accessToken;
        }

        /// <inheritdoc />
        public IClientDescription FindClient(string clientIdentifier)
        {
            var client = Clients.Where(x => x.Identifier.Equals(clientIdentifier)).SingleOrDefault();
            if (client.IsNull())
            {
                throw new Exception("No client!");
            }
            return client;
        }

        /// <inheritdoc />
        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(IAccessTokenRequest accessRequest, string userName, string password)
        {
            var client = Clients.Where(x => x.Identifier.Equals(accessRequest.ClientIdentifier)).SingleOrDefault();
            if (!accessRequest.ClientAuthenticated || client.IsNull())
            {
                throw new Exception("Invalid credentials!");
            }
            if (userName.NotEquals("admin") && password.NotEquals("password"))
            {
                throw new Exception("Invalid credentials!");
            }
            var isApproved = accessRequest.Scope.IsSubsetOf(client.Scopes);
            if (isApproved)
            {
                Authorizations.Add(new Authorization()
                {
                    ClientId = accessRequest.ClientIdentifier,
                    UserId = "admin",
                    Scopes = accessRequest.Scope,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(100)
                });
                accessRequest.ExtraData.Add("firstname", "admin");
                accessRequest.ExtraData.Add("lastname", "adminsson");
            }
            return new AutomatedUserAuthorizationCheckResponse(accessRequest, isApproved, "admin");
        }

        /// <inheritdoc />
        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            var client = Clients.Where(x => x.Identifier.Equals(accessRequest.ClientIdentifier)).SingleOrDefault();
            if (! accessRequest.ClientAuthenticated || client.IsNull())
            {
                throw new Exception("Invalid credentials!");
            }
            var isApproved = accessRequest.Scope.IsSubsetOf(client.Scopes);
            if (isApproved)
            {
                accessRequest.ExtraData.Add("client", client.Name);
                Authorizations.Add(new Authorization()
                    {
                        ClientId = accessRequest.ClientIdentifier,
                        UserId = client.Name,
                        Scopes = accessRequest.Scope,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(100)
                    });
            }
            return new AutomatedAuthorizationCheckResponse(accessRequest, isApproved);
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// Authorization.
        /// </summary>
        public class Authorization
        {
            /// <summary>
            /// The client id.
            /// </summary>
            public string ClientId { get; set; }

            /// <summary>
            /// The user id. 
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// The authorized scopes.
            /// </summary>
            public HashSet<string> Scopes { get; set; }

            /// <summary>
            /// The authorization created at date time (UTC).
            /// </summary>
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// The authorization expiration at date time (UTC).
            /// </summary>
            public DateTime? ExpiresAt { get; set; }
        }

        #endregion
    }
}