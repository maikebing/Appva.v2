// <copyright file="AuthorizationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.OAuth;
    using Appva.Persistence;
    using DotNetOpenAuth.OAuth2;
    using DotNetOpenAuth.OAuth2.ChannelElements;
    using DotNetOpenAuth.OAuth2.Messages;
    using Validation;
    using Appva.Core.Extensions;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using System.Security.Claims;
    using Newtonsoft.Json;
    using Appva.Mcss.AuthorizationServer.Domain.Services;

    #endregion

    /// <summary>
    /// Marker interface.
    /// </summary>
    public interface IAuthorizationService : IOAuth2Service
    {
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthorizationService : IAuthorizationService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IOAuth2AuthorizationServerSigningKeyHandler"/>.
        /// </summary>
        private readonly IOAuth2AuthorizationServerSigningKeyHandler authorizationServerSigningKeyHandler;

        /// <summary>
        /// The <see cref="IAuthenticationService"/>.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IOAuth2AuthorizationServerSigningKeyHandler"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public AuthorizationService(
            IOAuth2AuthorizationServerSigningKeyHandler authorizationServerSigningKeyHandler,
            IUserService userService,
            IPersistenceContext persistenceContext)
        {
            this.authorizationServerSigningKeyHandler = authorizationServerSigningKeyHandler;
            this.userService = userService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IOAuth2Service Members

        /// <inheritdoc />
        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            //// FIXME: Add IsAuthorizationValid in stand alone service
            var issuedAt = authorization.UtcIssued + TimeSpan.FromSeconds(1);
            var auth = this.persistenceContext.QueryOver<Authorization>()
                .Where(x => x.CreatedAt <= issuedAt)
                .And(Restrictions.Disjunction()
                    .Add(Restrictions.On<Authorization>(x => x.ExpiresAt).IsNull)
                    .Add<Authorization>(x => x.ExpiresAt.Value >= DateTime.UtcNow)
                )
                .And(x => x.UserIdentifier == authorization.User)
                .JoinQueryOver<Client>(x => x.Client)
                .Where(x => x.Identifier == authorization.ClientIdentifier)
                .List()
                .FirstOrDefault();
            if (! auth.Scopes.Any())
            {
                return false;
            }
            var grantedScopes = new HashSet<string>(auth.Scopes.Select(x => x).ToList());
            return authorization.Scope.IsSubsetOf(grantedScopes);
        }

        /// <inheritdoc />
        public AuthorizationServerAccessToken MintAccessToken(IAccessTokenRequest request)
        {
            //// FIXME: Add MintAccessToken in stand alone service
            //// FIXME: BuildClaims should be separated
            Requires.NotNull(request, "request");
            var client = this.persistenceContext.QueryOver<Client>()
                .Where(x => x.Identifier == request.ClientIdentifier)
                .And(x => x.IsActive == true)
                .SingleOrDefault();
            Requires.ValidState(client.IsNotNull(), "No client identifier");
            var resources = this.persistenceContext.QueryOver<Resource>()
                .JoinQueryOver<Scope>(x => x.Scopes)
                .WhereRestrictionOn(x => x.Key).IsIn(request.Scope.ToArray())
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            Requires.ValidState(resources.Count.Equals(1), "Ambiguous scopes");
            var resource = resources.SingleOrDefault();
            var accessToken = new AuthorizationServerAccessToken();
            this.BuildClaims(client, resource, accessToken, request);
            if (client.AccessTokenLifetime > 0)
            {
                accessToken.Lifetime = TimeSpan.FromMinutes(client.AccessTokenLifetime);
            }
            accessToken.ResourceServerEncryptionKey = resource.PublicTokenEncrypter;
            accessToken.AccessTokenSigningKey = this.authorizationServerSigningKeyHandler.TokenSigningKey;
            return accessToken;
        }

        private void BuildClaims(Client client, Resource resource, AuthorizationServerAccessToken token, IAccessTokenRequest request)
        {
            var accessTokenClaim = new AccessTokenClaim();
            accessTokenClaim.Claims.Add("https://schemas.appva.se/identity/claims/client", client.Identifier);
            accessTokenClaim.Claims.Add("https://schemas.appva.se/identity/claims/audience", resource.Slug.Name);
            accessTokenClaim.Claims.Add("https://schemas.appva.se/identity/claims/tenant", client.Tenants.Count == 1 ? client.Tenants.First().Id.ToString() : null);
            token.ExtraData.Add("claims", JsonConvert.SerializeObject(accessTokenClaim));
            //// token.ExtraData.Add("test", request.ExtraData["x"]);
        }

        /// <inheritdoc />
        public IClientDescription FindClient(string clientIdentifier)
        {
            //// FIXME: FindClient Service call
            var client = this.persistenceContext.QueryOver<Client>()
                .Where(x => x.Identifier == clientIdentifier)
                .SingleOrDefault();
            Requires.ValidState(client.IsNotNull(), "Invalid client identifier");
            return new OAuth2Client(client.Identifier, client.Name, client.Secret, client.RedirectionEndpoint, new HashSet<string>(client.Scopes.Select(x => x.Key).ToList()), (int) client.Type);
        }

        /// <inheritdoc />
        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(IAccessTokenRequest request, string userName, string password)
        {
            //// FIXME: CheckAuthorizeResourceOwnerCredentialGrant Service call.
            //// FIXME: Add claims here!
            var isApproved = false;
            var client = this.persistenceContext.QueryOver<Client>()
                .Where(x => x.Identifier == request.ClientIdentifier)
                .SingleOrDefault();
            Requires.ValidState(client.IsNotNull(), "Invalid client identifier");
            var grantedScopes = new HashSet<string>(client.Scopes.Select(x => x.Key).ToList());
            isApproved = request.Scope.IsSubsetOf(grantedScopes);
            User user = null;
            this.userService.AuthenticateWithPersonalIdentityNumber(userName, password, "oauth", out user);
            if (user.IsNull())
            {
                isApproved = false;
            }
            if (isApproved)
            {
                this.persistenceContext.Save(new Authorization(client, userName, request.Scope.ToList(), DateTime.UtcNow.AddYears(100)));
            }
            return new AutomatedUserAuthorizationCheckResponse(request, isApproved, userName);
        }

        /// <inheritdoc />
        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest request)
        {
            //// FIXME: CheckAuthorizeClientCredentialsGrant Service call
            //// FIXME: Add claims here!
            var client = this.persistenceContext.QueryOver<Client>()
                .Where(x => x.Identifier == request.ClientIdentifier)
                .SingleOrDefault();
            Requires.ValidState(client.IsNotNull(), "Invalid client identifier");
            var grantedScopes = new HashSet<string>(client.Scopes.Select(x => x.Key).ToList());
            var isApproved = request.Scope.IsSubsetOf(grantedScopes);
            return new AutomatedAuthorizationCheckResponse(request, isApproved);
        }

        #endregion

        //// FIXME: Should not be here!
        [Serializable]
        public sealed class AccessTokenClaim
        {
            public AccessTokenClaim()
            {
                Claims = new Dictionary<string, string>();
            }

            public IDictionary<string, string> Claims
            {
                get;
                private set;
            }
        }
    }
}