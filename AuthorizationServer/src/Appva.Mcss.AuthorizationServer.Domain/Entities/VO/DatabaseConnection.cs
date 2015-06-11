// <copyright file="DatabaseConnection.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// A database connection value object.
    /// </summary>
    public class DatabaseConnection : ValueObject<DatabaseConnection>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public DatabaseConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnection"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate</remarks>
        protected DatabaseConnection()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The connection string.
        /// </summary>
        public virtual string ConnectionString
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override bool Equals(DatabaseConnection other)
        {
            return this.ConnectionString.Equals(other.ConnectionString);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ConnectionString.GetHashCode();
        }

        #endregion
    }
}