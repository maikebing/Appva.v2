// <copyright file="MultiTenantDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

using System.Linq;

namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System.Collections.Generic;
    using Apis.TenantServer;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MultiTenantDatasource : Datasource, IMultiTenantDatasource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMultiTenantDatasourceConfiguration"/> instance.
        /// </summary>
        private readonly IMultiTenantDatasourceConfiguration configuration;

        /// <summary>
        /// The <see cref="ITenantClient"/> instance.
        /// </summary>
        private readonly ITenantClient client;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantDatasource"/> class.
        /// </summary>
        /// <param name="client">The <see cref="ITenantClient"/></param>
        /// <param name="configuration">The <see cref="IMultiTenantDatasourceConfiguration"/></param>
        /// <param name="exceptionHandler">The <see cref="IDatasourceExceptionHandler"/></param>
        /// <param name="eventInterceptor">The <see cref="IDatasourceEventInterceptor"/></param>
        public MultiTenantDatasource(
            ITenantClient client,
            IMultiTenantDatasourceConfiguration configuration,
            IDatasourceExceptionHandler exceptionHandler,
            IDatasourceEventInterceptor eventInterceptor)
            : base(exceptionHandler, eventInterceptor)
        {
            Requires.NotNull(configuration, "configuration");
            Requires.NotNull(client, "client");
            this.configuration = configuration;
            this.client = client;
        }

        #endregion

        #region IMultiTenantDatasource Members.

        /// <inheritdoc />
        public IDictionary<string, ISessionFactory> SessionFactories
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public ISessionFactory Lookup(string key)
        {
            if (! this.SessionFactories.ContainsKey(key))
            {
                return null;
            }
            ISessionFactory retval;
            this.SessionFactories.TryGetValue(key, out retval);
            return retval;
        }

        #endregion

        #region Datasource Implementation.

        /// <inheritdoc />
        public override void Connect()
        {
            var tenants = this.client.ListAll();
            Requires.ValidState(tenants.Count > 0, "No Tenants found!");
            var units = tenants.Select(x => new PersistenceUnit(x.ConnectionString, this.configuration.Assembly, this.configuration.Properties, x.Id)).ToList();
            this.SessionFactories = this.Build(units);
        }

        #endregion
    }
}