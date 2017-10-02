// <copyright file="DelegationRepository.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.NHibernateUtils.Restrictions;

    #endregion

    public interface IDelegationRepository :
        IRepository<Delegation>,
        IUpdateRepository<Delegation>,
        ISaveRepository<Delegation>
    {
        /// <summary>
        /// List all delegation by given paramters
        /// </summary>
        /// <param param name="taxonFilter">The taxon path filter.</param>
        /// <param name="byAccount">An account</param>
        /// <param name="isPending"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">If taxon filter is null or empty.</exception>
        IList<Delegation> List(string taxonFilter, Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null);

        /// <summary>
        /// Updates a delegation and saves the changes
        /// </summary>
        /// <param name="entity">The delegation</param>
        /// <param name="changes">The changes</param>
        void Update(Delegation entity, ChangeSet changes);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationRepository : Repository<Delegation>, IDelegationRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public DelegationRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region IDelegationRepository members.

        /// <inheritdoc />
        public IList<Delegation> List(string taxonFilter, Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null)
        {
            if (string.IsNullOrWhiteSpace(taxonFilter))
            {
                throw new ArgumentException("A Taxon filter must be set", "taxonFilter");
            }
            var query = this.Context.QueryOver<Delegation>();
            if (byAccount.HasValue && byAccount.Value.IsNotEmpty())
            {
                query.Where(x => x.Account.Id == byAccount.Value);
            }
            if (createdBy.HasValue && createdBy.Value.IsNotEmpty())
            {
                query.Where(x => x.CreatedBy.Id == createdBy.Value);
            }
            if (isPending.HasValue)
            {
                query.Where(x => x.Pending == isPending.Value);
            }
            if (isActive.HasValue)
            {
                query.Where(x => x.IsActive == isActive.Value);
            }
            if (isGlobal.HasValue)
            {
                query.Where(x => x.IsGlobal == isGlobal.Value);
            }
            Taxon taxonAlias = null;
            if (byCategory.HasValue && byCategory.Value.IsNotEmpty())
            {
                query
                    .Inner.JoinAlias(x => x.OrganisationTaxon, () => taxonAlias, TaxonFilterRestrictions.Pipe<Taxon>(x => x.Path, taxonFilter));
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(x => x.Parent.Id == byCategory.Value);
            }
            else
            {
                query
                    .Inner.JoinAlias(x => x.OrganisationTaxon, () => taxonAlias, TaxonFilterRestrictions.Pipe<Taxon>(x => x.Path, taxonFilter));
            }
            return query.List();
        }

        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        public void Update(Delegation entity, ChangeSet changes)
        {
            this.Context.Save<ChangeSet>(changes);
            this.Update(entity);
        }

        /// <inheritdoc />
        public void Update(Delegation entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.Context.Update<Delegation>(entity);
        }

        #endregion

        #region ISaveRepository Members.

        /// <inheritdoc />
        public void Save(Delegation entity)
        {
            this.Context.Save<Delegation>(entity);
        }

        #endregion
    }
}