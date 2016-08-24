// <copyright file="InstallTokenHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Caching.Providers;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddPdfNoticeHandler : NotificationHandler<AddPdfNotice>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPdfNoticeHandler"/> class.
        /// </summary>
        public AddPdfNoticeHandler(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(AddPdfNotice notification)
        {
            var notice = new DashboardNotification
            {
                IsActive = true,
                IsVisibleToEveryone = true,
                Name = "Nyheter - PDF",
                Published = true,
                PublishedDate = DateTime.Now.Date,
                Template = "Templates/pdf",
                ViewedBy = new List<NotificationViewedBy>(),
                VisibleTo = new List<Account>()
            };

            var entries = cache.List().Where(x => x.Key.ToString().StartsWith(CacheTypes.Persistence.FormatWith(string.Empty))).ToList();

            foreach (var entry in entries)
            {
                var factory = entry.Value as ISessionFactory;

                using (var context = factory.OpenSession())
                using(var transaction = context.BeginTransaction())
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