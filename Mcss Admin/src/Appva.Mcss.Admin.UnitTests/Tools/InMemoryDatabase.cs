// <copyright file="InMemoryDatabase.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Tools
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Appva.Core.Exceptions;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// An in memory database for persistence tests.
    /// </summary>
    public class InMemoryDatabase : IDisposable
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private IPersistenceContext persistenceContext;

        /// <summary>
        /// The DB configuration.
        /// </summary>
        private static readonly IDefaultDatasourceConfiguration Configuration = new DefaultDatasourceConfiguration
            {
                ConnectionString = "data source=:memory:",
                Assembly = "Appva.Mcss.Admin.Domain",
                Properties = new Dictionary<string, string> { 
                    { "connection.connection_string", "data source=:memory:" },
                    { "connection.release_mode", "on_close" },
                    { "connection.driver_class", "NHibernate.Driver.SQLite20Driver, NHibernate, Version=4.0.0.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4" },
                    { "dialect", "NHibernate.Dialect.SQLiteDialect, NHibernate, Version=4.0.0.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4" },
                    { "show_sql", "true" } 
                }
            };

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDatabase"/> class.
        /// </summary>
        public InMemoryDatabase()
        {
            //// var persistenceResolver = new DefaultPersistenceContextAwareResolver(new DefaultDatasource(Configuration), new DefaultExceptionHandler());
            //// this.persistenceContext = persistenceResolver.CreateNew().Open();
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// Returns the <see cref="IPersistenceContext"/>.
        /// </summary>
        protected IPersistenceContext PersistenceContext
        {
            get
            {
                return this.persistenceContext;
            }
        }

        #endregion

        #region IDisposable Members.

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        /// <param name="disposing">If disposing should be performed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.persistenceContext != null)
            {
                this.persistenceContext.Dispose();
                this.persistenceContext = null;
            }
        }

        #endregion
    }
}