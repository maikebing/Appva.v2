// <copyright file="IListRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Interface for providing list access to the repository.
    /// </summary>
    public interface IListRepository<T> where T : Entity<T>
    {
        /// <summary>
        /// Returns a collection of {T} entities.
        /// </summary>
        /// <param name="maximumItems">
        /// Optional maximum amount of items to be retrieved, defaults to <see cref="long.MaxValue"/>
        /// </param>
        /// <returns>A collection of {T} entities</returns>
        IList<T> List(ulong maximumItems = long.MaxValue);
    }
}