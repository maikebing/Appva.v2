// <copyright file="IPagedRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Enables a simple method for pagable lists.
    /// </summary>
    public interface IPagedRepository<T> where T : class, IAggregateRoot
    {
        /// <summary>
        /// Returns a simple paged collection of {T}.
        /// </summary>
        /// <param name="pageQuery">The page query parameters.</param>
        /// <returns>A <see cref="IPaged{T}"/>.</returns>
        IPaged<T> Paged(IPageQuery pageQuery);
    }
}