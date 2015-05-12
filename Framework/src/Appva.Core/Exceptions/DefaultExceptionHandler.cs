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
    using Logging;

    #endregion

    /// <summary>
    /// An implementation of <see cref="IExceptionHandler"/>.
    /// </summary>
    /// <remarks>
    /// All handle methods will log and throw the original <see cref="Exception"/>.
    /// </remarks>
    public sealed class DefaultExceptionHandler : IExceptionHandler
    {
        #region Variables.

        /// <summary>
        /// Logging for <see cref="DefaultExceptionHandler"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<DefaultExceptionHandler>();

        #endregion

        #region IExceptionHandler Members

        /// <inheritdoc />
        /// <exception cref="ApplicationException">Throws a new exception</exception>
        public void Handle(Exception exception)
        {
            throw new ApplicationException(ExceptionWhen.HandlingExceptions, exception);
        }

        /// <inheritdoc />
        /// <exception cref="ApplicationException">Throws a new exception</exception>
        public void Handle(AggregateException exception)
        {
            throw new ApplicationException(ExceptionWhen.HandlingExceptions, exception);
        }

        #endregion
    }
}