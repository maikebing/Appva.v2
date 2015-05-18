// <copyright file="DefaultExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Exceptions
{
    #region Imports.

    using System;
    using Resources;
    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// An implementation of <see cref="IExceptionHandler"/>.
    /// </summary>
    /// <remarks>
    /// All handle methods will log and throw the original <see cref="Exception"/>.
    /// </remarks>
    public sealed class DefaultExceptionHandler : IExceptionHandler
    {
        #region IExceptionHandler Members

        /// <inheritdoc />
        /// <exception cref="ApplicationException">Throws a new exception</exception>
        public void Handle([NotNull] Exception exception)
        {
            throw new ApplicationException(ExceptionWhen.HandlingExceptions, exception);
        }

        /// <inheritdoc />
        /// <exception cref="ApplicationException">Throws a new exception</exception>
        public void Handle([NotNull] AggregateException exception)
        {
            throw new ApplicationException(ExceptionWhen.HandlingExceptions, exception);
        }

        #endregion
    }
}