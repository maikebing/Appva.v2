// <copyright file="CreateNotificationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Caching.Providers;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateNotificationPublisher : RequestHandler<CreateNotificationModel, Object>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNotificationPublisher"/> class.
        /// </summary>
        public CreateNotificationPublisher(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override Object Handle(CreateNotificationModel message)
        {
            var notice = new DashboardNotification
            {
                IsActive = true,
                IsVisibleToEveryone = true,
                Name = message.Name,
                Published = true,
                PublishedDate = message.StartDate,
                UnPublishedDate = message.EndDate,
                Template = string.Format("Templates/{0}", message.Template),
                ViewedBy = new List<NotificationViewedBy>(),
                VisibleTo = new List<Account>()
            };

            var entries = cache.List().Where(x => x.Key.ToString().StartsWith(CacheTypes.Persistence.FormatWith(string.Empty))).ToList();

            foreach (var entry in entries)
            {
                var factory = entry.Value as ISessionFactory;

                using (var context = factory.OpenSession())
                using (var transaction = context.BeginTransaction())
                {
                    var i = context.QueryOver<Notification>()
                        .Where(x => x.IsActive)
                        .And(x => x.Name == notice.Name)
                        .RowCount();

                    if (i == 0)
                    {
                        context.Save(notice);
                    }
                    transaction.Commit();
                }
            }
            return null;
        }

        #endregion
    }
}