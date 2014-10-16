// <copyright file="SingleDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence
{
    #region Imports.

    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// A single data source.
    /// </summary>
    public interface ISingleDatasource
    {
        /// <summary>
        /// Builds a single <see cref="ISessionFactory"/>.
        /// </summary>
        /// <returns>A single <see cref="ISessionFactory"/></returns>
        ISessionFactory Build();
    }

    /// <summary>
    /// Implementation of <see cref="ISingleDatasource"/>. A representation of a single
    /// data base access point.
    /// </summary>
    public class SingleDatasource : Datasource, ISingleDatasource
    {
        #region Variables.

        /// <summary>
        /// A <see cref="IPersistenceUnit"/>.
        /// </summary>
        private readonly IPersistenceUnit persistenceUnit;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleDatasource"/> class.
        /// </summary>
        /// <param name="persistenceUnit">The <see cref="IPersistenceUnit"/></param>
        public SingleDatasource(IPersistenceUnit persistenceUnit) 
        {
            Requires.NotNull(persistenceUnit, "persistenceUnit");
            this.persistenceUnit = persistenceUnit;
        }

        #endregion

        #region ISingleDatasource Members.

        /// <inheritdoc />
        public ISessionFactory Build()
        {
            return this.BuildSessionFactory(this.persistenceUnit);
        }

        #endregion
    }
}