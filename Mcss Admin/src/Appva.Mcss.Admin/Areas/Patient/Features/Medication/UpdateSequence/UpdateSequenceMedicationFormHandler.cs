// <copyright file="UpdateSequenceMedicationFormHandler.cs" company="Appva AB">
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
    using Appva.Core.Utilities;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateSequenceMedicationFormHandler : RequestHandler<UpdateSequenceMedicationForm, DetailsMedication>
    {
        #region Field

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly ISequenceService sequences;

        /// <summary>
        /// The <see cref="IRoleService"/>
        /// </summary>
        private readonly IRoleService roles;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceMedicationFormHandler"/> class.
        /// </summary>
        public UpdateSequenceMedicationFormHandler(IPersistenceContext persistence, ISequenceService sequences, IRoleService roles)
        {
            this.persistence = persistence;
            this.sequences = sequences;
            this.roles = roles;
        }

        #endregion

        #region Overrides

        public override DetailsMedication Handle(UpdateSequenceMedicationForm message)
        {
            var sequence = this.sequences.Find(message.SequenceId); 
            var schedule = this.persistence.Get<Schedule>(sequence.Schedule.Id);
            Account recipient = null;
            Taxon delegation = null;
            if (message.Delegation.HasValue && !message.Nurse)
            {
                delegation = this.persistence.Get<Taxon>(message.Delegation.Value);
            }
            sequence = this.CreateOrUpdate(message, sequence, schedule, delegation, recipient);
            this.persistence.Update(sequence);
            
            schedule.UpdatedAt = DateTime.Now;
            this.persistence.Update(schedule);
            return new DetailsMedication
            {
                Id = sequence.Patient.Id,
                MedicationId = sequence.ExternalId
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        private Sequence CreateOrUpdate(UpdateSequenceMedicationForm model, Sequence sequence, Schedule schedule, Taxon delegation, Account recipient)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate = null;
            DateTime tempDate;
            Role requiredRole = null;
            if (model.Dates.IsNotEmpty() && model.Interval == 0)
            {
                var dates = model.Dates.Split(',');
                if (dates.Count() > 0)
                {
                    if (!DateTime.TryParse(dates[0], out startDate))
                    {
                        startDate = DateTimeUtilities.Now();
                    }
                    if (DateTime.TryParse(dates[dates.Count() - 1], out tempDate))
                    {
                        endDate = tempDate;
                    }
                }
            }
            if (model.Interval > 0)
            {
                model.Dates = null;
            }
            if (model.Nurse)
            {
                requiredRole = this.roles.Find(RoleTypes.Nurse);
            }

            if (model.OnNeedBasis)
            {
                if (model.OnNeedBasisStartDate.HasValue)
                {
                    startDate = model.OnNeedBasisStartDate.Value;
                }
                if (model.OnNeedBasisEndDate.HasValue)
                {
                    endDate = model.OnNeedBasisEndDate.Value;
                }
            }
            else
            {
                if (model.StartDate.HasValue)
                {
                    startDate = model.StartDate.Value;
                }
                if (model.EndDate.HasValue)
                {
                    endDate = model.EndDate.Value;
                }
            }

            if (schedule.ScheduleSettings.HasInventory && sequence.Inventory == null)
            {
                var inventory = new Inventory()
                {
                    CurrentLevel = (double)sequence.StockAmount,
                    Description = sequence.Name,
                    LastRecount = sequence.LastStockAmountCalculation //TODO: If null today
                };
                this.persistence.Save(inventory);
                sequence.Inventory = inventory;
            }
            sequence.Name = model.Name;
            sequence.Description = model.Description;
            sequence.StartDate = startDate;
            sequence.EndDate = endDate;
            sequence.RangeInMinutesBefore = model.RangeInMinutesBefore;
            sequence.RangeInMinutesAfter = model.RangeInMinutesAfter;
            sequence.Times = string.Join(",", model.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray());
            sequence.Dates = model.Dates;
            sequence.Hour = model.Hour;
            sequence.Minute = model.Minute;
            sequence.Interval = model.OnNeedBasis ? 1 : model.Interval.Value;
            sequence.OnNeedBasis = model.OnNeedBasis;
            sequence.Reminder = model.Reminder;
            sequence.ReminderInMinutesBefore = model.ReminderInMinutesBefore;
            sequence.ReminderRecipient = recipient; //// FIXME: This is always NULL why is it here at all? 
            ////sequence.Patient = Patient; // unnecassary
            sequence.Schedule = schedule;
            sequence.Taxon = delegation;
            sequence.Role = requiredRole;
            sequence.IsActive = true;
            return sequence;


        }

        #endregion
    }
}