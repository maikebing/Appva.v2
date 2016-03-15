// <copyright file="IdentityService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;

    #endregion

    /// <summary>
    /// Handles Owin web based identity.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Returns the current authenticated user.
        /// </summary>
        /// <returns>The current authenticated user or if not authenticated; null</returns>
        ClaimsPrincipal Principal
        {
            get;
        }

        Guid PrincipalId
        {
            get;
        }

        /// <summary>
        /// Returns the current authenticated user roles.
        /// </summary>
        /// <returns>The current authenticated user roles or if not authenticated; null</returns>
        IEnumerable<Claim> Roles();

        /// <summary>
        /// Returns the current authenticated user permissions.
        /// </summary>
        /// <returns>The current authenticated user permissions or if not authenticated; null</returns>
        IEnumerable<Claim> Permissions();

        /// <summary>
        /// Returns the current authenticated user schedule permissions.
        /// </summary>
        /// <returns>The current authenticated user schedule permissions or if not authenticated; null</returns>
        IEnumerable<Claim> SchedulePermissions();

        /// <summary>
        /// Returns whether the current authenticated user is a member of the specified role.
        /// </summary>
        /// <param name="role">The role which the user must be a member of</param>
        /// <returns>True, if the user is a member of the specified role</returns>
        bool IsInRole(string role);

        /// <summary>
        /// Returns whether the current authenticated user is a member of the specified permission.
        /// </summary>
        /// <param name="role">The permission which the user must be a member of</param>
        /// <returns>True, if the user is a member of the specified permission</returns>
        bool HasPermission(string permission);

        /// <summary>
        /// Add information to the response environment that will cause the appropriate
        /// authentication middleware to grant a claims-based identity to the recipient of 
        /// the response. The exact mechanism of this may vary.  Examples include setting a 
        /// cookie, to adding a fragment on the redirect url, or producing an OAuth2 access 
        /// code or token response.
        /// </summary>
        /// <param name="properties">
        /// Contains additional properties the middleware are expected to persist along with 
        /// the claims. These values will be returned as the 
        /// <c>AuthenticateResult.properties</c> collection when <c>AuthenticateAsync</c> is 
        /// called on subsequent requests.
        /// </param>
        /// <param name="identities">
        /// Determines which claims are granted to the signed in user. The 
        /// <c>ClaimsIdentity.AuthenticationType</c> property is compared to the 
        /// middleware's <c>Options.AuthenticationType</c> value to determine which claims 
        /// are granted by which middleware. The recommended use is to have a single 
        /// <c>ClaimsIdentity</c> which has the AuthenticationType matching a specific 
        /// middleware.
        /// </param>
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);

        /// <summary>
        /// Add information to the response environment that will cause the appropriate
        /// authentication middleware to revoke any claims identity associated the the
        /// caller. The exact method varies.
        /// </summary>
        /// <param name="properties">
        /// Additional arbitrary values which may be used by particular authentication types
        /// </param>
        /// <param name="authenticationTypes">
        /// Identifies which middleware should perform the work to sign out. Multiple
        /// authentication types may be provided to clear out more than one cookie at a 
        /// time, or to clear cookies and redirect to an external single-sign out url
        /// </param>
        void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);

        /// <summary>
        /// Add information to the response environment that will cause the appropriate
        /// authentication middleware to revoke any claims identity associated the the
        /// caller. The exact method varies.
        /// </summary>
        /// <param name="authenticationTypes">
        /// Identifies which middleware should perform the work to sign out. Multiple
        /// authentication types may be provided to clear out more than one cookie at a 
        /// time, or to clear cookies and redirect to an external single-sign out url
        /// </param>
        void SignOut(params string[] authenticationTypes);

        /// <summary>
        /// Whether or not access control is activated or in preview mode for the user
        /// account.
        /// </summary>
        /// <returns>
        /// True if access control is in preview mode for the mcss administrative role
        /// or if access control is activated.
        /// </returns>
        bool IsAccessControlActiveForUser();

        /// <summary>
        /// Whether or not the current user account is a member of the 
        /// <see cref="RoleTypes.Appva"/> role.
        /// </summary>
        /// <returns>
        /// True if the current user account is a member of the 
        /// <see cref="RoleTypes.Appva"/> role; otherwise false.
        /// </returns>
        bool IsAppvaAccount();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class IdentityService : IIdentityService
    {
        #region Variables.

        /// <summary>
        /// The current Owin context.
        /// </summary>
        private readonly IOwinContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="environment">The Owin environment variables</param>
        public IdentityService(IDictionary<string, object> environment)
        {
            this.context = new OwinContext(environment);
        }

        #endregion

        #region IIdentityService Members.

        /// <inheritdoc />
        public ClaimsPrincipal Principal
        {
            get
            {
                return this.context.Authentication.User;
            }
        }

        /// <inheritdoc />
        public Guid PrincipalId
        {
            get
            {
                return new Guid(this.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Claim> Roles()
        {
            return this.Principal.Claims.Where(x => x.Type.Equals(ClaimTypes.Role)).ToList();
        }

        /// <inheritdoc />
        public IEnumerable<Claim> Permissions()
        {
            return this.Principal.Claims.Where(x => x.Type.Equals(Core.Resources.ClaimTypes.Permission)).ToList();
        }

        /// <inheritdoc />
        public IEnumerable<Claim> SchedulePermissions()
        {
            return this.Principal.Claims.Where(x => x.Type.Equals(Core.Resources.ClaimTypes.SchedulePermission)).ToList();
        }

        /// <inheritdoc />
        public bool IsInRole(string role)
        {
            return this.Principal.IsInRole(role);
        }

        /// <inheritdoc />
        public bool HasPermission(string permission)
        {
            return this.Permissions().Any(x => x.Value.Equals(permission));
        }

        /// <inheritdoc />
        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            this.context.Authentication.SignIn(properties, identities);
        }

        /// <inheritdoc />
        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            this.context.Authentication.SignOut(properties, authenticationTypes);
        }

        /// <inheritdoc />
        public void SignOut(params string[] authenticationTypes)
        {
            this.context.Authentication.SignOut(authenticationTypes);
        }

        /// <inheritdoc />
        public bool IsAccessControlActiveForUser()
        {
            return this.Principal.HasClaim(Core.Resources.ClaimTypes.AclEnabled, "Y") ||
                (this.Principal.HasClaim(Core.Resources.ClaimTypes.AclPreview, "Y") && this.IsAppvaAccount());
        }

        /// <inheritdoc />
        public bool IsAppvaAccount()
        {
            return this.Principal.IsInRole(Core.Resources.RoleTypes.Appva);
        }

        #endregion
    }
}