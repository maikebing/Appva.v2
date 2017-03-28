// <copyright file="ISaveListRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents save collection capabilities for a repository.
    /// </summary>
    public interface ISaveListRepository<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Saves a collection of entities.
        /// </summary>
        /// <param name="entities">The entities to be saved.</param>
        void Save(IList<T> entities);
    }
}