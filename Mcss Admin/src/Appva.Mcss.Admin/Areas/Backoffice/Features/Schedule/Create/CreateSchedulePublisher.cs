// <copyright file="CreateSchedulePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSchedulePublisher : RequestHandler<CreateScheduleModel, Identity<DetailsScheduleModel>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSchedulePublisher"/> class.
        /// </summary>
        public CreateSchedulePublisher(IScheduleService scheduleService, ITaxonomyService taxonomyService, IArticleRepository articleRepository)
        {
            this.taxonomyService = taxonomyService;
            this.scheduleService = scheduleService;
            this.articleRepository = articleRepository;
        }

        #endregion

        #region

        /// <inheritdoc />
        public override Identity<DetailsScheduleModel> Handle(CreateScheduleModel message)
        {
            ArticleCategory articleCategory = null;

            if (message.DeviationMessage.IsNull())
            {
                message.DeviationMessage = new ConfirmDeviationMessage();
            }

            if(string.IsNullOrEmpty(message.SelectedCategory) == false)
            {
                articleCategory = this.articleRepository.GetCategory(new Guid(message.SelectedCategory));
            }

            var schedule = new ScheduleSettings
            {
                Absence = false,
                AlternativeName = message.AlternativeName,
                CanRaiseAlerts = message.CanRaiseAlerts,
                CountInventory = message.CountInventory,
                DelegationTaxon = message.DelegationTaxon.HasValue ? this.taxonomyService.Load(message.DelegationTaxon.Value) : null,
                GenerateIncompleteTasks = true,
                HasInventory = message.HasInventory,
                HasSetupDrugsPanel = message.HasSetupDrugsPanel,
                IsActive = true,
                IsPausable = message.IsPausable,
                MachineName = Guid.NewGuid().ToString(),
                Name = message.Name,
                NurseConfirmDeviation = message.NurseConfirmDeviation,
                NurseConfirmDeviationMessage = message.DeviationMessage.ToHtmlString(),
                OrderRefill = message.HasMigratedArticles ? (string.IsNullOrEmpty(message.SelectedCategory) ? false : true) : message.OrderRefill,
                ScheduleType = ScheduleType.Action,
                SpecificNurseConfirmDeviation = message.DeviationMessage.IncludeListOfNurses,
                ArticleCategory = articleCategory
            };

            this.scheduleService.SaveScheduleSetting(schedule);

            return new Identity<DetailsScheduleModel>
            {
                Id = schedule.Id
            };
        }

        #endregion
    }
}