// <copyright file="LogSqlInterceptor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.Interceptors
{
    #region Imports.

    using System;
    using Core.Logging;
    using NHibernate;
    using NHibernate.SqlCommand;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Serializable]
    internal sealed class LogSqlInterceptor : EmptyInterceptor
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="Datasource"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<LogSqlInterceptor>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogSqlInterceptor"/> class.
        /// </summary>
        public LogSqlInterceptor()
        {
        }

        #endregion

        #region EmptyInterceptor Overrides.

        /// <inheritdoc />
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Log.Debug("SQL: {0}", sql);
            return base.OnPrepareStatement(sql);
        }

        #endregion
    }
}