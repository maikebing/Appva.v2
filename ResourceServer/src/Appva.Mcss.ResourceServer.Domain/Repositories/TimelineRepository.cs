// <copyright file="TimelineRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// The taxon repository.
    /// </summary>
    public interface ITimelineRepository : IRepository<Task>
    {
    }

    /// <summary>
    /// Implementation of <see cref="ITimelineRepository"/>.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class TimelineRepository : Repository<Task>, ITimelineRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TimelineRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion
    }
}