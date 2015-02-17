// <copyright file="PersistenceModule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Autofac
{
    #region Imports.

    using System.Data;
    using global::Autofac;
    using Validation;

    #endregion

    /// <summary>
    /// An NHibernate session per request on application level model.
    /// </summary>
    /*public sealed class PersistenceModule : Module
    {
        #region Variables.

        /// <summary>
        /// The <see cref="PersistenceConfiguration"/>.
        /// </summary>
        private readonly PersistenceConfiguration configuration;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceModule"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="PersistenceConfiguration"/></param>
        public PersistenceModule(DefaultDatasourceConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        #endregion

        #region Module Overrides.

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            Requires.NotNull(builder, "builder");
            var persistenceContextFactory = this.configuration.Build();
            builder.Register(x => persistenceContextFactory).As<IPersistenceContextAwareResolver>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextFactory>().Build()).As<IPersistenceContext>()
                .InstancePerLifetimeScope()
                .OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();
        }

        #endregion
    }*/
}