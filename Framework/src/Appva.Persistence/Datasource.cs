// <copyright file="Datasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Logging;
    using JetBrains.Annotations;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using Validation;

    #endregion

    /// <summary>
    /// The data source for which the database connection will be made.
    /// </summary>
    public interface IDatasource
    {
        /// <summary>
        /// Attempts to connect to the data source and establish a database connection.
        /// </summary>
        /// <returns>A data source connection result</returns>
        IDatasourceResult Connect();
    }

    /// <summary>
    /// The data source result after the database connection attempt.
    /// </summary>
    public interface IDatasourceResult
    {
        /// <summary>
        /// Returns the exceptions.
        /// </summary>
        ConcurrentQueue<Exception> Exceptions
        {
            get;
        }

        /// <summary>
        /// Returns the exceptions.
        /// </summary>
        Dictionary<string, ISessionFactory> SessionFactories
        {
            get;
        }
    }

    /// <summary>
    /// Abstract base implementation of <see cref="IDatasource"/>.
    /// </summary>
    public abstract class Datasource : IDatasource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<Datasource>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Datasource"/> class.
        /// </summary>
        protected Datasource()
        {
        }

        #endregion

        #region IDatasource Members.

        /// <inheritdoc />
        public abstract IDatasourceResult Connect();

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Builds a single <see cref="ISessionFactory"/>.
        /// </summary>
        /// <param name="unit">The <see cref="IPersistenceUnit"/></param>
        /// <returns>An <see cref="ISessionFactory"/> instance</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If the <see cref="IPersistenceUnit"/> is null
        /// </exception>
        protected ISessionFactory Build(IPersistenceUnit unit)
        {
            return this.CreateConnection(unit);
        }

        /// <summary>
        /// Builds a key value <see cref="ISessionFactory"/> pairs.
        /// </summary>
        /// <param name="units">The <see cref="IPersistenceUnit"/></param>
        /// <returns>An <see cref="ISessionFactory"/> key value pairs or null if failed</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If the <see cref="IEnumerable{IPersistenceUnit}"/> is null
        /// </exception>
        protected IDatasourceResult Build([NotNull] IEnumerable<IPersistenceUnit> units)
        {
            Requires.NotNull(units, "units");
            var result = DatasourceResult.CreateNew();
            Parallel.ForEach(
                units, 
                x =>
                {
                    try
                    {
                        result.SessionFactories.Add(x.Id, this.CreateConnection(x));
                    }
                    catch (Exception ex)
                    {
                        result.Exceptions.Enqueue(ex);
                    }
                });
            return result;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <param name="unit">The <see cref="IPersistenceUnit"/> to connect</param>
        /// <returns>A new <see cref="ISessionFactory"/> instance</returns>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// An exception if a connection cannot be established
        /// </exception>
        private ISessionFactory CreateConnection([NotNull] IPersistenceUnit unit)
        {
            Requires.NotNull(unit, "unit");
            Log.DebugJson(unit);
            var configuration = new Configuration();
            configuration.SetProperties(unit.Properties);
            configuration.AddAssembly(unit.Assembly);
            configuration.DataBaseIntegration(x =>
                {
                    x.ConnectionString = unit.ConnectionString;
                });
            return configuration.BuildSessionFactory();
        }

        #endregion

        #region Private Class.

        /// <summary>
        /// Represents a create databases results.
        /// </summary>
        private class DatasourceResult : IDatasourceResult
        {
            #region Constructor.

            /// <summary>
            /// Prevents a default instance of the <see cref="DatasourceResult" /> class from 
            /// being created.
            /// </summary>
            private DatasourceResult()
            {
                this.Exceptions = new ConcurrentQueue<Exception>();
                this.SessionFactories = new Dictionary<string, ISessionFactory>();
            }

            #endregion

            #region IDatasourceResult Members.

            /// <inheritdoc />
            public ConcurrentQueue<Exception> Exceptions
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public Dictionary<string, ISessionFactory> SessionFactories
            {
                get;
                private set;
            }

            #endregion

            #region Public Static Functions.

            /// <summary>
            /// Creates a new instance of the <see cref="DatasourceResult"/> class.
            /// </summary>
            /// <returns>A new <see cref="DatasourceResult"/> instance</returns>
            public static DatasourceResult CreateNew()
            {
                return new DatasourceResult();
            }

            #endregion
        }

        #endregion
    }
}