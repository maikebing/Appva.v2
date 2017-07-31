// <copyright file="MigrateCategoriesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Features.Accounts.List
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Admin.Features.Area51.ArticleOption;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MigrateCategoriesHandler : RequestHandler<MigrateCategories, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService service;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrateCategoriesHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public MigrateCategoriesHandler(IPersistenceContext persistence, ISettingsService service)
        {
            this.persistence = persistence;
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(MigrateCategories message)
        {
            var scheduleSettings = this.service.GetOrderListItemsFromScheduleSettings().List();

            foreach (var setting in scheduleSettings)
            {
                var category = new ArticleCategory();
                category.Name = setting.Name;
                this.persistence.Save(category);

                setting.ArticleCategory = category;
                this.persistence.Update(setting);
            }

            var orderListConfiguration = this.service.Find(ApplicationSettings.OrderListConfiguration);
            this.service.Upsert(ApplicationSettings.OrderListConfiguration, OrderListConfiguration.CreateNew(
                true,
                orderListConfiguration.HasMigratedArticles,
                orderListConfiguration.HasMigratableItems)
            );

            return true;
        }

        #endregion
    }
}