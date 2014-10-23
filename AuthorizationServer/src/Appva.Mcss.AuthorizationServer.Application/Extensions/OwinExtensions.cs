// <copyright file="OwinExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loginPath"></param>
        public static void UseAuthorizationServerOwin(this IAppBuilder app, string loginPath)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AuthenticationType = AuthorizationServerOwinConstants.AuthenticationType,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString(loginPath)
            });
        }
    }
}