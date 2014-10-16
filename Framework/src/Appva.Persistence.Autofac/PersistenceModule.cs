// <copyright file="PersistenceModule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Autofac
{
    #region Imports.

    using System.Data;
    using global::Autofac;
    using Core.Configuration;
    using Providers;
    using Validation;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PersistenceModule : Module
    {
        #region Public Properties.

        /// <summary>
        /// Returns or sets the persistence configuration
        /// file path.
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }

        #endregion

        #region Module Overrides.

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            Requires.NotNull(builder, "builder");
            var persistenceContextFactory = ConfigurableApplicationContext.Read<SinglePersistenceConfiguration>().From("App_Data\\Persistence.json").ToObject().Build();
            builder.Register(x => persistenceContextFactory).As<IPersistenceContextFactory>().SingleInstance();
            builder.Register(x => x.Resolve<IPersistenceContextFactory>().Build()).As<IPersistenceContext>().InstancePerLifetimeScope().OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();
        }

        #endregion
    }
}