// <copyright file="SqlDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.Tests
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using Appva.Core.Extensions;
    using Appva.Core.IO;
    using Validation;

    #endregion

    /// <summary>
    /// Testable <c>xunit</c> local attached .mdf database.
    /// </summary>
    /// <typeparam name="T">The initial data populator type</typeparam>
    public class SqlDatasource<T> : IDisposable where T : DataPopulation, IDataPopulation
    {
        #region Variables.

        /// <summary>
        /// The local database connection string format.
        /// </summary>
        private const string ConnectionStringFormat = @"Data Source=(localdb)\v11.0;AttachDbFilename='{0}.mdf';Initial Catalog=LocalTestDb;Integrated Security=True";

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        /// <summary>
        /// The <see cref="DefaultPersistenceContextAwareResolver"/>.
        /// </summary>
        private readonly DefaultPersistenceContextAwareResolver resolver;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDatasource{T}"/> class.
        /// </summary>
        public SqlDatasource()
        {
            var assembly         = this.GetConfigurationForAssembly();
            var database         = this.GetConfigurationForDataBase();
            var teardown         = this.IsTearDownEnabled();
            var connectionString = this.ResolveConnectionString(database);
            this.resolver = new DefaultPersistenceContextAwareResolver(
                new DefaultDatasource(new DefaultDatasourceConfiguration
                {
                    Assembly = assembly,
                    ConnectionString = connectionString,
                    Properties = new Dictionary<string, string>
                    {
                        { "show_sql", "true" },
                        { "hbm2ddl.auto", teardown ? "create-drop" : "create" }
                    }
                }),
                new DefaultPersistenceExceptionHandler());
            this.PopulateData();
            this.persistenceContext = this.resolver.CreateNew().Open();
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// Returns the <see cref="IPersistenceContext"/>.
        /// </summary>
        public IPersistenceContext PersistenceContext
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
            }
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns the connection string with local path.
        /// </summary>
        /// <param name="database">The database name</param>
        /// <returns>A connection string for the attached database</returns>
        private string ResolveConnectionString(string database)
        {
            return ConnectionStringFormat.FormatWith(PathResolver.ResolveAppRelativePath(database)
                .ReplaceAll(@"\bin", @"\Debug").Replace(@"\\", @"\"));
        }

        /// <summary>
        /// Returns the local data base name from app settings.
        /// </summary>
        /// <returns>The local database name</returns>
        private string GetConfigurationForDataBase()
        {
            var value = ConfigurationManager.AppSettings.Get("database:name");
            Requires.ValidState(value.IsNotEmpty(), "You must set <appSettings><add key=\"database:name\" value=\"your database name here\" /></appSettings>");
            return value;
        }

        /// <summary>
        /// Returns the entity assembly name from app settings.
        /// </summary>
        /// <returns>The assemby name</returns>
        private string GetConfigurationForAssembly()
        {
            var value = ConfigurationManager.AppSettings.Get("database:assembly");
            Requires.ValidState(value.IsNotEmpty(), "You must set <appSettings><add key=\"database:assembly\" value=\"your assembly to entities here\" /></appSettings>");
            return value;
        }

        /// <summary>
        /// Returns whether or not tear down database on dispose is enabled or not.
        /// </summary>
        /// <returns>True if database is deleted on dispose</returns>
        private bool IsTearDownEnabled()
        {
            var value = ConfigurationManager.AppSettings.Get("database:teardown");
            return value.IsNotEmpty() && (value.ToLower().Equals("true") || value.ToLower().Equals("yes"));
        }

        /// <summary>
        /// Pre-populate database data.
        /// </summary>
        private void PopulateData()
        {
            if (typeof(T) == typeof(NoDataPopulation))
            {
                return;
            }
            using (var context = this.resolver.CreateNew().Open())
            using (var transaction = context.BeginTransaction())
            {
                var populator = (T) Activator.CreateInstance(typeof(T), new object[] { context });
                populator.Populate();
                transaction.Commit();
            }
        }

        #endregion
    }

    /// <summary>
    /// Testable <c>xunit</c> local attached .mdf database.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class SqlDatasource : SqlDatasource<NoDataPopulation>
    {
    }
}