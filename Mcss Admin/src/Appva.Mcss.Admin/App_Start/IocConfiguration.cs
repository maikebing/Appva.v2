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
    using System.Data;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Apis.TenantServer.Legacy;
    using Appva.Caching.Providers;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Persistence;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using Appva.Persistence.Autofac;
    using Appva.Persistence.MultiTenant;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Microsoft.Owin;
    using Appva.Mcss.Admin.Application.Services.Menus;
    using Appva.Tenant.Identity;
    using Appva.Apis.TenantServer;
    using Appva.Tenant.Interoperability.Client;

    #endregion

    /// <summary>
    /// IoC configuration.
    /// </summary>
    public static class IocConfiguration
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

            var client = TenantWcfClient.CreateNew();
            builder.Register(x => client).As<ITenantClient>().InstancePerRequest();

            //// Persistence registration
            builder.RegisterType<TenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
            var datasource = new MultiTenantDatasource(client, cache, new MultiTenantDatasourceConfiguration
            {
                Assembly = "Appva.Mcss.Admin.Domain"
            }, new DefaultDatasourceExceptionHandler());
            builder.Register(x => new MultiTenantPersistenceContextAwareResolver(x.Resolve<ITenantIdentificationStrategy>(), datasource)).As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerLifetimeScope().OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}