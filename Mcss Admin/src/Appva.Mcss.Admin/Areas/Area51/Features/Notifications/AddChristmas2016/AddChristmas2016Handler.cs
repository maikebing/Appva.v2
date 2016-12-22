// <copyright file="AddChristmas2016Handler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Handlers
{
    #region Imports.

    using Appva.Caching.Providers;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Features.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddChristmas2016Handler : NotificationHandler<AddChristmas2016Notice>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddChristmas2016Handler"/> class.
        /// </summary>
        public AddChristmas2016Handler(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(AddChristmas2016Notice notification)
        {
            var notice = new DashboardNotification
            {
                IsActive = true,
                IsVisibleToEveryone = true,
                Name = "God Jul och Gott Nytt år 2016",
                Published = true,
                PublishedDate = new DateTime(2016, 12, 19),
                UnPublishedDate = new DateTime(2017, 1, 6),
                Template = "Templates/christmas2016",
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
        }

        #endregion
    }
}