// <copyright file="TenantCookieAuthenticationMiddleware.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Middleware.Cookie
{
    #region Imports.

    using System;
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Infrastructure;

    #endregion

    /// <summary>
    /// The tenant specific cookie authentication middleware.
    /// </summary>
    public sealed class TenantCookieAuthenticationMiddleware : AuthenticationMiddleware<CookieAuthenticationOptions>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantCookieAuthenticationMiddleware>();

        /// <summary>
        /// The temporary options.
        /// </summary>
        private readonly TenantCookieAuthenticationOptions temp;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantCookieAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the owin pipeline to invoke.</param>
        /// <param name="options">The configuration options for the middleware.</param>
        public TenantCookieAuthenticationMiddleware(OwinMiddleware next, TenantCookieAuthenticationOptions options)
            : base(next, options)
        {
            //// If middleware is called in between nhibernate session then temporary store it.
            //// Otherwise, if within a session it will call CreateHandler.
            this.temp = options;
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
                Log.Warn(ex);
                return;
            }
        }

        #endregion

        #region AuthenticationMiddleware Overrides.

        /// <inheritdoc />
        protected override AuthenticationHandler<CookieAuthenticationOptions> CreateHandler()
        {
            return new TenantCookieAuthenticationHandler(this.temp.CreateNewCookieAuthenticationOptions());
        }

        #endregion
    }
}