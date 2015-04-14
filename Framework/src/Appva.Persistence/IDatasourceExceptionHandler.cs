// <copyright file="IDatasourceExceptionHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Handles <see cref="Datasource"/> connection failures.
    /// </summary>
    public interface IDatasourceExceptionHandler
    {
        /// <summary>
        /// Handle <see cref="Datasource"/> failure when attempting to establish a database 
        /// connection.
        /// </summary>
        /// <param name="exception">An exception</param>
        void Handle(Exception exception);

        /// <summary>
        /// Handle <see cref="Datasource"/> failures when attempting to establish a database 
        /// connection.
        /// </summary>
        /// <param name="exceptions">A collection of exceptions</param>
        void Handle(AggregateException exception);
    }
}