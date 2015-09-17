// <copyright file="ResetPasswordMiddleware.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System;
    using System.IdentityModel;
    using System.IdentityModel.Tokens;
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SecurityTokenOptions : AuthenticationOptions
    {
        #region Variables.

        /// <summary>
        /// The token query parameter.
        /// </summary>
        public const string TokenQueryParameter = "token";

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTokenOptions"/> class.
        /// </summary>
        public SecurityTokenOptions()
            : base("JWT")
        {
            this.AuthenticationMode = AuthenticationMode.Active;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The reset token path.
        /// </summary>
        public PathString ResetTokenPath
        {
            get;
            set;
        }

        /// <summary>
        /// The reset token expiration path.
        /// </summary>
        public PathString ResetTokenExpiredPath
        {
            get;
            set;
        }

        /// <summary>
        /// The registration token path.
        /// </summary>
        public PathString RegisterTokenPath
        {
            get;
            set;
        }

        /// <summary>
        /// The registration token expiration path.
        /// </summary>
        public PathString RegisterTokenExpiredPath
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
    public sealed class SecurityTokenMiddleware : AuthenticationMiddleware<SecurityTokenOptions>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<SecurityTokenMiddleware>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTokenMiddleware"/> class.
        /// </summary>
        /// <param name="options">The <see cref="SecurityTokenOptions"/></param>
        /// <param name="next">The <see cref="OwinMiddleware"/></param>
        public SecurityTokenMiddleware(SecurityTokenOptions options, OwinMiddleware next)
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
            catch (TokenExpiredException ex)
            {
                if (ex.IsRegisterToken)
                {
                    context.Response.Redirect(this.Options.RegisterTokenExpiredPath.Value);
                }
                else if (ex.IsResetToken)
                {
                    context.Response.Redirect(this.Options.ResetTokenExpiredPath.Value);
                }
                else
                {
                    context.Response.Redirect(this.Options.TokenInvalidPath.Value);
                }
                Log.Info(ex);
                return;
            }
            catch (TokenInvalidException ex)
            {
                Log.Warn(ex);
                context.Response.Redirect(this.Options.TokenInvalidPath.Value);
                return;
            }
        }

        #endregion

        #region AuthenticationMiddleware Overrides.

        /// <inheritdoc />
        protected override AuthenticationHandler<SecurityTokenOptions> CreateHandler()
        {
            return new SecurityTokenAuthenticationHandler();
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SecurityTokenAuthenticationHandler : AuthenticationHandler<SecurityTokenOptions>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<SecurityTokenAuthenticationHandler>();

        #endregion

        #region AuthenticationHandler Overrides.

        /// <inheritdoc />
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var isRegisterToken = Request.Path.StartsWithSegments(this.Options.RegisterTokenPath);
            var isResetToken    = Request.Path.StartsWithSegments(this.Options.ResetTokenPath);
            if (! isRegisterToken && ! isResetToken)
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            if (Request.Path.Value.Contains("success") || Request.Path.Value.Contains("expired"))
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            var token = this.Request.Query.Get(SecurityTokenOptions.TokenQueryParameter);
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new TokenInvalidException("Invalid token, missing required token parameter");
            }
            try
            {
                var context = new AuthenticationTokenReceiveContext(Context, Options.Provider, token);
                context.DeserializeTicket(context.Token);
                if (context.Ticket == null)
                {
                    return Task.FromResult<AuthenticationTicket>(null);
                }
                return Task.FromResult(context.Ticket);
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw new TokenExpiredException(isRegisterToken, isResetToken, "The token is expired", ex);
            }
            catch (SignatureVerificationFailedException ex)
            {
                throw new TokenExpiredException("The token is expired due to invalid signature", ex);
            }
            catch (FormatException ex)
            {
                throw new TokenInvalidException("Invalid token format", ex);
            }
            catch (Exception ex)
            {
                throw new TokenInvalidException("Invalid token", ex);
            }
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Serializable]
    public sealed class TokenExpiredException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenExpiredException"/> class.
        /// </summary>
        public TokenExpiredException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenExpiredException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public TokenExpiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenExpiredException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public TokenExpiredException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenExpiredException"/> class.
        /// </summary>
        /// <param name="isRegisterToken">
        /// Whether or not the token is a register token or not.
        /// </param>
        /// <param name="isResetToken">
        /// Whether or not the token is a reset token or not.
        /// </param>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public TokenExpiredException(bool isRegisterToken, bool isResetToken, string message, Exception inner)
            : base(message, inner)
        {
            this.IsRegisterToken = isRegisterToken;
            this.IsResetToken    = isResetToken;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the token is a register token or not.
        /// </summary>
        public bool IsRegisterToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not the token is a reset token or not.
        /// </summary>
        public bool IsResetToken
        {
            get;
            private set;
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Serializable]
    public sealed class TokenInvalidException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenInvalidException"/> class.
        /// </summary>
        public TokenInvalidException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public TokenInvalidException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public TokenInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}