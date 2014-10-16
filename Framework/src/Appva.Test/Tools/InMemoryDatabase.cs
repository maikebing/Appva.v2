// <copyright file="InMemoryDatabase.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Tools
{
    #region Imports.

    using Appva.Core.Configuration;
    using Appva.Persistence;
    using Appva.Persistence.Providers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
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
            var persistenceFactory = ConfigurableApplicationContext.Read<PersistenceConfiguration>()
                .From("App_Data\\Persistence.json")
                .ToObject()
                .Build();
            this.persistenceContext = persistenceFactory.Build().Open();
        }

        #endregion

        #region Inherited.

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

        #region IDisposable Implementation.

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