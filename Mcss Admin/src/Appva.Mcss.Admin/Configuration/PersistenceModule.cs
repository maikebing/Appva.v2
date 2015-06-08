// <copyright file="PersistenceModule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System.Data;
    using Appva.Apis.TenantServer.Legacy;
    using Appva.Caching.Providers;
    using Appva.Core.Exceptions;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Persistence;
    using Appva.Persistence.Autofac;
    using Appva.Persistence.MultiTenant;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Autofac;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PersistenceModule : Module
    {
        #region Variables.

        /// <summary>
        /// The persistence assembly.
        /// </summary>
        private const string Assembly = "Appva.Mcss.Admin.Domain";

        /// <summary>
        /// The cache bucket key.
        /// </summary>
        private const string BucketKey = "https://schemas.appva.se/2015/04/cache/db/admin";

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceModule"/> class.
        /// </summary>
        private PersistenceModule()
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PersistenceModule"/> class.
        /// </summary>
        /// <returns>A new <see cref="PersistenceModule"/> instance</returns>
        public static PersistenceModule CreateNew()
        {
            return new PersistenceModule();
        }

        #endregion

        #region Module Overrides.

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            switch (Configuration.Application.OperationalEnvironment)
            {
                case OperationalEnvironment.Production:
                    builder.RegisterType<ProductionTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
                    break;
                case OperationalEnvironment.Demo:
                    builder.RegisterType<DemoTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
                    break;
                case OperationalEnvironment.Staging:
                    builder.RegisterType<StagingTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
                    break;
                case OperationalEnvironment.Development:
                    builder.RegisterType<DevelopmentTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
                    break;
            }
            builder.Register<RuntimeMemoryCache>(x => new RuntimeMemoryCache(BucketKey)).As<IRuntimeMemoryCache>().SingleInstance();
            builder.Register(x => TenantWcfClient.CreateNew()).As<ITenantClient>().SingleInstance();
            builder.Register<MultiTenantDatasourceConfiguration>(x => new MultiTenantDatasourceConfiguration { Assembly = Assembly }).As<IMultiTenantDatasourceConfiguration>().SingleInstance();
            builder.Register<MultiTenantDatasource>(x => new MultiTenantDatasource(x.Resolve<ITenantClient>(), x.Resolve<IRuntimeMemoryCache>(), x.Resolve<IMultiTenantDatasourceConfiguration>())).As<IMultiTenantDatasource>().SingleInstance().AutoActivate();
            builder.Register<MultiTenantPersistenceContextAwareResolver>(x => new MultiTenantPersistenceContextAwareResolver(x.Resolve<ITenantIdentificationStrategy>(), x.Resolve<IMultiTenantDatasource>(), x.Resolve<IPersistenceExceptionHandler>())).As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerLifetimeScope().OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
        }

        #endregion
    }
}