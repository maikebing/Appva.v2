// <copyright file="AutoFacConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer
{
    #region Imports.

    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Apis.TenantServer;
    using Application.Configuration;
    using Application.ExceptionHandling;
    using Application.Persistence;
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
            builder.RegisterType<NoOpPushNotification>().As<IPushNotification>().SingleInstance();
        }

        /// <summary>
        /// Configures persistence.
        /// </summary>
        /// <param name="builder">The <see cref="ContainerBuilder"/></param>
        private static void ConfigurePersistence(ContainerBuilder builder)
        {
            var messaging = new EmailService();
            var tenantServerUri = ConfigurableApplicationContext.Get<ResourceServerConfiguration>().TenantServerUri;
            var configuration = ConfigurableApplicationContext.Read<MultiTenantDatasourceConfiguration>().From(PersistenceConfig).AsMachineNameSpecific().ToObject();
            var client = TenantClient.CreateNew(tenantServerUri);
            var datasource = new MultiTenantDatasource(client, configuration, new DatasourceEmailExceptionHandler(messaging), new DefaultDatasourceEventInterceptor());
            var persistence = new TenantIdentityPersistenceContextAwareResolver(datasource);
            builder.Register(x => client).As<ITenantClient>().SingleInstance();
            builder.Register(x => persistence).As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerRequest();
            builder.Register(x => new PersistenceAttribute(x.Resolve<IPersistenceContext>())).AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
        }
    }
}