// <copyright file="ISaveRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// Represents save capabilities for a repository.
    /// </summary>
    public interface ISaveRepository<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        void Save(T entity);
    }
}