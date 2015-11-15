// <copyright file="NotificationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate.Criterion;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface INotificationRepository : 
        IIdentityRepository<Notification>,
        IUpdateRepository<Notification>, 
        IRepository
    {
        /// <summary>
        /// Gets a Dashboard Notification by account id
        /// </summary>
        /// <param name="accountId">The account id</param>
        /// <returns><see cref="DashboardNotification"/></returns>
        DashboardNotification GetFirstVisibleDashboardNotificationByAccount(Guid accountId);

        /// <summary>
        /// List notifications ordered by created date
        /// </summary>
        /// <param name="page">The page</param>
        /// <param name="size">The pagesize</param>
        /// <returns></returns>
        PageableSet<Notification> List(ulong page = 1, ulong size = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepository"/> class.
        /// </summary>
        public NotificationRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IIdentityRepository members.

        /// <inheritdoc /> 
        public Notification Find(Guid id)
        {
            return this.persistenceContext.Get<Notification>(id);
        }

        #endregion

        #region IUpdateRepository members.

        public void Update(Notification entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.persistenceContext.Update<Notification>(entity);
        }

        #endregion

        #region INotificationRepository members

        /// <inheritdoc />
        public DashboardNotification GetFirstVisibleDashboardNotificationByAccount(Guid accountId)
        {
            Account vissibleByAlias = null;
            DashboardNotification notificationAlias = null;
            var query = this.persistenceContext.QueryOver<DashboardNotification>(() => notificationAlias)
                .Where(x => x.IsActive)
                .And(x => x.Published)
                .And(x => x.PublishedDate <= DateTime.Now)
                .And(x => x.UnPublishedDate == null || x.UnPublishedDate >= DateTime.Now)
                .Left.JoinAlias(n => n.VisibleTo, () => vissibleByAlias)
                    .Where(
                        Restrictions.Or(
                            Restrictions.Eq(Projections.Property(() => vissibleByAlias.Id), accountId), 
                            Restrictions.Eq(Projections.Property<Notification>(x => x.IsVisibleToEveryone), true)))
                .WithSubquery.WhereValue(accountId).NotIn(QueryOver.Of<NotificationViewedBy>().Where(x => x.IsActive).And(x => x.Notification.Id == notificationAlias.Id).Select(x => x.Account.Id))
                .OrderBy(x => x.PublishedDate).Asc;

            return query.SingleOrDefault<DashboardNotification>();
        }

        #endregion


        #region IPagingAndSortingRepository members.

        /// <inheritdoc />
        public PageableSet<Notification> List(ulong page = 1, ulong size = 10)
        {
            var skip = (page - 1) * size;

            var query = this.persistenceContext.QueryOver<Notification>()
                .Where(x => x.IsActive)
                .OrderBy(x => x.CreatedAt).Desc
                .Skip((int)skip).Take((int)size);

            return new PageableSet<Notification>
            {
                CurrentPage = (long)page,
                NextPage = (long)page++,
                PageSize = (long)size,
                TotalCount = (long)query.ToRowCountQuery().RowCount(),
                Entities = query.List()
            };
        }

        #endregion
    }
}