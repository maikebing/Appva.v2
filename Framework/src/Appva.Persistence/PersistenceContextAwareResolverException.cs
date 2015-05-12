// <copyright file="PersistenceContextAwareResolverException.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Represents errors that occur during persistence resolving execution.
    /// </summary>
    public sealed class PersistenceContextAwareResolverException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextAwareResolverException"/> class.
        /// </summary>
        public PersistenceContextAwareResolverException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextAwareResolverException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public PersistenceContextAwareResolverException(string message)
        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextAwareResolverException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public PersistenceContextAwareResolverException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}