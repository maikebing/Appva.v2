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
    using System.Web;
    using Appva.Apis.TenantServer.Legacy;
    using Appva.Caching.Providers;
    using Appva.Core.Exceptions;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Infrastructure;
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
            builder.RegisterType<TenantWcfClient>().As<ITenantClient>().SingleInstance();
            builder.Register<MultiTenantDatasourceConfiguration>(x => new MultiTenantDatasourceConfiguration
            {
                Assembly = Assembly
            }).As<IMultiTenantDatasourceConfiguration>().SingleInstance();
            builder.RegisterType<MultiTenantDatasource>().As<IMultiTenantDatasource>().SingleInstance();
            builder.RegisterType<MultiTenantPersistenceContextAwareResolver>().As<IPersistenceContextAwareResolver>().SingleInstance().AutoActivate();
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AuthorizeTenantIdentity>().As<IAuthorizeTenantIdentity>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerLifetimeScope()
                .OnActivated(x => 
                    {
                        var authorization = x.Context.Resolve<IAuthorizeTenantIdentity>();
                        var request = x.Context.Resolve<HttpRequestBase>();
                        if (authorization.Validate(request).IsAuthorized)
                        {
                            x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted);
                        }
                    });
            
        }

        #endregion
    }
}