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
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Appva.Persistence.Interceptors;
    using Logging;
    using NHibernate;
    using NHibernate.Cfg;
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
    /// Abstract base implementation of <see cref="IDatasource"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Reviewed.")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException", Justification = "Reviewed.")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local", Justification = "Reviewed.")]
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
            try
            {
                Requires.NotNull(unit, "unit");
                return this.CreateConnection(unit);
            }
            catch (Exception ex)
            {
                Log.ErrorException("Unable to create database connection!", ex);
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
        protected IDictionary<string, ISessionFactory> Build(IEnumerable<IPersistenceUnit> units)
        {
            Requires.NotNull(units, "units");
            var exceptions = new ConcurrentQueue<Exception>();
            var retval = new Dictionary<string, ISessionFactory>();
            Parallel.ForEach(units, x =>
                {
                    try
                    {
                        retval.Add(x.Id, this.CreateConnection(x));
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorException("Unable to create database connection!", ex);
                        exceptions.Enqueue(ex);
                    }
                });
            if (exceptions.Count > 0)
            {
                //throw new AggregateException(exceptions);
            }
            return retval;
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
            var configuration = new Configuration();
            configuration.SetProperties(unit.Properties);
            configuration.AddAssembly(unit.Assembly);
            configuration.DataBaseIntegration(x =>
                {
                    x.ConnectionString = unit.ConnectionString;
                });
            if (Log.IsDebugEnabled())
            {
                configuration.SetInterceptor(new LogSqlInterceptor());
            }
            return configuration.BuildSessionFactory();
        }

        #endregion
    }
}