// <copyright file="DefaultPersistenceExceptionHandler.cs" company="Appva AB">
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
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DefaultPersistenceExceptionHandler : IPersistenceExceptionHandler
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPersistenceExceptionHandler"/> class.
        /// </summary>
        public DefaultPersistenceExceptionHandler()
        {
        }

        #endregion

        #region IExceptionHandler Members.

        /// <inheritdoc />
        public void Handle(Exception exception)
        {
            throw new ApplicationException("Persistence exception", exception);
        }

        /// <inheritdoc />
        public void Handle(AggregateException exception)
        {
            throw new ApplicationException("Persistence exception", exception);
        }

        #endregion
    }
}