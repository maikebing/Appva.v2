// <copyright file="MultiTenantPersistenceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Appva.Core.Extensions;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Persistence;
    using Appva.Persistence.Providers;
    using NHibernate;

    #endregion

    /// <summary>
    /// A multi tenancy configuration implementation of a <see cref="PersistenceConfiguration"/>.
    /// </summary>
    public sealed class MultiTenantPersistenceConfiguration : PersistenceConfiguration
    {
        #region Public Properties.

        /// <summary>
        /// Skips multi-tenant and instead 
        /// creates a <see cref="SinglePersistenceConfiguration"/>.
        /// </summary>
        public bool SkipMultiTenant
        {
            get;
            set;
        }

        /// <summary>
        /// A database connection string.
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Assembly of which the NHibernate entities resides.
        /// </summary>
        public string Assembly
        {
            get;
            set;
        }

        /// <summary>
        /// Persistence unit properties, i.e. NHibernate properties..
        /// </summary>
        /// <remarks>Optional</remarks>
        public IDictionary<string, string> Properties
        {
            get;
            set;
        }

        #endregion

        #region ConfigurablePersistenceContext Overrides.

        /// <inheritdoc />
        public override IPersistenceContextFactory Build()
        {
            if (this.SkipMultiTenant)
            {
                return new SinglePersistenceConfiguration
                {
                    ConnectionString = this.ConnectionString,
                    Assembly = this.Assembly,
                    Properties = this.Properties
                }.Build();
            }
            return new MultiTenantPersistenceContextFactory(this.Assembly, this.Properties);
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// Private multi tenancy persistence context factory.
        /// </summary>
        private sealed class MultiTenantPersistenceContextFactory : PersistenceContextFactory
        {
            #region Variables.

            /// <summary>
            /// Assembly of which the NHibernate entities resides.
            /// </summary>
            private readonly string assembly;

            /// <summary>
            /// Persistence unit properties, i.e. NHibernate properties..
            /// </summary>
            /// <remarks>Optional</remarks>
            private readonly IDictionary<string, string> properties;

            /// <summary>
            /// The <see cref="ITenantService"/>.
            /// </summary>
            private readonly ITenantService tenantService = new TenantService();

            /// <summary>
            /// The key value <see cref="ISessionFactory"/> cache.
            /// </summary>
            private readonly IDictionary<object, ISessionFactory> sessionFactories;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="MultiTenantPersistenceContextFactory"/> class.
            /// </summary>
            /// <param name="assembly">The assembly</param>
            /// <param name="properties">The properties</param>
            public MultiTenantPersistenceContextFactory(string assembly, IDictionary<string, string> properties)
            {
                this.assembly = assembly;
                this.properties = properties;
                var tenants = this.tenantService.ListTenants();
                if (tenants.IsEmpty())
                {
                    throw new Exception("Zero tenants to load!");
                }
                var dic = new Dictionary<string, IPersistenceUnit>();
                foreach (var tenant in tenants)
                {
                    dic.Add(tenant.id, new PersistenceUnit(tenant.connection_string, this.assembly, this.properties));
                }
                this.sessionFactories = new MultiDataSource(dic).Build();
            }

            #endregion

            #region PersistenceContextFactory Overrides.

            /// <inheritdoc />
            public override ISessionFactory SessionFactory
            {
                get
                {
                    if (HttpContext.Current.IsNotNull())
                    {
                        var tenantId = HttpContext.Current.User.Identity.Tenant();
                        if (this.sessionFactories.ContainsKey(tenantId.ToString()))
                        {
                            return this.sessionFactories[tenantId.ToString()];
                        }
                    }
                    throw new Exception("No HttpContext or no tenant!");
                }
            }

            #endregion
        }

        #endregion
    }
}