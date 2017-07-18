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
        private readonly Persistence.IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleOptionListHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ISettingsService"/>.</param>
        public ArticleOptionListHandler(ISettingsService service, Persistence.IPersistenceContext persistence)
        {
            this.service = service;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ArticleOption Handle(Parameterless<ArticleOption> message)
        {
            var categorySetting = this.service.Find(ApplicationSettings.HasCreatedCategories);
            var articleSetting = this.service.Find(ApplicationSettings.HasMigratedArticles);

            return new ArticleOption
            {
                HasCreatedCategories = categorySetting,
                HasMigratedArticles = articleSetting
            };
        }

        #endregion
    }
}