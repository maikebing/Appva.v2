// <copyright file="AutoFacConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.Monitoring
{
    #region Imports.

    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Appva.Core.Configuration;
    using Appva.Cqrs;
    using Appva.Mcss.Monitoring.Infrastructure.Attributes;
    using Appva.Persistence;
    using Appva.Persistence.Providers;
    using Autofac;
    using Autofac.Features.Variance;
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
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            //var applicationConfiguration = ConfigurableApplicationContext.Read<ResourceServerConfiguration>()
            //    .From("App_Data\\Application.config").AsMachineNameSpecific().ToObject();
            //ConfigurableApplicationContext.Add<ResourceServerConfiguration>(applicationConfiguration);
            var persistenceConfiguration = ConfigurableApplicationContext.Read<SinglePersistenceConfiguration>()
                .From("App_Data\\Persistence.config").AsMachineNameSpecific().ToObject();
            var persistenceContextFactory = persistenceConfiguration.Build();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.Register(x => persistenceContextFactory).As<IPersistenceContextFactory>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextFactory>().Build()).As<IPersistenceContext>().InstancePerRequest();
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorDependencyResolver>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            builder.RegisterApiControllers(assembly);
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.Register(x => new PersistenceAttribute(x.Resolve<IPersistenceContext>())).AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}