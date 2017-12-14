// <copyright file="ObservationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    public interface IObservationItemRepository : 
        IRepository<ObservationItem>, 
        ISaveRepository<ObservationItem>
    {
        /// <summary>
        /// Lists the specified observation identifier.
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <returns>IList&lt;ObservationItem&gt;<see cref="ObservationItem"/>.</returns>
        IList<ObservationItem> List(Guid observationId);

        /// <summary>
        /// Lists Observations Items by date.
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>IList&lt;ObservationItem&gt;<see cref="ObservationItem"/>.</returns>
        IList<ObservationItem> ListByDate(Guid observationId, DateTime startDate, DateTime endDate);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ObservationItemRepository : Repository<ObservationItem>, IObservationItemRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationItemRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="T:Appva.Persistence.IPersistenceContext" />.</param>
        public ObservationItemRepository(IPersistenceContext context) 
            : base(context)
        {
        }

        #endregion

        #region IObservationItemRepository Members.

        /// <inheritdoc />
        public IList<ObservationItem> List(Guid observationId)
        {
            return this.Context.QueryOver<ObservationItem>()
                  .Where(x => x.IsActive)
                    .And(x => x.Observation.Id == observationId)
                .OrderBy(x => x.CreatedAt).Desc
                .List();
        }

        /// <inheritdoc />
        public IList<ObservationItem> ListByDate(Guid observationId, DateTime startDate, DateTime endDate)
        {
            return this.Context.QueryOver<ObservationItem>()
                .Where(x => x.IsActive)
                  .And(x => x.Observation.Id == observationId)
                  .And(x => x.CreatedAt > startDate && x.CreatedAt < endDate)
                .OrderBy(x => x.CreatedAt).Desc
                .List();
        }

        #endregion
    }
}
