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
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Appva.Persistence.Interceptors;
    using Core.Logging;
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
        void Connect();
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
        /// The <see cref="ILog"/> for <see cref="Datasource"/>.
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
        public abstract void Connect();

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
            Requires.NotNull(unit, "unit");
            try
            {
                return this.CreateConnection(unit);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Builds a key value <see cref="ISessionFactory"/> pairs.
        /// </summary>
        /// <param name="units">The <see cref="IPersistenceUnit"/></param>
        /// <returns>An <see cref="ISessionFactory"/> key value pairs or null if failed</returns>
        /// <exception cref="System.AggregateException">
        /// If one or several database connections failed
        /// </exception>
        protected IDatasourceResult Build(IEnumerable<IPersistenceUnit> units)
        {
            Requires.NotNull(units, "units");
            Stopwatch watch = null;
            var result = DatasourceResult.CreateNew();
            if (Log.IsDebugEnabled())
            {
                watch = new Stopwatch();
                watch.Start();
            }
            Parallel.ForEach(units, x =>
                {
                    try
                    {
                        result.SessionFactories.Add(x.Id, this.CreateConnection(x));
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        result.Exceptions.Enqueue(ex);
                    }
                });
            if (Log.IsDebugEnabled())
            {
                watch.Stop();
                Log.Debug("Startup time was " + watch.ElapsedMilliseconds + "ms");
            }
            return result;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <param name="unit">The <see cref="IPersistenceUnit"/> to connect</param>
        /// <returns>A new <see cref="ISessionFactory"/> instance</returns>
        /// <exception cref="System.Data.SqlClient.SqlException">An exception if a connection cannot be established</exception>
        private ISessionFactory CreateConnection(IPersistenceUnit unit)
        {
            Log.DebugJson(unit);
            var configuration = new Configuration();
            configuration.SetProperties(unit.Properties);
            configuration.AddAssembly(unit.Assembly);
            configuration.DataBaseIntegration(x =>
                {
                    x.ConnectionString = unit.ConnectionString;
                    x.Dialect<MsSql2012Dialect>();
                });
            if (Log.IsDebugEnabled())
            {
                configuration.SetInterceptor(new LogSqlInterceptor());
            }
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
            /// Initializes a new instance of the <see cref="DatasourceResult"/> class.
            /// </summary>
            private DatasourceResult()
            {
                this.Exceptions = new ConcurrentQueue<Exception>();
                this.SessionFactories = new Dictionary<string, ISessionFactory>();
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
        }

        #endregion
    }
}