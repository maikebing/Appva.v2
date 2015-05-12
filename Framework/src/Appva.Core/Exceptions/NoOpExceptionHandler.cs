// <copyright file="NoOpExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Exceptions
{
    #region Imports.

    using System;
    using Logging;

    #endregion

    /// <summary>
    /// A No operation implementation of <see cref="IExceptionHandler"/>.
    /// </summary>
    public sealed class NoOpExceptionHandler : IExceptionHandler
    {
        #region Variables.

        /// <summary>
        /// Logging for <see cref="DefaultExceptionHandler"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<NoOpExceptionHandler>();

        #endregion

        #region IExceptionHandler Members

        /// <inheritdoc />
        public void Handle(Exception exception)
        {
            //// No op!
        }

        /// <inheritdoc />
        public void Handle(AggregateException exception)
        {
            //// No op!
        }

        #endregion
    }
}