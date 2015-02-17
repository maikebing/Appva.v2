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
    using Appva.Apis.TenantServer;
    using Appva.Core.Configuration;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Appva.Mcss.ResourceServer.Application.ExceptionHandling;
    using Appva.Mcss.ResourceServer.Application.Persistence;
    using Appva.Mcss.ResourceServer.Domain.Services;
    using Appva.Persistence;
    using Appva.Persistence.MultiTenant;
    using Appva.Repository;
    using Autofac;
    using Autofac.Integration.WebApi;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class AutoFacConfig
    {
        /// <summary>
        /// Configures AutoFac and Persistence. 
        /// </summary>
        public static void Configure()
        {
            var assembly = typeof(IService).Assembly;
            var builder = new ContainerBuilder();
            var configuration = ConfigurableApplicationContext.Read<ResourceServerConfiguration>()
                .From("App_Data\\Application.config").AsMachineNameSpecific().ToObject();
            ConfigurableApplicationContext.Add<ResourceServerConfiguration>(configuration);
            ConfigurePersistence(builder);
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(PagingAndSortingRepository<>)).As(typeof(IPagingAndSortingRepository<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(Repository<>)).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(assembly).Where(x => x.GetInterfaces().Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.Register(x => new PersistenceAttribute(x.Resolve<IPersistenceContext>())).AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
            builder.RegisterType<ExceptionFilterAttribute>().AsWebApiExceptionFilterFor<ApiController>().InstancePerRequest();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        private static void ConfigurePersistence(ContainerBuilder builder)
        {
            var configuration = ConfigurableApplicationContext.Read<MultiTenantDatasourceConfiguration>()
                .From("App_Data\\Persistence.config").AsMachineNameSpecific().ToObject();
            var client = new TenantClient(new TenantServerConfiguration
            {
                Uri = ConfigurableApplicationContext.Get<ResourceServerConfiguration>().TenantServerUri
            });
            var datasource = new MultiTenantDatasource(client, configuration, new DefaultDatasourceExceptionHandler(), new DefaultDatasourceEventInterceptor());
            var persistence = new TenantIdentityPersistenceContextAwareResolver(datasource);
            builder.Register(x => persistence).As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerRequest();
            builder.Register(x => client).As<ITenantClient>().SingleInstance();
        }
    }
}