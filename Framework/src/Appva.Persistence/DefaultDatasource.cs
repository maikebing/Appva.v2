﻿// <copyright file="DefaultDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using JetBrains.Annotations;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// Default single database <see cref="IDatasource"/>.
    /// </summary>
    public interface IDefaultDatasource : IDatasource
    {
        /// <summary>
        /// The <see cref="ISessionFactory"/> instance.
        /// </summary>
        ISessionFactory SessionFactory
        {
            get;
        }
    }

    /// <summary>
    /// Single database data source implementation of <see cref="IDefaultDatasource"/>.
    /// </summary>
    public sealed class DefaultDatasource : Datasource, IDefaultDatasource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IDefaultDatasourceConfiguration"/> instance.
        /// </summary>
        private readonly IDefaultDatasourceConfiguration configuration;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDatasource"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IDefaultDatasourceConfiguration"/></param>
        public DefaultDatasource([NotNull] IDefaultDatasourceConfiguration configuration)
        {
            Requires.NotNull(configuration, "configuration");
            this.configuration = configuration;
        }

        #endregion

        #region IDefaultDatasource Members.

        /// <inheritdoc />
        public ISessionFactory SessionFactory
        {
            get;
            private set;
        }

        #endregion

        #region Datasource Overrides.

        /// <inheritdoc />
        public override IDatasourceResult Connect()
        {
            this.SessionFactory = this.Build(PersistenceUnit.CreateNew(
                    this.configuration.ConnectionString,
                    this.configuration.Assembly,
                    this.configuration.Properties));
            return null;
        }

        #endregion
    }
}