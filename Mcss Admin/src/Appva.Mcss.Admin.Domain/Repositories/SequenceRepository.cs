// <copyright file="SequenceRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISequenceRepository : IRepository<Sequence>, IUpdateRepository<Sequence>
    {
        /// <summary>
        /// Lists the specified ordinations ids.
        /// </summary>
        /// <param name="ordinationsIds">If set, lists by given ordination-ids<param>
        /// <returns></returns>
        IList<Sequence> List(IList<long> ordinationsIds = null);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SequenceRepository : Repository<Sequence>, ISequenceRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public SequenceRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        /// <inheritdoc />
        public void Update(Sequence entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.Context.Update<Sequence>(entity);
        }

        #endregion

        #region ISequenceRepository members.

        /// <inheritdoc />
        public IList<Sequence> List(IList<long> ordinationsIds = null)
        {   
            // TODO: Check permissions to schedules 
            var query = this.persistenceContext.QueryOver<Sequence>();

            if (ordinationsIds != null)
            {
                var array = ordinationsIds.ToArray();
                Medication alias = null;
                query.JoinQueryOver(x => x.Medications, () => alias)
                    .WhereRestrictionOn(() => alias.OrdinationId).IsIn(array);
            }
            return query.List();
        }

        #endregion
    }
}