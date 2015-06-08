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
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Configuration;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mvc.Messaging;
    using Appva.Siths;
    using Appva.Siths.Security;
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
            builder.RegisterFilterProvider();

            //// Register repositories.
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IRepository)))).AsImplementedInterfaces().InstancePerRequest();

            //// Register services.
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<TenantService>().As<ITenantService>().SingleInstance();

            //// Global tenant cache registration
            builder.RegisterType<TenantAwareMemoryCache>().As<ITenantAwareMemoryCache>().SingleInstance();

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

            //// SITHS
            builder.Register<SithsClient>(x => new SithsClient(new AuthifyWtfTokenizer())).As<ISithsClient>().SingleInstance();

            builder.RegisterModule(PersistenceModule.CreateNew());
            builder.RegisterModule(MessagingModule.CreateNew());
            builder.RegisterModule(ExceptionHandlerModule.CreateNew());
            
            //// Cache per tenant?
            //// http://docs.autofac.org/en/latest/advanced/multitenant.html#resolve-tenant-specific-dependencies
            builder.RegisterModule(new AutofacWebTypesModule());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}