// <copyright file="DefaultDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System;
    using NHibernate;
    using Validation;

    #endregion

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
        /// <param name="exceptionHandler">The <see cref="IDatasourceExceptionHandler"/></param>
        /// <param name="eventInterceptor">The <see cref="IDatasourceEventInterceptor"/></param>
        public DefaultDatasource(
            IDefaultDatasourceConfiguration configuration,
            IDatasourceExceptionHandler exceptionHandler,
            IDatasourceEventInterceptor eventInterceptor)
            : base(exceptionHandler, eventInterceptor)
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

        #region Datasource Implementation.

        /// <inheritdoc />
        public override void Connect()
        {
            this.SessionFactory = this.Build(new PersistenceUnit(
                this.configuration.ConnectionString, 
                this.configuration.Assembly, 
                this.configuration.Properties));
            if (this.SessionFactory == null)
            {
                throw new Exception("Unable to connect to database!");
            }
        }

        #endregion
    }
}