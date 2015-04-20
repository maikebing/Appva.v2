// <copyright file="PersistenceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Data;
    using Appva.Apis.TenantServer.Legacy;
    using Appva.Core.Configuration;
    using Appva.Mcss.Admin.Application.Persistence;
    using Appva.Persistence;
    using Appva.Persistence.Autofac;
    using Appva.Persistence.MultiTenant;
    using Autofac;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class PersistenceConfiguration
    {
        /// <summary>
        /// The persistence configuration path.
        /// </summary>
        private const string ConfigurationPath = "App_Data\\Persistence.config";

        /// <summary>
        /// Registers the persistence module.
        /// </summary>
        /// <param name="builder">The <see cref="ContainerBuilder"/></param>
        public static void Register(ContainerBuilder builder)
        {
            /*var datasource = new MultiTenantDatasource(TenantWcfClient.CreateNew(null), new MultiTenantDatasourceConfiguration {
                Assembly = "Appva.Mcss.Admin.Domain"
            }, new DefaultDatasourceExceptionHandler());
            var persistence = new MultiTenantPersistenceContextAwareResolver(datasource);

            builder.Register(x => persistence).As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerRequest();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerLifetimeScope().OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();*/

            /*var configuration = ConfigurableApplicationContext.Read<DefaultDatasourceConfiguration>().From(ConfigurationPath).AsMachineNameSpecific().ToObject();
            var datasource = new DefaultDatasource(configuration, new DefaultDatasourceExceptionHandler(), new DefaultDatasourceEventInterceptor());
            builder.Register(x => datasource).As<IDefaultDatasource>().SingleInstance();
            builder.RegisterType<DefaultPersistenceContextAwareResolver>().As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerLifetimeScope().OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();*/
        }
    }
}