// <copyright file="Datasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence
{
    #region Imports.

    using NHibernate;
    using NHibernate.Cfg;

    #endregion

    /// <summary>
    /// Abstract implementation of a data source.
    /// </summary>
    public abstract class Datasource
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Datasource"/> class.
        /// </summary>
        protected Datasource()
        {
        }

        #endregion

        #region Protected Functions.
        
        /// <summary>
        /// Builds a <see cref="ISessionFactory"/> by connection string.
        /// </summary>
        /// <param name="persistenceUnit">A <see cref="IPersistenceUnit"/></param>
        /// <returns><see cref="ISessionFactory"/></returns>
        protected virtual ISessionFactory BuildSessionFactory(IPersistenceUnit persistenceUnit)
        {
            return new Configuration()
                .SetProperties(persistenceUnit.Properties)
                .AddAssembly(persistenceUnit.Assembly)
                .DataBaseIntegration(db => 
                {
                    db.ConnectionString = persistenceUnit.ConnectionString;
                })
                .BuildSessionFactory();
        }

        #endregion
    }
}