// <copyright file="UpdateSequenceMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Core.Extensions;
using Appva.Core.Resources;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateSequenceMedicationHandler : RequestHandler<UpdateSequenceMedication, UpdateSequenceMedicationForm>
    {
        #region Fields 

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceMedicationHandler"/> class.
        /// </summary>
        public UpdateSequenceMedicationHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        public override UpdateSequenceMedicationForm Handle(UpdateSequenceMedication message)
        {
            var sequence = this.persistence.Get<Sequence>(message.SequenceId);
            return new UpdateSequenceMedicationForm
            {
                SequenceId = message.SequenceId,
                Name = sequence.Name,
                Description = sequence.Description,
                StartDate = sequence.OnNeedBasis ? (DateTime?)null : sequence.Dates.IsEmpty() ? sequence.StartDate : (DateTime?)null,
                EndDate = sequence.OnNeedBasis ? (DateTime?)null : sequence.Dates.IsEmpty() ? sequence.EndDate : (DateTime?)null,
                RangeInMinutesBefore = sequence.RangeInMinutesBefore,
                RangeInMinutesAfter = sequence.RangeInMinutesAfter,
                Delegation = sequence.Taxon.IsNotNull() ? sequence.Taxon.Id : (Guid?)null,
                Delegations = this.GetDelegations(sequence.Schedule),
                Dates = sequence.Dates,
                Hour = sequence.Hour,
                Minute = sequence.Minute,
                Interval = sequence.Interval,
                Times = this.CreateTimes(sequence),
                OnNeedBasis = sequence.OnNeedBasis,
                OnNeedBasisStartDate = sequence.OnNeedBasis ? sequence.StartDate : (DateTime?)null,
                OnNeedBasisEndDate = sequence.OnNeedBasis ? sequence.EndDate : (DateTime?)null,
                Reminder = sequence.Reminder,
                ReminderInMinutesBefore = sequence.ReminderInMinutesBefore,
                Patient = sequence.Patient,
                Schedule = sequence.Schedule,
                Nurse = sequence.Role != null && sequence.Role.MachineName.Equals(RoleTypes.Nurse)
            };
        }

        #region Private members

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetDelegations(Schedule schedule)
        {
            var delegations = this.persistence.QueryOver<Taxon>()
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
                Text = x.Name,
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