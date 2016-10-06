// <copyright file="TenantCookieAuthenticationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Middleware.Cookie
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Infrastructure;

    #endregion

    /// <summary>
    /// A custom <see cref="AuthenticationHandler{CookieAuthenticationOptions}"/> implementation.
    /// <externalLink>
    ///     <linkText>Katana AuthenticationHandler.cs</linkText>
    ///     <linkUri>
    ///         https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security/Infrastructure/AuthenticationHandler.cs
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    internal class TenantCookieAuthenticationHandler : AuthenticationHandler<CookieAuthenticationOptions>
    {
        #region Variables.

        /// <summary>
        /// The session ID claim.
        /// </summary>
        private const string SessionIdClaim = "Microsoft.Owin.Security.Cookies-SessionId";

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantCookieAuthenticationHandler>();

        /// <summary>
        /// The session key.
        /// </summary>
        private string sessionKey;

        /// <summary>
        /// If the cookie should renew.
        /// </summary>
        private bool shouldRenew;

        /// <summary>
        /// The renewal issue utc date time.
        /// </summary>
        private DateTimeOffset renewIssuedUtc;

        /// <summary>
        /// The renewal expiration utc date time.
        /// </summary>
        private DateTimeOffset renewExpiresUtc;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantCookieAuthenticationHandler"/> class.
        /// </summary>
        /// <param name="options">The <see cref="CookieAuthenticationOptions"/>.</param>
        public TenantCookieAuthenticationHandler(CookieAuthenticationOptions options)
        {
            //// We must set the options again with the resolved expiration time.
            //// Otherwise the call will be done outside the scope of an nhibernate session.
            this.Options = options;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Overridden (new) options.
        /// </summary>
        protected new CookieAuthenticationOptions Options
        {
            get;
            private set;
        }

        #endregion

        /// <inheritdoc />
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationTicket ticket = null;
            try
            {
                var cookie = this.Options.CookieManager.GetRequestCookie(Context, this.Options.CookieName);
                if (string.IsNullOrWhiteSpace(cookie))
                {
                    return null;
                }
                ticket = this.Options.TicketDataFormat.Unprotect(cookie);
                if (ticket == null)
                {
                    Log.Error("Unprotect ticket failed");
                    return null;
                }
                if (this.Options.SessionStore != null)
                {
                    var claim = ticket.Identity.Claims.FirstOrDefault(x => x.Type.Equals(SessionIdClaim));
                    if (claim == null)
                    {
                        Log.Error("SessoinId missing");
                        return null;
                    }
                    this.sessionKey = claim.Value;
                    ticket = await this.Options.SessionStore.RetrieveAsync(this.sessionKey);
                    if (ticket == null)
                    {
                        Log.Error("Identity missing in session store");
                        return null;
                    }
                }
                var currentUtc = this.Options.SystemClock.UtcNow;
                var issuedUtc  = ticket.Properties.IssuedUtc;
                var expiresUtc = ticket.Properties.ExpiresUtc;
                this.Request.Set<DateTimeOffset?>("ExpiresUtc", expiresUtc);
                if (expiresUtc != null && expiresUtc.Value < currentUtc)
                {
                    if (this.Options.SessionStore != null)
                    {
                        await this.Options.SessionStore.RemoveAsync(this.sessionKey);
                    }
                    return null;
                }
                var allowRefresh = ticket.Properties.AllowRefresh;
                if (issuedUtc != null && expiresUtc != null && this.Options.SlidingExpiration
                    && (!allowRefresh.HasValue || allowRefresh.Value))
                {
                    var timeElapsed   = currentUtc.Subtract(issuedUtc.Value);
                    var timeRemaining = expiresUtc.Value.Subtract(currentUtc);
                    if (timeRemaining < timeElapsed)
                    {
                        this.shouldRenew     = true;
                        this.renewIssuedUtc  = currentUtc;
                        this.renewExpiresUtc = currentUtc.Add(expiresUtc.Value.Subtract(issuedUtc.Value));
                        this.Request.Set<DateTimeOffset?>("ExpiresUtc", expiresUtc.Value.Add(this.Options.ExpireTimeSpan));
                    }
                }
                var context = new CookieValidateIdentityContext(this.Context, ticket, this.Options);
                await this.Options.Provider.ValidateIdentity(context);
                return new AuthenticationTicket(context.Identity, context.Properties);
            }
            catch (Exception exception)
            {
                Log.Warn(exception);
                var exceptionContext = new CookieExceptionContext(
                    this.Context,
                    this.Options,
                    CookieExceptionContext.ExceptionLocation.AuthenticateAsync, 
                    exception, 
                    ticket);
                this.Options.Provider.Exception(exceptionContext);
                if (exceptionContext.Rethrow)
                {
                    throw;
                }
                return exceptionContext.Ticket;
            }
        }

        /// <inheritdoc />
        protected override async Task ApplyResponseGrantAsync()
        {
            var signin         = this.Helper.LookupSignIn(this.Options.AuthenticationType);
            var signout        = this.Helper.LookupSignOut(this.Options.AuthenticationType, this.Options.AuthenticationMode);
            bool shouldSignin  = signin  != null;
            bool shouldSignout = signout != null;
            if (! (shouldSignin || shouldSignout || this.shouldRenew))
            {
                return;
            }
            var model = await AuthenticateAsync();
            try
            {
                var cookieOptions = new CookieOptions
                {
                    Domain   = this.Options.CookieDomain,
                    HttpOnly = this.Options.CookieHttpOnly,
                    Path     = this.Options.CookiePath ?? "/",
                };
                if (this.Options.CookieSecure == CookieSecureOption.SameAsRequest)
                {
                    cookieOptions.Secure = Request.IsSecure;
                }
                else
                {
                    cookieOptions.Secure = this.Options.CookieSecure == CookieSecureOption.Always;
                }
                if (shouldSignin)
                {
                    var signInContext = new CookieResponseSignInContext(
                        this.Context,
                        this.Options,
                        this.Options.AuthenticationType,
                        signin.Identity,
                        signin.Properties,
                        cookieOptions);
                    DateTimeOffset issuedUtc;
                    if (signInContext.Properties.IssuedUtc.HasValue)
                    {
                        issuedUtc = signInContext.Properties.IssuedUtc.Value;
                    }
                    else
                    {
                        issuedUtc = this.Options.SystemClock.UtcNow;
                        signInContext.Properties.IssuedUtc = issuedUtc;
                    }
                    if (! signInContext.Properties.ExpiresUtc.HasValue)
                    {
                        signInContext.Properties.ExpiresUtc = issuedUtc.Add(this.Options.ExpireTimeSpan);
                        Log.Debug("[ApplyResponseGrantAsync] signInContext.Properties.ExpiresUtc {0}", signInContext.Properties.ExpiresUtc);
                        this.Request.Set<DateTimeOffset?>("ExpiresUtc", signInContext.Properties.ExpiresUtc);
                    }
                    this.Options.Provider.ResponseSignIn(signInContext);
                    if (signInContext.Properties.IsPersistent)
                    {
                        var expiresUtc = signInContext.Properties.ExpiresUtc ?? issuedUtc.Add(this.Options.ExpireTimeSpan);
                        signInContext.CookieOptions.Expires = expiresUtc.ToUniversalTime().DateTime;
                        Log.Debug("[ApplyResponseGrantAsync] signInContext.Properties.IsPersistent {0}", expiresUtc);
                        this.Request.Set<DateTimeOffset?>("ExpiresUtc", expiresUtc);
                    }
                    model = new AuthenticationTicket(signInContext.Identity, signInContext.Properties);
                    if (this.Options.SessionStore != null)
                    {
                        if (this.sessionKey != null)
                        {
                            await this.Options.SessionStore.RemoveAsync(this.sessionKey);
                        }
                        this.sessionKey = await this.Options.SessionStore.StoreAsync(model);
                        var identity = new ClaimsIdentity(
                            new[] { new Claim(SessionIdClaim, this.sessionKey) },
                            this.Options.AuthenticationType);
                        model = new AuthenticationTicket(identity, null);
                    }
                    var cookieValue = this.Options.TicketDataFormat.Protect(model);
                    this.Options.CookieManager.AppendResponseCookie(
                        this.Context,
                        this.Options.CookieName,
                        cookieValue,
                        signInContext.CookieOptions);
                    var signedInContext = new CookieResponseSignedInContext(
                        this.Context,
                        this.Options,
                        this.Options.AuthenticationType,
                        signInContext.Identity,
                        signInContext.Properties);
                    this.Options.Provider.ResponseSignedIn(signedInContext);
                }
                else if (shouldSignout)
                {
                    if (this.Options.SessionStore != null && this.sessionKey != null)
                    {
                        await this.Options.SessionStore.RemoveAsync(this.sessionKey);
                    }
                    var context = new CookieResponseSignOutContext(this.Context, this.Options, cookieOptions);
                    this.Options.Provider.ResponseSignOut(context);
                    this.Options.CookieManager.DeleteCookie(this.Context, this.Options.CookieName, context.CookieOptions);
                }
                else if (this.shouldRenew)
                {
                    model.Properties.IssuedUtc  = this.renewIssuedUtc;
                    model.Properties.ExpiresUtc = this.renewExpiresUtc;
                    if (this.Options.SessionStore != null && this.sessionKey != null)
                    {
                        Log.Debug("[ApplyResponseGrantAsync] shouldRenew {0}", this.renewExpiresUtc);
                        this.Request.Set<DateTimeOffset?>("ExpiresUtc", this.renewExpiresUtc);
                        await this.Options.SessionStore.RenewAsync(this.sessionKey, model);
                        var identity = new ClaimsIdentity(
                            new[] { new Claim(SessionIdClaim, this.sessionKey) },
                            this.Options.AuthenticationType);
                        model = new AuthenticationTicket(identity, null);
                    }
                    var cookieValue = this.Options.TicketDataFormat.Protect(model);
                    if (model.Properties.IsPersistent)
                    {
                        Log.Debug("[ApplyResponseGrantAsync] shouldRenew IsPersistent {0}", this.renewExpiresUtc);
                        cookieOptions.Expires = this.renewExpiresUtc.ToUniversalTime().DateTime;
                        this.Request.Set<DateTimeOffset?>("ExpiresUtc", cookieOptions.Expires);
                    }
                    this.Options.CookieManager.AppendResponseCookie(
                        this.Context,
                        this.Options.CookieName,
                        cookieValue,
                        cookieOptions);
                }
                Response.Headers.Set("Cache-Control", "no-cache");
                Response.Headers.Set("Pragma",        "no-cache");
                Response.Headers.Set("Expires",       "-1");
                bool shouldLoginRedirect  = shouldSignin  && this.Options.LoginPath.HasValue  && this.Request.Path == this.Options.LoginPath;
                bool shouldLogoutRedirect = shouldSignout && this.Options.LogoutPath.HasValue && this.Request.Path == this.Options.LogoutPath;
                if ((shouldLoginRedirect || shouldLogoutRedirect) && Response.StatusCode == 200)
                {
                    var query = this.Request.Query;
                    var redirectUri = query.Get(this.Options.ReturnUrlParameter);
                    if (! string.IsNullOrWhiteSpace(redirectUri) && this.IsHostRelative(redirectUri))
                    {
                        var redirectContext = new CookieApplyRedirectContext(this.Context, this.Options, redirectUri);
                        this.Options.Provider.ApplyRedirect(redirectContext);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Warn(exception);
                var exceptionContext = new CookieExceptionContext(
                    this.Context,
                    this.Options,
                    CookieExceptionContext.ExceptionLocation.ApplyResponseGrant, 
                    exception, 
                    model);
                this.Options.Provider.Exception(exceptionContext);
                if (exceptionContext.Rethrow)
                {
                    throw;
                }
            }
        }

        /// <inheritdoc />
        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401 || !this.Options.LoginPath.HasValue)
            {
                return Task.FromResult(0);
            }
            var challenge = Helper.LookupChallenge(this.Options.AuthenticationType, this.Options.AuthenticationMode);
            try
            {
                if (challenge != null)
                {
                    var loginUri = challenge.Properties.RedirectUri;
                    if (string.IsNullOrWhiteSpace(loginUri))
                    {
                        var currentUri =
                            Request.PathBase +
                            Request.Path +
                            Request.QueryString;
                        loginUri =
                            Request.Scheme +
                            Uri.SchemeDelimiter +
                            Request.Host +
                            Request.PathBase +
                            this.Options.LoginPath +
                            new QueryString(this.Options.ReturnUrlParameter, currentUri);
                    }
                    var redirectContext = new CookieApplyRedirectContext(this.Context, this.Options, loginUri);
                    this.Options.Provider.ApplyRedirect(redirectContext);
                }
            }
            catch (Exception exception)
            {
                Log.Warn(exception);
                var exceptionContext = new CookieExceptionContext(
                    this.Context,
                    this.Options,
                    CookieExceptionContext.ExceptionLocation.ApplyResponseChallenge, 
                    exception, 
                    ticket: null);
                this.Options.Provider.Exception(exceptionContext);
                if (exceptionContext.Rethrow)
                {
                    throw;
                }
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns whether or not the host is a relative path or not.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>True if path is relative; otherwise false.</returns>
        private bool IsHostRelative(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (path.Length == 1)
            {
                return path[0] == '/';
            }
            return path[0] == '/' && path[1] != '/' && path[1] != '\\';
        }
    }
}