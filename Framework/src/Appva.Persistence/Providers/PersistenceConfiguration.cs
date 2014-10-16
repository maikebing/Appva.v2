// <copyright file="PersistenceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence
{
    #region Imports.

    using Core.Configuration;
    using NHibernate;

    #endregion

    /// <summary>
    /// A Persistence context interface, or an NHibernate <see cref="ISessionFactory"/> wrapper.
    /// </summary>
    public interface IPersistenceConfiguration : IConfigurableResource
    {
        /// <summary>
        /// Builds a persistence context factory.
        /// </summary>
        /// <returns>A <see cref="IPersistenceContextFactory"/></returns>
        IPersistenceContextFactory Build();
    }

    /// <summary>
    /// Implementation of <see cref="IPersistenceConfiguration"/>.
    /// </summary>
    public abstract class PersistenceConfiguration : IPersistenceConfiguration
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceConfiguration" /> class.
        /// </summary>
        protected PersistenceConfiguration()
        {
        }

        #endregion

        #region IPersistenceConfiguration Members.

        /// <inheritdoc />
        public abstract IPersistenceContextFactory Build();

        #endregion
    }
}