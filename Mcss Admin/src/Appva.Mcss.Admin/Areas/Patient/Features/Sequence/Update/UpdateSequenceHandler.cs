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
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceHandler : RequestHandler<UpdateSequence, UpdateSequenceForm>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceHandler"/> class.
        /// </summary>
        public UpdateSequenceHandler( IInventoryService inventories, IRoleService roleService, ISettingsService settingsService, IPersistenceContext context)
        {
            this.inventories     = inventories;
            this.roleService     = roleService;
            this.settingsService = settingsService;
            this.context         = context;
        }

        #endregion

        #region RequestHandler<CreateSequence, SequenceViewModel> Overrides.

        /// <inheritdoc />
        public override UpdateSequenceForm Handle(UpdateSequence message)
        {
            //// FIXME: Log here!
            var sequence     = this.context.Get<Sequence>(message.SequenceId);
            var schedule     = this.context.Get<Schedule>(sequence.Schedule.Id);
            //// Temporary mapping
            Role requiredRole = null;
            var temp  = this.settingsService.Find<Dictionary<Guid, Guid>>(ApplicationSettings.TemporaryScheduleSettingsRoleMap);
            if (temp != null && temp.ContainsKey(schedule.ScheduleSettings.Id))
            {
                requiredRole = this.roleService.Find(temp[schedule.ScheduleSettings.Id]);
            }
            if (requiredRole == null)
            {
                requiredRole = this.roleService.Find(RoleTypes.Nurse);
            }
            return new UpdateSequenceForm
            {
                Id                          = message.Id,
                Name                        = sequence.Name,
                Description                 = sequence.Description,
                StartDate                   = sequence.OnNeedBasis ? (DateTime?) null : sequence.Dates.IsEmpty() ? sequence.StartDate : (DateTime?) null,
                EndDate                     = sequence.OnNeedBasis ? (DateTime?) null : sequence.Dates.IsEmpty() ? sequence.EndDate : (DateTime?) null,
                RangeInMinutesBefore        = sequence.RangeInMinutesBefore,
                RangeInMinutesAfter         = sequence.RangeInMinutesAfter,
                IsCollectingGivenDosage     = sequence.Schedule.ScheduleSettings.IsCollectingGivenDosage,
                Delegation                  = sequence.Taxon.IsNotNull() ? sequence.Taxon.Id : (Guid?) null,
                Delegations                 = this.GetDelegations(schedule),
                Dates                       = sequence.Dates,
                Interval                    = sequence.Interval,
                Times                       = this.CreateTimes(sequence),
                OnNeedBasis                 = sequence.OnNeedBasis,
                OnNeedBasisStartDate        = sequence.OnNeedBasis ? sequence.StartDate : (DateTime?) null,
                OnNeedBasisEndDate          = sequence.OnNeedBasis ? sequence.EndDate   : (DateTime?) null,
                Reminder                    = sequence.Reminder,
                ReminderInMinutesBefore     = sequence.ReminderInMinutesBefore,
                Patient                     = sequence.Patient,
                Schedule                    = sequence.Schedule,
                Nurse                       = sequence.Role != null,
                Inventory                   = sequence.Inventory.IsNotNull() ? sequence.Inventory.Id : Guid.Empty,
                Inventories                 = schedule.ScheduleSettings.HasInventory ? this.inventories.Search(message.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }) : null,
                RequiredRoleText            = requiredRole.Name.ToLower()
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetDelegations(Schedule schedule)
        {
            var delegations = this.context.QueryOver<Taxon>()
                .Where(x => x.IsActive == true)
                .And(x => x.IsRoot == false)
                .And(x => x.Parent == schedule.ScheduleSettings.DelegationTaxon)
                .OrderBy(x => x.Weight).Asc
                .ThenBy(x => x.Name).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == TaxonomicSchema.Delegation.Id)
                .List();
            return delegations.Select(x => new SelectListItem
            {
                Text  = x.Name,
                Value = x.Id.ToString()
            });
        }

        /// <summary>
        /// TODO: REFACTOR?
        /// </summary>
        /// <returns></returns>
        private IList<CheckBoxViewModel> CreateTimes(Sequence sequence)
        {
            var checkBoxList = new List<CheckBoxViewModel>
            {
                new CheckBoxViewModel(6),
                new CheckBoxViewModel(7),
                new CheckBoxViewModel(8),
                new CheckBoxViewModel(9),
                new CheckBoxViewModel(10),
                new CheckBoxViewModel(11),
                new CheckBoxViewModel(12),
                new CheckBoxViewModel(13),
                new CheckBoxViewModel(14),
                new CheckBoxViewModel(15),
                new CheckBoxViewModel(16),
                new CheckBoxViewModel(17),
                new CheckBoxViewModel(18),
                new CheckBoxViewModel(19),
                new CheckBoxViewModel(20),
                new CheckBoxViewModel(21),
                new CheckBoxViewModel(22),
                new CheckBoxViewModel(23),
                new CheckBoxViewModel(24),
                new CheckBoxViewModel(1),
                new CheckBoxViewModel(2),
                new CheckBoxViewModel(3),
                new CheckBoxViewModel(4),
                new CheckBoxViewModel(5),
            };

            if (sequence.Times.IsNotEmpty())
            {
                var times = sequence.Times.Split(',');
                foreach (var time in times)
                {
                    var value = 0;
                    if (int.TryParse(time, out value))
                    {
                        foreach (var checkbox in checkBoxList)
                        {
                            if (checkbox.Id == value)
                            {
                                checkbox.Checked = true;
                            }
                        }
                    }
                }
            }
            return checkBoxList;
        }

        #endregion
    }
}