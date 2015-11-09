// <copyright file="InstallTokenHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Tenant.Identity;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddVersion162NoticeHandler : NotificationHandler<AddVersion162Notice>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallTokenHandler"/> class.
        /// </summary>
        /// <param name="tenantService">The <see cref="ITenantService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public AddVersion162NoticeHandler(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(AddVersion162Notice notification)
        {
            var notice = new DashboardNotification
            {
                IsActive = true,
                IsVisibleToEveryone = true,
                Name = "Nyheter i MCSS 1.6.2",
                //NotificationType = NotificationType.DashboardNotification,
                Published = true,
                PublishedDate = DateTime.Now.Date,
                Template = "Templates/version1.6.2",
                ViewedBy = new List<NotificationViewedBy>(),
                VisibleTo = new List<Account>()
            };

            this.context.Save<DashboardNotification>(notice);
        }

        #endregion

    }
}