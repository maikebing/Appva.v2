// <copyright file="IRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IRepository<T> where T : IAggregateRoot, IEntity
    {
        /// <summary>
        /// Returns the entity {T} by ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>A {T} entity; or null if not found.</returns>
        T Get(object id);

        /// <summary>
        /// Creates a proxy from the guid
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>A {T} entity; or null if not found.</returns>
        T Load(object id);
    }
}