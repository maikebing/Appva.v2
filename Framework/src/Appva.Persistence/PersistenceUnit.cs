// <copyright file="PersistenceUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Core.Extensions;
    using Validation;

    #endregion

    /// <summary>
    /// Persistence unit interface.
    /// </summary>
    public interface IPersistenceUnit
    {
        /// <summary>
        /// The multi tenant ID.
        /// </summary>
        string Id
        {
            get;
        }

        /// <summary>
        /// The database connection string.
        /// </summary>
        string ConnectionString
        {
            get;
        }

        /// <summary>
        /// The assembly of which the NHibernate entities resides.
        /// </summary>
        string Assembly
        {
            get;
        }

        /// <summary>
        /// Persistence unit properties, i.e. NHibernate properties.
        /// </summary>
        IDictionary<string, string> Properties
        {
            get;
        }
    }

    /// <summary>
    /// Implementation of <see cref="IPersistenceUnit"/>.
    /// </summary>
    public sealed class PersistenceUnit : IPersistenceUnit
    {
        #region Variables.

        /// <summary>
        /// Default persistence unit properties.
        /// </summary>
        private readonly IDictionary<string, string> defaultProperties = new Dictionary<string, string>
            {
                { "hbm2ddl.auto", "update" },
                { "adonet.batch_size", "30" },
                { "default_batch_fetch_size", "30" },
                { "cache.use_query_cache", "false" },
                { "cache.use_second_level_cache", "false" },
                { "cache.show_sql", "false" },
                { "current_session_context_class", "web" },
                { "dialect", "NHibernate.Dialect.MsSql2012Dialect" },
                { "connection.provider", "NHibernate.Connection.DriverConnectionProvider" }
            };

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceUnit"/> class.
        /// </summary>
        /// <param name="id">The tenant identifier</param>
        /// <param name="connectionString">The tenant database connection string</param>
        /// <param name="assembly">The assembly of which the entities resides</param>
        /// <param name="properties">Optional NHibernate properties</param>
        public PersistenceUnit(string id, string connectionString, string assembly, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            this.Id = id;
            this.ConnectionString = connectionString;
            this.Assembly = assembly;
            this.Properties = this.MergeProperties(properties);
        }

        #endregion

        #region IPersistenceUnit Members.

        /// <inheritdoc />
        public string Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string ConnectionString
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Assembly
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public IDictionary<string, string> Properties
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PersistenceUnit"/> class.
        /// </summary>
        /// <param name="id">The tenant identifier</param>
        /// <param name="connectionString">The tenant database connection string</param>
        /// <param name="assembly">The assembly of which the entities resides</param>
        /// <param name="properties">Optional NHibernate properties</param>
        /// <returns>A <see cref="IPersistenceUnit"/> instance</returns>
        public static IPersistenceUnit CreateNew(string id, string connectionString, string assembly, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            return new PersistenceUnit(id, connectionString, assembly, properties);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PersistenceUnit"/> class.
        /// </summary>
        /// <param name="connectionString">The tenant database connection string</param>
        /// <param name="assembly">The assembly of which the entities resides</param>
        /// <param name="properties">Optional NHibernate properties</param>
        /// <returns>A <see cref="IPersistenceUnit"/> instance</returns>
        public static IPersistenceUnit CreateNew(string connectionString, string assembly, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            return CreateNew(null, connectionString, assembly, properties);
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Merge default properties with custom.
        /// </summary>
        /// <param name="properties">The properties which will be merged with default</param>
        /// <returns>A new properties dictionary</returns>
        private IDictionary<string, string> MergeProperties(IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (properties == null)
            {
                return this.defaultProperties;
            }
            foreach (var property in properties)
            {
                if (this.defaultProperties.ContainsKey(property.Key))
                {
                    this.defaultProperties[property.Key] = property.Value;
                }
                else
                {
                    this.defaultProperties.Add(property.Key, property.Value);
                }
            }
            return this.defaultProperties;
        }

        #endregion
    }
}