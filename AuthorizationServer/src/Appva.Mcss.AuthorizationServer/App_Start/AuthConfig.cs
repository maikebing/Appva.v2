// <copyright file="AuthConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using Application;
    using Owin;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 
        /// See <a href="http://go.microsoft.com/fwlink/?LinkId=301864">Configuration</a>.
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseAuthorizationServerOwin("/Admin/Auth/Login");
            IocConfig.Register(app, AuthorizationServerOwinConstants.AuthenticationType);
        }
    }
}