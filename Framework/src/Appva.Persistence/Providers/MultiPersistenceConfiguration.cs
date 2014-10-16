// <copyright file="MultiPersistenceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Providers
{
    #region Imports.

    using System;
    using NHibernate;

    #endregion

    /// <summary>
    /// A multi tenancy configuration implementation of a <see cref="PersistenceConfiguration"/>.
    /// </summary>
    public sealed class MultiPersistenceConfiguration : PersistenceConfiguration
    {
        #region ConfigurablePersistenceContext Overrides.

        /// <inheritdoc />
        public override IPersistenceContextFactory Build()
        {
            return new MultiTenantPersistenceContextFactory();
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// Private multi tenancy persistence context factory.
        /// </summary>
        private sealed class MultiTenantPersistenceContextFactory : PersistenceContextFactory
        {
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