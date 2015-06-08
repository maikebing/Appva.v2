// <copyright file="TenantNotFoundException.cs" company="Appva AB">
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
    /// Represents errors that occur when a tenant is not found.
    /// </summary>
    public sealed class TenantNotFoundException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TenantNotFoundException"/> class.
        /// </summary>
        public TenantNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TenantNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public TenantNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TenantNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public TenantNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}