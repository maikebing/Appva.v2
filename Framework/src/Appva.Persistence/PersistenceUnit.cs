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
    /// Implementation of <see cref="IPersistenceUnit"/>.
    /// </summary>
    public sealed class PersistenceUnit : IPersistenceUnit
    {
        #region Variables.

        /// <summary>
        /// The multi tenancy identifier.
        /// </summary>
        private readonly string id;

        /// <summary>
        /// The database connection string.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Assembly of which the NHibernate entities resides.
        /// </summary>
        private readonly string assembly;

        /// <summary>
        /// Persistence unit properties, i.e. NHibernate properties.
        /// </summary>
        private readonly IDictionary<string, string> properties = new Dictionary<string, string>
            {
                { "hbm2ddl.auto", "update" },
                { "adonet.batch_size", "30" },
                { "default_batch_fetch_size", "30" },
                { "cache.use_query_cache", "false" },
                { "cache.use_second_level_cache", "false" },
                { "cache.show_sql", "false" },
                { "current_session_context_class", "web" },
                { "dialect", "NHibernate.Dialect.MsSql2008Dialect" },
                { "connection.provider", "NHibernate.Connection.DriverConnectionProvider" }
            };

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceUnit"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string></param>
        /// <param name="assembly">The assembly of which the entities resides</param>
        /// <param name="properties">Optional NHibernate properties</param>
        /// <param name="id">Optional multi tenancy identifier</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Reviewed.")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException", Justification = "Reviewed.")]
        public PersistenceUnit(string connectionString, string assembly, IEnumerable<KeyValuePair<string, string>> properties = null, string id = null)
        {
            Requires.NotNull(connectionString, "connectionString");
            Requires.NotNull(assembly, "assembly");
            this.connectionString = connectionString;
            this.assembly = assembly;
            this.id = id;
            if (properties.IsNotNull())
            {
                foreach (var property in properties)
                {
                    if (this.properties.ContainsKey(property.Key))
                    {
                        this.properties[property.Key] = property.Value;
                    }
                    else
                    {
                        this.properties.Add(property.Key, property.Value);
                    }
                }
            }
        }

        #endregion

        #region IPersistenceUnit Members.

        /// <inheritdoc />
        public string Id
        {
            get
            {
                return this.id;
            }
        }

        /// <inheritdoc />
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        /// <inheritdoc />
        public string Assembly
        {
            get
            {
                return this.assembly;
            }
        }

        /// <inheritdoc />
        public IDictionary<string, string> Properties
        {
            get
            {
                return this.properties;
            }
        }

        #endregion
    }
}