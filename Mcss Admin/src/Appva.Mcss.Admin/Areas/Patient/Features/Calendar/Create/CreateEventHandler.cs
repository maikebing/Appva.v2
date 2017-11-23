// <copyright file="CreateEventHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Areas.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateEventHandler : RequestHandler<Identity<CreateEventModel>, CreateEventModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventHandler"/> class.
        /// </summary>
        public CreateEventHandler(ISequenceService sequenceService, ISettingsService settingsService)
        {
            this.sequenceService = sequenceService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateEventModel Handle(Identity<CreateEventModel> message)
        {
            var categories = this.sequenceService.GetCategories();
            var categorySelectlist = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            //// Shall check which role needed to have premissions to create categories.
            /*if (PermissionUtils.UserHasPermission(Identity(), "CreateCalendarCategory"))
            {
                categorySelectlist.Add(new SelectListItem
                {
                    Value = "new",
                    Text = "Skapa ny...",
                    Selected = false
                });
            }*/
            return new CreateEventModel
            {
                Id = message.Id,
                PatientId = message.Id,
                StartDate = DateTime.Now,
                StartTime = string.Format("{0:HH}:00", DateTime.Now.AddHours(1)),
                EndDate = DateTime.Now,
                EndTime = string.Format("{0:HH}:00", DateTime.Now.AddHours(2)),
                Categories = categorySelectlist,
                CalendarSettings = this.settingsService.GetCalendarSettings()
            };
        }

        #endregion
    }
}