// <copyright file="DefaultDatasourceExceptionHandler.cs" company="Appva AB">
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
    /// Default no op implementation of <see cref="IDatasourceExceptionHandler"/>.
    /// </summary>
    public sealed class DefaultDatasourceExceptionHandler : IDatasourceExceptionHandler
    {
        #region IDatasourceExceptionHandler Members.

        /// <inheritdoc />
        public void Handle(Exception exception)
        {
            //// No op.
        }

        /// <inheritdoc />
        public void Handle(IEnumerable<Exception> exceptions)
        {
            //// No op.
        }

        #endregion
    }
}