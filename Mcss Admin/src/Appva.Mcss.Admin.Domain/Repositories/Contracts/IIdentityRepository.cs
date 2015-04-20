// <copyright file="IIdentityRepository.cs" company="Appva AB">
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
    /// Repository interface for identifier access.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IIdentityRepository<T> where T : Entity<T>
    {
        /// <summary>
        /// Returns a single entity by it's unique id.
        /// </summary>
        /// <param name="id">The unique entity identifier</param>
        /// <returns>A single instance of {T} or null if not found</returns>
        T Find(Guid id);
    }
}