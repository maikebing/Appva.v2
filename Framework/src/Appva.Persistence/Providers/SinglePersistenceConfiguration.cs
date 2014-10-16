// <copyright file="SinglePersistenceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Providers
{
    #region Imports.

    using System.Collections.Generic;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// A single tenant configuration implementation of a <see cref="PersistenceConfiguration"/>.
    /// </summary>
    public sealed class SinglePersistenceConfiguration : PersistenceConfiguration
    {
        #region Public Properties.

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

        /// <inheritdoc/>
        public override IPersistenceContextFactory Build()
        {
            return new DefaultPersistenceContextFactory(new SingleDatasource(new PersistenceUnit(this.ConnectionString, this.Assembly, this.Properties)));
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// Private single tenancy persistence context factory.
        /// </summary>
        private sealed class DefaultPersistenceContextFactory : PersistenceContextFactory
        {
            #region Variables.

            /// <summary>
            /// The <see cref="ISessionFactory"/>.
            /// </summary>
            private readonly ISessionFactory sessionFactory;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultPersistenceContextFactory"/> class.
            /// </summary>
            /// <param name="datasource">The <see cref="ISingleDatasource"/></param>
            public DefaultPersistenceContextFactory(ISingleDatasource datasource)
            {
                Requires.NotNull(datasource, "datasource");
                this.sessionFactory = datasource.Build();
            }

            #endregion

            #region PersistenceContextFactory Overrides.

            /// <inheritdoc />
            public override ISessionFactory SessionFactory
            {
                get
                {
                    return this.sessionFactory;
                }
            }

            #endregion
        }

        #endregion
    }
}
