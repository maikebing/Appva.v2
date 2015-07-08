// <copyright file="Watchdog.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Tenant.Identity;

    #endregion

    public interface IAuthorizeTenantIdentity
    {
        IAuthorizeTenantIdentityResult Validate(HttpRequestBase request);
    }

    public sealed class AuthorizeTenantIdentity : IAuthorizeTenantIdentity
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeTenantIdentity"/> class.
        /// </summary>
        public AuthorizeTenantIdentity(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        #endregion

        #region IAuthorizeTenantIdentity Members.

        public IAuthorizeTenantIdentityResult Validate(HttpRequestBase request)
        {
            ITenantIdentity identity;
            if (! this.tenantService.TryIdentifyTenant(out identity))
            {
                return AuthorizeTenantIdentityResult.NotFound;
            }
            if (identity.HostName.IsNotEmpty() && !identity.HostName.Equals(request.Url.Subdomain()))
            {
                return AuthorizeTenantIdentityResult.Unauthorized;
            }
            return AuthorizeTenantIdentityResult.Authorized;
        }

        #endregion
    }

    public interface IAuthorizeTenantIdentityResult
    {
        bool IsAuthorized{get;}
        bool IsUnauthorized
        {
            get;
        }
        bool IsNotFound
        {
            get;
        }
    }

    public sealed class AuthorizeTenantIdentityResult : IAuthorizeTenantIdentityResult
    {
        #region Variables.

        /// <summary>
        /// The tenant identity are missing from the request.
        /// </summary>
        public static readonly IAuthorizeTenantIdentityResult NotFound = new AuthorizeTenantIdentityResult(AuthorizeTenantResultCode.NotFound);

        /// <summary>
        /// The current tenant identity is unauthorized for the current request (URL).
        /// </summary>
        public static readonly IAuthorizeTenantIdentityResult Unauthorized = new AuthorizeTenantIdentityResult(AuthorizeTenantResultCode.Unauthorized);

        /// <summary>
        /// The current tenant identity is authorized for the current request (URL).
        /// </summary>
        public static readonly IAuthorizeTenantIdentityResult Authorized = new AuthorizeTenantIdentityResult(AuthorizeTenantResultCode.Authorized);

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeTenantIdentityResult"/> class.
        /// </summary>
        /// <param name="code">The result code</param>
        private AuthorizeTenantIdentityResult(AuthorizeTenantResultCode code)
        {
            this.Code = code;
        }

        #endregion

        #region Internal Enums.

        /// <summary>
        /// The tenant authorization result code.
        /// </summary>
        public enum AuthorizeTenantResultCode : int
        {
            /// <summary>
            /// No tenant identification found.
            /// </summary>
            NotFound = 0,

            /// <summary>
            /// The current tenant identity are not authorized for
            /// the current URL.
            /// </summary>
            Unauthorized = 1,

            /// <summary>
            /// The current tenant are authorized.
            /// </summary>
            Authorized = 2
        }

        #endregion

        #region IAuthorizeTenantIdentityResult Members.

        /// <inheritdoc />
        public AuthorizeTenantResultCode Code
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IsAuthorized
        {
            get
            {
                return this.Code.Equals(AuthorizeTenantResultCode.Authorized);
            }
        }

        /// <inheritdoc />
        public bool IsUnauthorized
        {
            get
            {
                return this.Code.Equals(AuthorizeTenantResultCode.Unauthorized);
            }
        }

        /// <inheritdoc />
        public bool IsNotFound
        {
            get
            {
                return this.Code.Equals(AuthorizeTenantResultCode.NotFound);
            }
        }

        #endregion
    }
}