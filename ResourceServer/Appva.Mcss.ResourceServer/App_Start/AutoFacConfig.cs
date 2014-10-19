// <copyright file="AutoFacConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer
{
    #region Imports.

    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Appva.Core.Configuration;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Appva.Mcss.ResourceServer.Domain.Services;
    using Appva.Persistence.Autofac;
    using Appva.Persistence.Providers;
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
            var applicationConfiguration = ConfigurableApplicationContext.Read<ResourceServerConfiguration>()
                .From("App_Data\\Application.config").AsMachineNameSpecific().ToObject();
            var persistenceConfiguration = ConfigurableApplicationContext.Read<SinglePersistenceConfiguration>()
                .From("App_Data\\Persistence.config").AsMachineNameSpecific().ToObject();
            ConfigurableApplicationContext.Add<ResourceServerConfiguration>(applicationConfiguration);
            builder.RegisterModule(new PersistenceModule(persistenceConfiguration));
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(PagingAndSortingRepository<>)).As(typeof(IPagingAndSortingRepository<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(Repository<>)).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(assembly).Where(x => x.GetInterfaces().Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}