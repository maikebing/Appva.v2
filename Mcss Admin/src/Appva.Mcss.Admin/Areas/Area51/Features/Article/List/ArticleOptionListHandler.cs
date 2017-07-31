// <copyright file="ArticleOptionListHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Admin.Features.Area51.ArticleOption;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ArticleOptionListHandler : RequestHandler<Parameterless<ArticleOption>, ArticleOption>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService service;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleOptionListHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ISettingsService"/>.</param>
        public ArticleOptionListHandler(ISettingsService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ArticleOption Handle(Parameterless<ArticleOption> message)
        {
            var hasMigratableItems = this.service.HasMigratableItems();
            var settings = this.service.Find(ApplicationSettings.OrderListConfiguration);
            this.service.Upsert(ApplicationSettings.OrderListConfiguration, OrderListConfiguration.CreateNew(
                settings.HasCreatedCategories,
                settings.HasMigratedArticles,
                hasMigratableItems)
            );

            return new ArticleOption
            {
                HasCreatedCategories = settings.HasCreatedCategories,
                HasMigratedArticles = settings.HasMigratedArticles,
                HasMigratableItems = hasMigratableItems
            };
        }

        #endregion
    }
}