// <copyright file="EnableOrderListHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EnableOrderListHandler : RequestHandler<EnableOrderList, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService service;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EnableOrderListHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ISettingsService"/>.</param>
        public EnableOrderListHandler(ISettingsService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(EnableOrderList message)
        {
            var settings = this.service.Find(ApplicationSettings.OrderListConfiguration);
            this.service.Upsert(ApplicationSettings.OrderListConfiguration, OrderListConfiguration.CreateNew(
                settings.HasCreatedCategories, 
                settings.HasMigratedArticles, 
                true, 
                settings.IsEnableable)
            );

            return true;
        }

        #endregion
    }
}