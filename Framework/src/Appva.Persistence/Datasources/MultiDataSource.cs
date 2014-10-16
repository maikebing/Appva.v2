// <copyright file="MultiDataSource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// A multi data source.
    /// </summary>
    public interface IMultiDataSource
    {
        /// <summary>
        /// Builds a multi data source by returning several
        /// <see cref="ISessionFactory"/> by key.
        /// </summary>
        /// <returns>Key value based <see cref="ISessionFactory"/></returns>
        IDictionary<object, ISessionFactory> Build();
    }

    /// <summary>
    /// A simple multiple data source implementation of <see cref="IMultiDataSource"/>.
    /// </summary>
    public sealed class MultiDataSource : Datasource, IMultiDataSource
    {
        #region Variables.

        /// <summary>
        /// Key value connection strings.
        /// </summary>
        private readonly IDictionary<string, IPersistenceUnit> persistenceUnits;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDataSource"/> class.
        /// </summary>
        /// <param name="persistenceUnits">Key value <see cref="IPersistenceUnit"/></param>
        public MultiDataSource(IDictionary<string, IPersistenceUnit> persistenceUnits)
        {
            Requires.NotNull(persistenceUnits, "persistenceUnits");
            this.persistenceUnits = persistenceUnits;
        }

        #endregion

        #region IMultiDataSource Members.

        /// <inheritdoc />
        public IDictionary<object, ISessionFactory> Build()
        {
            return this.persistenceUnits.ToDictionary<KeyValuePair<string, IPersistenceUnit>, object, ISessionFactory>(persistenceUnit => persistenceUnit.Key, persistenceUnit => this.BuildSessionFactory(persistenceUnit.Value));
        }

        #endregion
    }
}