// <copyright file="ProductionConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using Owin;

    #endregion

    /// <summary>
    /// Owin configuration.
    /// </summary>
    /// <example>
    /// <code language="xml" title="Example Configuration">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8"?>
    /// <appSettings>  
    ///    <add key="owin:appStartup" value="OwinConfiguration" />       
    /// </appSettings>
    /// ]]>
    /// </code>
    /// </example>
    internal sealed class OwinConfiguration
    {
        /// <summary>
        /// Configures Owin.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/></param>
        public void Configuration(IAppBuilder app)
        {
            IocConfiguration.Configure(app);
            AuthenticationConfiguration.Configure(app);
        }
    }
}