// <copyright file="AutoFacConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Apis.TenantServer;
    using Application.Configuration;
    using Application.ExceptionHandling;
    using Application.Persistence;
    using Appva.Azure.Messaging;
    using Appva.Caching.Providers;
    using Appva.Mcss.Domain.Entities;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Azure;
    using Controllers;
    using Core.Configuration;
    using Core.Messaging;
    using Domain.Services;
    using Persistence;
    using Persistence.MultiTenant;
    using Repository;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class AutoFacConfig
    {
        /// <summary>
        /// The application config path.
        /// </summary>
        private const string ApplicationConfig = "App_Data\\Application.config";

        /// <summary>
        /// The persistence config path.
        /// </summary>
        private const string PersistenceConfig = "App_Data\\Persistence.config";

        /// <summary>
        /// Configures AutoFac and Persistence. 
        /// </summary>
        public static void Configure()
        {
            var assembly = typeof(IService).Assembly;
            var builder = new ContainerBuilder();
            var configuration = ConfigurableApplicationContext.Read<ResourceServerConfiguration>().From(ApplicationConfig).AsMachineNameSpecific().ToObject();
            ConfigurableApplicationContext.Add<ResourceServerConfiguration>(configuration);
            ConfigurePersistence(builder);
            ConfigurePushNotification(builder);
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(PagingAndSortingRepository<>)).As(typeof(IPagingAndSortingRepository<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(Repository<>)).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(assembly).Where(x => x.GetInterfaces().Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.OverrideWebApiActionFilterFor<HealthController>(x => x.Health());
            builder.RegisterType<ExceptionFilterAttribute>().AsWebApiExceptionFilterFor<ApiController>().InstancePerRequest();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        /// <summary>
        /// Configures push notifications.
        /// </summary>
        /// <param name="builder">The <see cref="ContainerBuilder"/></param>
        private static void ConfigurePushNotification(ContainerBuilder builder)
        {
            builder.RegisterType<PushNotificationManager>().As<IPushNotification>().SingleInstance();
        }

        /// <summary>
        /// Configures persistence.
        /// </summary>
        /// <param name="builder">The <see cref="ContainerBuilder"/></param>
        private static void ConfigurePersistence(ContainerBuilder builder)
        {
            builder.RegisterType<MailService>().As<ISimpleMailService>().SingleInstance();
            builder.RegisterType<TenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
            builder.RegisterType<TenantPersistenceExceptionHandler>().As<IPersistenceExceptionHandler>().SingleInstance();
            
            var tenantServerUri = ConfigurableApplicationContext.Get<ResourceServerConfiguration>().TenantServerUri;
            var configuration = ConfigurableApplicationContext.Read<MultiTenantDatasourceConfiguration>().From(PersistenceConfig).AsMachineNameSpecific().ToObject();

            //// Setup cache for multi tenant.
            builder.Register<RuntimeMemoryCache>(x => new RuntimeMemoryCache("https://schemas.appva.se/2015/04/cache/resource")).As<IRuntimeMemoryCache>().SingleInstance();
            builder.Register(x => TenantClient.CreateNew(tenantServerUri)).As<ITenantClient>().SingleInstance();
            builder.Register<MultiTenantDatasourceConfiguration>(x => new MultiTenantDatasourceConfiguration
            {
                UseIdAsIdentifier = true,
                Properties = configuration.Properties,
                Assembly = Assembly.GetAssembly(typeof(Entity<Guid>)).FullName
            }).As<IMultiTenantDatasourceConfiguration>().SingleInstance();
            builder.RegisterType<MultiTenantDatasource>().As<IMultiTenantDatasource>().SingleInstance().AutoActivate();
            builder.RegisterType<MultiTenantPersistenceContextAwareResolver>().As<IPersistenceContextAwareResolver>().SingleInstance().AutoActivate();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerRequest();
            builder.Register(x => new PersistenceAttribute(x.Resolve<IPersistenceContext>())).AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
        }
    }
}