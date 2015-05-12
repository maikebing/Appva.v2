// <copyright file="IExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Exceptions
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Handles exceptions.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Handles a single exception.
        /// </summary>
        /// <param name="exception">An exception</param>
        void Handle(Exception exception);

        /// <summary>
        /// Handles an aggregated exception, <see cref="AggregateException"/>.
        /// </summary>
        /// <param name="exceptions">An aggregate exception</param>
        void Handle(AggregateException exception);
    }
}