// <copyright file="AuthenticationConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Security;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Owin;

    #endregion

    /// <summary>
    /// The authentication configuration.
    /// </summary>
    internal static class AuthenticationConfiguration
    {
        /// <summary>
        /// Registers the OWIN authentication pipelines.
        /// <externalLink>
        ///     <linkText>OWIN Authentication</linkText>
        ///     <linkUri>
        ///         http://go.microsoft.com/fwlink/?LinkId=301864
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/></param>
        public static void Register(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = AuthenticationType.Administrative.Value,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString("/auth/sign-in")
            });
        }
    }
}