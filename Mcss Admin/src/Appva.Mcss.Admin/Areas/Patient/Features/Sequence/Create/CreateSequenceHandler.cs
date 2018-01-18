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
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Resources;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Models;

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

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceHandler"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="delegations"></param>
        /// <param name="inventories"></param>
        /// <param name="settingsService"></param>
        /// <param name="roleService"></param>
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
                Id = message.Id,
                ScheduleId = schedule.Id,
                IsCollectingGivenDosage = schedule.ScheduleSettings.IsCollectingGivenDosage,
                DosageScales = schedule.ScheduleSettings.IsCollectingGivenDosage? this.GetDosageSelectList() : null,
                Delegations = schedule.ScheduleSettings.DelegationTaxon != null? this.delegations.ListDelegationTaxons(byRoot: schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false)
                    .Select(x => new SelectListItem { 
                        Text = x.Name,
                        Value = x.Id.ToString() })
                        : new List<SelectListItem>(),
                Times = CreateTimes().Select(x => new CheckBoxViewModel
                {
                    Id = x,
                    Checked = false
                }).ToList(),
                Inventories = schedule.ScheduleSettings.HasInventory ? this.inventories.Search(message.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }) : null,
                CreateNewInventory = true,
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
                6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
            };
        }

        /// <summary>
        /// Builds a select list,
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetDosageSelectList()
        {
            return this.settingsService.Find(ApplicationSettings.AdministrationUnitsWithAmounts)
                .Select(x => new SelectListItem { Text = x.ToLongString(), Value = x.Id.ToString() })
                .ToList();
        }

        #endregion
    }
}