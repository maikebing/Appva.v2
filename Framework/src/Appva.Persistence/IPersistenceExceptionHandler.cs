// <copyright file="IPersistenceExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using Appva.Core.Exceptions;

    #endregion

    /// <summary>
    /// Marker interface for a data source <see cref="IExceptionHandler"/>.
    /// </summary>
    public interface IPersistenceExceptionHandler : IExceptionHandler
    {
    }
}