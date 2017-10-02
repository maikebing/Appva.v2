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
    using Appva.Persistence;
    using NHibernate.Criterion;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface INotificationRepository :
        IRepository<Notification>,
        IUpdateRepository<Notification>
    {
        /// <summary>
        /// Gets a Dashboard Notification by account id.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns><see cref="DashboardNotification"/>.</returns>
        DashboardNotification GetFirstVisibleDashboardNotificationByAccount(Guid accountId);

        /// <summary>
        /// List notifications ordered by created date.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A paged collection of notifications.</returns>
        Paged<Notification> List(int page = 1, int pageSize = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public NotificationRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region INotificationRepository members

        /// <inheritdoc />
        public DashboardNotification GetFirstVisibleDashboardNotificationByAccount(Guid accountId)
        {
            Account vissibleByAlias                 = null;
            DashboardNotification notificationAlias = null;
            var notification = this.Context.QueryOver<DashboardNotification>(() => notificationAlias)
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
                .OrderBy(x => x.PublishedDate).Asc
                .Take(1)
                .SingleOrDefault();
            return notification;
        }

        #endregion

        #region IPagingAndSortingRepository members.

        /// <inheritdoc />
        public Paged<Notification> List(int page = 1, int pageSize = 10)
        {
            var pageQuery = PageQuery.New(page, pageSize);
            var query     = this.Context
                .QueryOver<Notification>()
                    .Where(x => x.IsActive)
                .OrderBy(x => x.CreatedAt).Desc
                .Skip(pageQuery.Skip)
                .Take(pageQuery.PageSize);
            var count = query.ToRowCountQuery().RowCount();
            var items = query.List();
            return Paged<Notification>.New(pageQuery, items, count);
        }

        #endregion

        public void UpdateMeasurementObservation(Notification entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.Context.Update<Notification>(entity);
        }
    }
}