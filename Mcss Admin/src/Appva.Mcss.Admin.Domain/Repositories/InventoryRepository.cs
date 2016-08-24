// <copyright file="InventoryRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IInventoryRepository : 
        IIdentityRepository<Inventory>, 
        IUpdateRepository<Inventory>,
        IRepository
    {
        /// <summary>
        /// Lists all inventories for a given patient.
        /// </summary>
        /// <param name="patientId">The patient id</param>
        /// <param name="scheduleSettings">List of schedulesettings for filtering</param>
        /// <param name="active">Include only active/inactive</param>
        /// <returns>List of <see cref="Inventory"/></returns>
        IList<Inventory> Search(Guid patientId, IList<Guid> scheduleSettings, bool? active = null);

        /// <summary>
        /// Lists all transcations for an inventory.
        /// </summary>
        /// <param name="inventory">Id of the <see cref="Inventory"/></param>
        /// <param name="fromDate">Filter from date</param>
        /// <param name="toDate">Filter to date</param>
        /// <param name="page">Current page</param>
        /// <param name="page">Current page siźe</param>
        /// <returns>List of <see cref="Inventory"/></returns>
        PageableSet<InventoryTransactionItem> ListTransactionsFor(Guid inventory, DateTime? fromDate = null, DateTime? toDate = null, int page = 0, int pageSize = 10);

        /// <summary>
        /// Saves the inventory.
        /// </summary>
        /// <param name="entity">The <see cref="Inventory"/></param>
        /// <returns>The id of the created inventory</returns>
        Guid Save(Inventory entity);

        /// <summary>
        /// Lists all inventorys which need a recount before date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns>List of <see cref="Inventory"/></returns>
        IList<Inventory> ListRecountsBefore(DateTime date, DateTime? toDate, Guid? taxonFilter);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InventoryRepository : IInventoryRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRepository"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public InventoryRepository(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region IIdentityRepository<Inventory> Members.

        /// <inheritdoc />
        public Inventory Find(Guid id)
        {
            return this.persistence.Get<Inventory>(id);
        }

        #endregion

        #region IIdentityRepository<Inventory> Members.

        /// <inheritdoc />
        public void Update(Inventory entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.persistence.Update<Inventory>(entity);
        }

        #endregion

        #region Members.

        /// <inheritdoc />
        public IList<Inventory> Search(Guid patientId, IList<Guid> scheduleSettings, bool? active = null)
        {
            var sequenceSubQuery = QueryOver.Of<Sequence>()
                .Where(x => x.Patient.Id == patientId)
                .Select(x => x.Inventory.Id)
                .JoinQueryOver(x => x.Schedule)
                    .JoinQueryOver(x => x.ScheduleSettings)
                        .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.ToArray());
            var query = this.persistence.QueryOver<Inventory>()
                .Where(
                    Restrictions.Or(
                        Restrictions.Eq(Projections.Property<Inventory>(x => x.Patient.Id), patientId),
                        Subqueries.WhereProperty<Inventory>(x => x.Id).In(sequenceSubQuery)));
            if (active.HasValue)
            {
                query.Where(x => x.IsActive == active.GetValueOrDefault(true));
            }
            query = query.OrderBy(x => x.IsActive).Desc.ThenBy(x => x.Description).Asc;
            return query.List();    
        }

        /// <inheritdoc />
        public PageableSet<InventoryTransactionItem> ListTransactionsFor(Guid inventory, DateTime? fromDate = null, DateTime? toDate = null, int page = 0, int pageSize = 10)
        {
            var skip = page != 0 ? (page - 1) * pageSize : 0;
            var query = this.persistence.QueryOver<InventoryTransactionItem>()
                .Where(x => x.IsActive)
                  .And(x => x.Inventory.Id == inventory);
            if (fromDate.HasValue)
            {
                query.Where(x => x.CreatedAt >= fromDate);
            }
            if (toDate.HasValue)
            {
                query.Where(x => x.CreatedAt <= toDate);
            }
            query = query.OrderBy(x => x.CreatedAt).Desc;
            return new PageableSet<InventoryTransactionItem>
            {
                CurrentPage = page,
                NextPage    = page++,
                PageSize    = pageSize,
                TotalCount  = query.ToRowCountQuery().RowCount(),
                Entities    = query.Skip(skip).Take(pageSize).List()
            };

        }

        /// <inheritdoc />
        public Guid Save(Inventory entity)
        {
            return (Guid)this.persistence.Save<Inventory>(entity);
        }

        /// <inheritdoc />
        public IList<Inventory> ListRecountsBefore(DateTime date, DateTime? toDate, Guid? taxonFilter)
        {
            Patient patientAlias = null;
            Taxon taxonAlias = null;
            var query = this.persistence.QueryOver<Inventory>()
                .Where(x => x.IsActive)
                .And(Restrictions.Disjunction()
                    .Add(Restrictions.IsNull(Projections.Property<Inventory>(x => x.LastRecount)))
                    .Add(Restrictions.Conjunction()
                        .Add(Restrictions.Le(Projections.Property<Inventory>(x => x.LastRecount), date))
                        .Add(Restrictions.Gt(Projections.Property<Inventory>(x => x.LastRecount), toDate.GetValueOrDefault(new DateTime(2000,1,1))))))
                .Inner.JoinAlias(x => x.Patient, () => patientAlias, () => patientAlias.IsActive && patientAlias.Deceased == false);
            if (taxonFilter.IsNotEmpty()){
                query.JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .WhereRestrictionOn(() => taxonAlias.Path)
                    .IsLike(taxonFilter.ToString(), MatchMode.Anywhere);
            }
            query = query.OrderBy(x => x.LastRecount).Asc;
            return query.List();
        }

        #endregion
    }
}