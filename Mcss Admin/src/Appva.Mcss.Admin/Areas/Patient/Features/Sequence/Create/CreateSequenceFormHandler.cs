// <copyright file="CreateSequenceFormHandler.cs" company="Appva AB">
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
    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceFormHandler : RequestHandler<CreateSequencePostRequest, DetailsSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceFormHandler"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="inventories"></param>
        /// <param name="roleService"></param>
        /// <param name="auditing"></param>
        public CreateSequenceFormHandler(IPersistenceContext context, ISequenceService sequenceService, IInventoryService inventories, IRoleService roleService, ISettingsService settingsService, IAuditService auditing)
        {
            this.context         = context;
            this.sequenceService = sequenceService;
            this.inventories     = inventories;
            this.roleService     = roleService;
            this.auditing        = auditing;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(CreateSequencePostRequest message)
        {
            ////////////////////////////////////////////////////////////////////
            //// FIXME: Should we have article stuff here??? -> see history (?) 
            ////////////////////////////////////////////////////////////////////

            var sequence = this.CreateOrUpdate(message);
            var hm = sequence.Repeat.TakeNumberOfNextValid(Date.Today, 5);
            this.context.Save(sequence);
            this.auditing.Create(sequence.Patient, "skapade {0} (REF: {1}) i {2} (REF: {3}).", sequence.Name, sequence.Id, sequence.Schedule.ScheduleSettings.Name, sequence.Schedule.Id);
            return new DetailsSchedule
                {
                    Id = message.PatientId,
                    ScheduleId = message.ScheduleId
                };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private Sequence CreateOrUpdate(CreateSequencePostRequest message)
        {
            Schedule schedule   = null;
            Taxon delegation    = null;
            Role role           = null;
            Inventory inventory = null;
            schedule = this.context.Get<Schedule>(message.ScheduleId);
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
                    message.InventoryId = this.inventories.Create(message.Name, null, null, schedule.Patient);
                }
                inventory = this.inventories.Find(message.InventoryId.Value);
            }
            return new Sequence(schedule, message.Name, message.Instruction, Repeat.New(type), delegation, role, inventory);
        }

        #endregion
    }
}