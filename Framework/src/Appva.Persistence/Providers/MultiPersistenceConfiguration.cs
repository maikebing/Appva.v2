﻿// <copyright file="MultiPersistenceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Providers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using NHibernate;

    #endregion

    /// <summary>
    /// A multi tenancy configuration implementation of a <see cref="PersistenceConfiguration"/>.
    /// </summary>
    public sealed class MultiPersistenceConfiguration : PersistenceConfiguration
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
        /// The tenant server URI.
        /// </summary>
        public Uri TenantServerUrl
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
            return new MultiTenantPersistenceContextFactory(this.TenantServerUrl, this.Assembly, this.Properties);
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
            /// The tenant server URI.
            /// </summary>
            private readonly Uri uri;

            /// <summary>
            /// Assembly of which the NHibernate entities resides.
            /// </summary>
            private readonly string assembly;

            /// <summary>
            /// Persistence unit properties, i.e. NHibernate properties..
            /// </summary>
            /// <remarks>Optional</remarks>
            private readonly IDictionary<string, string> properties;

            #endregion

            #region Constructor.

            public MultiTenantPersistenceContextFactory(Uri uri, string assembly, IDictionary<string, string> properties)
            {
                this.uri = uri;
                this.assembly = assembly;
                this.properties = properties;
            }

            #endregion

            #region PersistenceContextFactory Overrides.

            /// <inheritdoc />
            public override ISessionFactory SessionFactory
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            #endregion
        }

        #endregion
    }
}