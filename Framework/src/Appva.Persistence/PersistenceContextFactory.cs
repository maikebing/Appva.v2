// <copyright file="PersistenceContextFactory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence
{
    #region Imports.

    using Common.Logging;
    using NHibernate;

    #endregion

    /// <summary>
    /// Persistence context (<see cref="ISession"/>) provider interface. 
    /// An NHibernate <see cref="ISessionFactory"/> factory.
    /// </summary>
    public interface IPersistenceContextFactory
    {
        /// <summary>
        /// Returns the current <see cref="ISessionFactory"/>.
        /// </summary>
        ISessionFactory SessionFactory
        {
            get;
        }

        /// <summary>
        /// Builds an <see cref="IPersistenceContext"/>.
        /// </summary>
        /// <returns>An <see cref="IPersistenceContext"/></returns>
        IPersistenceContext Build();
    }

    /// <summary>
    /// Implementation of <see cref="IPersistenceContextFactory"/>.
    /// </summary>
    public abstract class PersistenceContextFactory : IPersistenceContextFactory
    {
        #region Contructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextFactory"/> class.
        /// </summary>
        protected PersistenceContextFactory()
        {
        }

        #endregion

        #region IPersistenceContextFactory Members.

        /// <inheritdoc />
        public abstract ISessionFactory SessionFactory
        {
            get;
        }

        /// <inheritdoc />
        public virtual IPersistenceContext Build()
        {
            return new PersistenceContext(this);
        }

        #endregion
    }
}