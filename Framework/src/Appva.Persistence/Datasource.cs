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
    using System.Collections.Generic;
    using Core.Extensions;
    using Logging;
    using NHibernate;
    using NHibernate.Cfg;
    using Validation;

    #endregion

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

        /// <summary>
        /// The <see cref="IDatasourceExceptionHandler"/>.
        /// </summary>
        private readonly IDatasourceExceptionHandler exceptionHandler;

        /// <summary>
        /// The <see cref="IDatasourceEventInterceptor"/>.
        /// </summary>
        private readonly IDatasourceEventInterceptor eventInterceptor;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Datasource"/> class.
        /// </summary>
        /// <param name="exceptionHandler"></param>
        /// <param name="eventInterceptor"></param>
        protected Datasource(
            IDatasourceExceptionHandler exceptionHandler, 
            IDatasourceEventInterceptor eventInterceptor)
        {
            Requires.NotNull(exceptionHandler, "exceptionHandler");
            Requires.NotNull(eventInterceptor, "eventInterceptor");
            this.exceptionHandler = exceptionHandler;
            this.eventInterceptor = eventInterceptor;
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
        /// <returns>An <see cref="ISessionFactory"/> or null if failed</returns>
        protected ISessionFactory Build(IPersistenceUnit unit)
        {
            Requires.NotNull(unit, "unit");
            try
            {
                return this.CreateConnection(unit);
            }
            catch (Exception ex)
            {
                Log.ErrorException("Unable to create database connection!", ex);
                this.exceptionHandler.Handle(ex);
            }
            return null;
        }

        /// <summary>
        /// Builds a key value <see cref="ISessionFactory"/> pairs.
        /// </summary>
        /// <param name="unit">The <see cref="IPersistenceUnit"/></param>
        /// <returns>An <see cref="ISessionFactory"/> key value pairs or null if failed</returns>
        protected IDictionary<string, ISessionFactory> Build(IEnumerable<IPersistenceUnit> units)
        {
            Requires.NotNull(units, "units");
            IDictionary<string, ISessionFactory> retval = new Dictionary<string, ISessionFactory>();
            IList<Exception> exceptions = null;
            foreach (var unit in units)
            {
                try
                {
                    retval.Add(unit.Id, this.CreateConnection(unit));
                }
                catch (Exception ex)
                {
                    Log.ErrorException("Unable to create database connection!", ex);
                    exceptions = exceptions.IsNull() ? new List<Exception>() : exceptions;
                    exceptions.Add(ex);
                }
            }
            if (exceptionHandler.IsNotNull())
            {
                this.exceptionHandler.Handle(exceptions);
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
        /// <exception cref="SqlException">An exception if a connection cannot be established</exception>
        private ISessionFactory CreateConnection(IPersistenceUnit unit)
        {
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
    }
}