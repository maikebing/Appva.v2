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
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public interface IDelegationRepository : 
        IIdentityRepository<Delegation>,
        IUpdateRepository<Delegation>,
        ISaveRepository<Delegation>,
        IRepository
    {
        /// <summary>
        /// List all delegation by given paramters
        /// </summary>
        /// <param name="byAccount">An account</param>
        /// <param name="isPending"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IList<Delegation> List(Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null);

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
    public sealed class DelegationRepository : IDelegationRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationRepository"/> class.
        /// </summary>
        public DelegationRepository(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region IDelegationRepository members.

        /// <inheritdoc />
        public IList<Delegation> List(Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null)
        {
            var query = this.persistence.QueryOver<Delegation>();

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
            if (byCategory.HasValue && byCategory.Value.IsNotEmpty())
            {
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(x => x.Parent.Id == byCategory.Value);
            }

            return query.List();
        }

        #endregion

        #region IIdentityRepository members.

        /// <inheritdoc />
        public Delegation Find(Guid id)
        {
            return this.persistence.Get<Delegation>(id);
        }

        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        public void Update(Delegation entity, ChangeSet changes)
        {
            this.persistence.Save<ChangeSet>(changes);
            this.Update(entity);
        }

        public void Update(Delegation entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version += 1;
            this.persistence.Update<Delegation>(entity);
        }

        #endregion

        #region ISaveRepository Members.

        /// <inheritdoc />
        public void Save(Delegation entity)
        {
            this.persistence.Save<Delegation>(entity);
        }

        #endregion
    }
}