// <copyright file="IPagingAndSortingRepository.cs" company="Appva AB">
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IPagingAndSortingRepository<T> where T : Entity<T>
    {
        /// <summary>
        /// Returns a paged collection of {T} entities.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="page">The current page number</param>
        /// <param name="size">The result set size</param>
        /// <returns>A paged collection <c>IPagedList{T}</c></returns>
        IPagedList<T> PagedList<T>(ulong page, ulong size) where T : Entity<T>;
    }
}