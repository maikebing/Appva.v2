// <copyright file="OwinConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Reflection;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Configuration;
    using Autofac;
    using Autofac.Integration.Mvc;
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
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(assembly);
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterFilterProvider();
            builder.RegisterEnvironment();
            builder.RegisterNhibernateProfiler();
            builder.RegisterRepositories();
            builder.RegisterServices();
            builder.RegisterTransformers();
            builder.RegisterTenantServices();
            builder.RegisterOwinContext();
            builder.RegisterIdentityServices();
            builder.RegisterMediator();
            builder.RegisterAuthorization(app);
            builder.RegisterAuthify();
            builder.RegisterPersistence();
            builder.RegisterEmailMessaging();
            builder.RegisterExceptionHandling();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}