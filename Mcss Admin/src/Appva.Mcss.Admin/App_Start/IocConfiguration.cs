// <copyright file="IocConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Caching.Providers;
    using Appva.Core.Exceptions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Configuration;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mvc.Messaging;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Microsoft.Owin;
    using RazorEngine.Configuration;

    #endregion

    /// <summary>
    /// IoC configuration.
    /// </summary>
    internal static class IocConfiguration
    {
        /// <summary>
        /// Configures IoC container. 
        /// </summary>
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            
            builder.RegisterControllers(assembly);
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            //// Register repositories.
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IRepository)))).AsImplementedInterfaces().InstancePerRequest();

            //// Register services.
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();

            

            //// Global cache registration
            var cache = new RuntimeMemoryCache("https://schemas.appva.se/2015/04/cache/admin");
            builder.Register<RuntimeMemoryCache>(x => cache).As<IRuntimeMemoryCache>().SingleInstance();

            //// Owin registration
            builder.Register(x => HttpContext.Current.GetOwinContext()).As<IOwinContext>();
            builder.Register(x =>
            {
                return new IdentityService(x.Resolve<IOwinContext>().Environment);
            }).As<IIdentityService>().InstancePerLifetimeScope();

            //// Authentication registration
            builder.RegisterType<FormsAuthentication>().As<IFormsAuthentication>().InstancePerRequest();

            //// Mediator registration
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorDependencyResolver>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //// Persistence registration
            builder.RegisterType<DefaultExceptionHandler>().As<IExceptionHandler>();

            builder.RegisterModule(PersistenceModule.CreateNew());

            //// Email
            builder.Register(x => new TemplateServiceConfiguration
            {
                TemplateManager = new CshtmlTemplateManager("Features/Shared/EmailTemplates")
            }).As<ITemplateServiceConfiguration>().SingleInstance();
            builder.RegisterType<MailService>().As<IRazorMailService>().SingleInstance();
            //// Cache per tenant?
            //// http://docs.autofac.org/en/latest/advanced/multitenant.html#resolve-tenant-specific-dependencies
            builder.RegisterModule(new AutofacWebTypesModule());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}