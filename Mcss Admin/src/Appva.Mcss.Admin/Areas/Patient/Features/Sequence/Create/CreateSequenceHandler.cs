// <copyright file="CreateSequenceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Domain;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceHandler : RequestHandler<CreateSequence, CreateSequenceForm>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegations;

        /// <summary>
        /// The <see cref="IInventoryService"/>
        /// </summary>
        private readonly IInventoryService inventories;

        /// <summary>
        /// The <see cref="IRoleService"/>
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceHandler"/> class.
        /// </summary>
        public CreateSequenceHandler(IPersistenceContext context, IDelegationService delegations, IInventoryService inventories, ISettingsService settingsService, IRoleService roleService)
        {
            this.context = context;
            this.delegations = delegations;
            this.inventories = inventories;
            this.roleService = roleService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateSequenceForm Handle(CreateSequence message)
        {
            var patient  = this.context.Get<Patient>(message.PatientId);
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            //// Temporary mapping
            Role requiredRole = null;
            var temp = this.settingsService.Find<Dictionary<Guid, Guid>>(ApplicationSettings.TemporaryScheduleSettingsRoleMap);
            if (temp != null && temp.ContainsKey(schedule.ScheduleSettings.Id))
            {
                requiredRole = this.roleService.Find(temp[schedule.ScheduleSettings.Id]);
            }
            if (requiredRole == null)
            {
                requiredRole = this.roleService.Find(RoleTypes.Nurse);
            }
            return new CreateSequenceForm
            {
                StartDate   = Date.Today,
                StartHour   = TimeOfDay.Now.Hour,
                StartMinute = TimeOfDay.Now.Minute,
                EndDate     = null,
                EndHour     = 23,
                EndMinute   = 59,
                PatientId   = patient.Id,
                ScheduleId  = schedule.Id,
                Delegations = schedule.ScheduleSettings.DelegationTaxon != null ? this.delegations.ListDelegationTaxons(byRoot: schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false)
                    .Select(x => new SelectListItem { 
                        Text = x.Name,
                        Value = x.Id.ToString() })
                        : new List<SelectListItem>(),
                Times = CreateTimes().Select(x => new CheckBoxViewModel
                {
                    Id = x,
                    Checked = false
                }).ToList(),
                Inventories        = schedule.ScheduleSettings.HasInventory ? this.inventories.Search(patient.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }) : null,
                CreateNewInventory = schedule.ScheduleSettings.HasInventory,
                RequiredRoleText   = requiredRole.Name.ToLower()
                
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: REFACTOR?
        /// </summary>
        /// <returns></returns>
        private IList<int> CreateTimes()
        {
            return new List<int>
            {
                6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 0, 1, 2, 3, 4, 5
            };
        }

        #endregion
    }
}