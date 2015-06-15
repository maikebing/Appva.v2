// <copyright file="TenantsResultException.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Represents errors that occur when client returns zero result.
    /// </summary>
    [Serializable]
    public sealed class TenantsResultException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsResultException"/> class.
        /// </summary>
        public TenantsResultException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public TenantsResultException(string message)
        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public TenantsResultException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}