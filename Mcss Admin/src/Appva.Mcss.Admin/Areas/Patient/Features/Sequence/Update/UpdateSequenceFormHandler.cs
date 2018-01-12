// <copyright file="UpdateSequenceHandler.cs" company="Appva AB">
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
    using Appva.Core.Utilities;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Extensions;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceFormHandler : RequestHandler<UpdateSequenceForm, DetailsSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

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
        /// Initializes a new instance of the <see cref="UpdateSequenceFormHandler"/> class.
        /// </summary>
        public UpdateSequenceFormHandler(IPersistenceContext context, ISequenceService sequenceService, IRoleService roleService, ISettingsService settingsService, IInventoryService inventoryService)
        {
            this.context            = context;
            this.roleService        = roleService;
            this.sequenceService    = sequenceService;
            this.inventoryService   = inventoryService;
            this.settingsService    = settingsService;
        }

        #endregion

        #region UpdateSequenceNotificationHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(UpdateSequenceForm message)
        {
            var sequence = this.CreateOrUpdate(message);
            this.sequenceService.Update(sequence);

            sequence.Schedule.UpdatedAt = DateTime.Now;
            this.context.Update(sequence.Schedule);
            return new DetailsSchedule
            {
                Id         = sequence.Patient.Id,
                ScheduleId = sequence.Schedule.Id
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private Sequence CreateOrUpdate(UpdateSequenceForm message)
        {
            Sequence sequence   = null;
            Schedule schedule   = null;
            Taxon delegation    = null;
            Role role           = null;
            Inventory inventory = null;
            sequence = this.sequenceService.Find(message.SequenceId);
            schedule = sequence.Schedule;
            var type = message.GetTypeOfSchedule();
            if (message.DelegationId.IsNotEmpty() && message.IsRequiredRole == false)
            {
                delegation = this.context.Get<Taxon>(message.DelegationId.Value); /* unable to combine role and delegation */
            }
            if (message.IsRequiredRole)
            {
                var temporary = this.settingsService.Find<Dictionary<Guid, Guid>>(ApplicationSettings.TemporaryScheduleSettingsRoleMap);
                if (temporary != null && temporary.ContainsKey(schedule.ScheduleSettings.Id))
                {
                    role = this.roleService.Find(temporary[schedule.ScheduleSettings.Id]);
                }
                if (role == null)
                {
                    role = this.roleService.Find(RoleTypes.Nurse);
                }
            }
            if (schedule.ScheduleSettings.HasInventory)
            {
                if (message.InventoryType == InventoryState.New)
                {
                    message.InventoryId = this.inventoryService.Create(message.Name, null, null, schedule.Patient);
                }
                inventory = this.inventoryService.Find(message.InventoryId.Value);
            }
            sequence.Name        = message.Name;
            sequence.Description = message.Instruction;
            sequence.Repeat      = Repeat.New(type);
            sequence.Taxon       = delegation;
            sequence.Role        = role;
            sequence.Inventory   = inventory;
            return sequence;
        }

        #endregion
    }
}