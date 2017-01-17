// <copyright file="TenantCookieAuthenticationOptions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Middleware.Cookie
{
    #region Imports.

    using Appva.Core.Logging;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Microsoft.Owin;
    using Microsoft.Owin.Infrastructure;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.DataHandler;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantCookieAuthenticationOptions : CookieAuthenticationOptions
    {
        #region Variables.

        /// <summary>
        /// The sign in path.
        /// </summary>
        /// <remarks>Added as static to prevent new PathStrings on each request</remarks>
        private static readonly PathString SignInPath  = new PathString("/auth/sign-in");

        /// <summary>
        /// The sign out path.
        /// </summary>
        /// <remarks>Added as static to prevent new PathStrings on each request</remarks>
        private static readonly PathString SignOutPath = new PathString("/auth/sign-out");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantCookieAuthenticationOptions"/> class.
        /// </summary>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="provider">The <see cref="ICookieAuthenticationProvider"/>.</param>
        /// <param name="ticketDataFormat">The <see cref="ISecureDataFormat{AuthenticationTicket}"/>.</param>
        /// <param name="cookieManager">The <see cref="ICookieManager"/>.</param>
        public TenantCookieAuthenticationOptions(
            ISettingsService settingsService,
            ICookieAuthenticationProvider provider,
            TicketDataFormat ticketDataFormat,
            ICookieManager cookieManager)
        {
            this.Settings           = settingsService;
            this.CookieManager      = cookieManager;
            this.Provider           = provider;
            this.TicketDataFormat   = ticketDataFormat;
            this.AuthenticationMode = AuthenticationMode.Active;
            this.AuthenticationType = Security.AuthenticationType.Administrative.Value;
            this.CookieSecure       = CookieSecureOption.SameAsRequest;
            this.LoginPath          = SignInPath;
            this.LogoutPath         = SignOutPath;
            this.SlidingExpiration  = true;
            this.CookieName         = CookieAuthenticationDefaults.CookiePrefix + this.AuthenticationType;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        public ISettingsService Settings
        {
            get;
            private set;
        }

        #endregion

        #region Public Members.

        /// <summary>
        /// Creates a copy with cookie expiration time span.
        /// </summary>
        /// <returns>A copy of <see cref="TenantCookieAuthenticationOptions"/>.</returns>
        public CookieAuthenticationOptions CreateNewCookieAuthenticationOptions()
        {
            return new CookieAuthenticationOptions
                {
                    AuthenticationMode = this.AuthenticationMode,
                    AuthenticationType = this.AuthenticationType,
                    CookieDomain       = this.CookieDomain,
                    CookieHttpOnly     = this.CookieHttpOnly,
                    CookieManager      = this.CookieManager,
                    CookieName         = this.CookieName,
                    CookiePath         = this.CookiePath,
                    CookieSecure       = this.CookieSecure,
                    Description        = this.Description,
                    ExpireTimeSpan     = this.Settings.GetCookieExpiration(),
                    LoginPath          = this.LoginPath,
                    LogoutPath         = this.LogoutPath,
                    Provider           = this.Provider,
                    ReturnUrlParameter = this.ReturnUrlParameter,
                    SessionStore       = this.SessionStore,
                    SlidingExpiration  = this.SlidingExpiration,
                    SystemClock        = this.SystemClock,
                    TicketDataFormat   = this.TicketDataFormat
                };
        }

        #endregion
    }
}