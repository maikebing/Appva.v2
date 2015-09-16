// <copyright file="ResetPasswordMiddleware.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System.IdentityModel.Tokens;
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;
    using Appva.Core.Extensions;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ResetPasswordOptions : AuthenticationOptions
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordOptions"/> class.
        /// </summary>
        public ResetPasswordOptions()
            : base("JWT")
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The 
        /// </summary>
        public string TokenQueryParam
        {
            get;
            set;
        }

        /// <summary>
        /// The reset password path.
        /// </summary>
        public PathString ResetPasswordPath
        {
            get;
            set;
        }

        /// <summary>
        /// The token expiration path.
        /// </summary>
        public PathString TokenExpiredPath
        {
            get;
            set;
        }

        /// <summary>
        /// The token invalid path.
        /// </summary>
        public PathString TokenInvalidPath
        {
            get;
            set;
        }

        /// <summary>
        /// The secure data format provider.
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> Provider
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ResetPasswordMiddleware : AuthenticationMiddleware<ResetPasswordOptions>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<ResetPasswordMiddleware>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordMiddleware"/> class.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="next"></param>
        public ResetPasswordMiddleware(ResetPasswordOptions options, OwinMiddleware next)
            : base(next, options)
        {
        }

        #endregion

        #region OwinMiddleware Overrides.

        /// <inheritdoc />
        public async override Task Invoke(IOwinContext context)
        {
            try
            {
                await base.Invoke(context);
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                if (ex is SecurityTokenExpiredException)
                {
                    context.Response.Redirect(this.Options.TokenExpiredPath.Value);
                }
                else
                {
                    context.Response.Redirect(this.Options.TokenInvalidPath.Value);
                }
                return;
            }
        }

        #endregion

        #region AuthenticationMiddleware Overrides.

        /// <inheritdoc />
        protected override AuthenticationHandler<ResetPasswordOptions> CreateHandler()
        {
            return new ResetPasswordAuthenticationHandler();
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ResetPasswordAuthenticationHandler : AuthenticationHandler<ResetPasswordOptions>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<ResetPasswordAuthenticationHandler>();

        #endregion

        #region AuthenticationHandler Overrides.

        /// <inheritdoc />
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            if (! Request.Path.StartsWithSegments(this.Options.ResetPasswordPath))
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            var token = this.Request.Query.Get(this.Options.TokenQueryParam);
            if (string.IsNullOrWhiteSpace(token))
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            var context = new AuthenticationTokenReceiveContext(Context, Options.Provider, token);
            context.DeserializeTicket(context.Token);
            if (context.Ticket == null)
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            return Task.FromResult(context.Ticket);
        }

        #endregion
    }
}