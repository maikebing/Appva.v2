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
    using Appva.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    public interface ISequenceRepository : IIdentityRepository<Sequence>, 
        IUpdateRepository<Sequence>, 
        ISaveRepository<Sequence>, 
        IRepository
    {
        #region Fields

        Sequence Get(Guid id);

        #endregion
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
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public SequenceRepository(IPersistenceContext context)
            : base(context)
        {
        }


        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        public IList<Sequence> List(IList<long> ordinationsIds = null)
        {   
            // TODO: Check permissions to schedules 
            var query = this.Context.QueryOver<Sequence>();

            if (ordinationsIds != null)
        {
                var array = ordinationsIds.ToArray();
                Medication alias = null;
                query.JoinQueryOver(x => x.Medications, () => alias)
                    .WhereRestrictionOn(() => alias.OrdinationId).IsIn(array);
            }
            return query.List();
        }
    }
}