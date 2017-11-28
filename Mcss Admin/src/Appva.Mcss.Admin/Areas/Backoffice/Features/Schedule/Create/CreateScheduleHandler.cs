// <copyright file="CreateScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Schedule.Create
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateScheduleHandler : RequestHandler<Parameterless<CreateScheduleModel>, CreateScheduleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        private readonly IArticleRepository articleRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateScheduleHandler"/> class.
        /// </summary>
        public CreateScheduleHandler(ITaxonomyService taxonomyService, ISettingsService settingsService, IArticleRepository articleRepository)
        {
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
            this.articleRepository = articleRepository;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override CreateScheduleModel Handle(Parameterless<CreateScheduleModel> message)
        {
            var orderListConfiguration = this.settingsService.Find(ApplicationSettings.OrderListSettings);
            List<SelectListItem> categorySelectList = null;

            if (orderListConfiguration.HasMigratedArticles)
            {
                var categories = this.articleRepository.GetCategories();
                categorySelectList = new List<SelectListItem>();
                categorySelectList.Add(new SelectListItem
                {
                    Text = "Välj",
                    Value = string.Empty,
                    Selected = true
                });

                foreach (var category in categories)
                {
                    categorySelectList.Add(new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    });
                }
            }

            return new CreateScheduleModel
            {
                Delegations = this.taxonomyService.Roots(TaxonomicSchema.Delegation).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                DeviationMessage = new ConfirmDeviationMessage(),
                ArticleModuleIsInstalled = orderListConfiguration.IsInstalled,
                Categories = categorySelectList
            };
        }

        #endregion
    }
}