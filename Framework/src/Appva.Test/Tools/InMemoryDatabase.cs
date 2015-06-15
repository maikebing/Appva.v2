// <copyright file="InMemoryDatabase.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Tools
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Core.Configuration;
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDatabase"/> class.
        /// </summary>
        public InMemoryDatabase()
        {
            var configuration = ConfigurableApplicationContext.Read<DefaultDatasourceConfiguration>()
                .From("App_Data\\Persistence.json")
                .ToObject();
            var persistenceResolver = new DefaultPersistenceContextAwareResolver(
                new DefaultDatasource(configuration),
                new DefaultPersistenceExceptionHandler());
            this.persistenceContext = persistenceResolver.CreateNew().Open();
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