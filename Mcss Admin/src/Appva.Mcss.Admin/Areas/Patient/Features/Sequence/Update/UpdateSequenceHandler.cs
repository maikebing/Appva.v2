// <copyright file="UpdateSequenceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceHandler : RequestHandler<UpdateSequence, SequenceViewModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceHandler"/> class.
        /// </summary>
        public UpdateSequenceHandler(IPersistenceContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region RequestHandler<CreateSequence, SequenceViewModel> Overrides.

        /// <inheritdoc />
        public override SequenceViewModel Handle(UpdateSequence message)
        {
            /*var roleService = new RoleService(Session);
            var schedule = Session.Get<Schedule>(Sequence.Schedule.Id);
            var delegations = GetDelegations(schedule);
            DateTime? dummy = null;
            var model = new SequenceViewModel();
            model.Name = Sequence.Name;
            model.Description = Sequence.Description;
            model.StartDate = (Sequence.OnNeedBasis) ? dummy : (Sequence.Dates.IsEmpty()) ? Sequence.StartDate : dummy;
            model.EndDate = (Sequence.OnNeedBasis) ? dummy : (Sequence.Dates.IsEmpty()) ? Sequence.EndDate : dummy;
            model.RangeInMinutesBefore = Sequence.RangeInMinutesBefore;
            model.RangeInMinutesAfter = Sequence.RangeInMinutesAfter;
            model.Delegation = (Sequence.Taxon.IsNotNull()) ? Sequence.Taxon.Id : Guid.Empty;
            model.Delegations = delegations;
            model.Dates = Sequence.Dates;
            model.Hour = Sequence.Hour;
            model.Minute = Sequence.Minute;
            model.Interval = Sequence.Interval;
            model.Times = CreateTimes().Select(x => new CheckBoxViewModel
            {
                Id = x,
                Checked = false
            }).ToList();
            if (Sequence.Times.IsNotEmpty())
            {
                var times = Sequence.Times.Split(',');
                foreach (var time in times)
                {
                    var value = 0;
                    if (int.TryParse(time, out value))
                    {
                        foreach (var checkbox in model.Times)
                        {
                            if (checkbox.Id == value)
                            {
                                checkbox.Checked = true;
                            }
                        }
                    }
                }
            }
            model.OnNeedBasis = Sequence.OnNeedBasis;
            model.OnNeedBasisStartDate = (Sequence.OnNeedBasis) ? Sequence.StartDate : dummy;
            model.OnNeedBasisEndDate = (Sequence.OnNeedBasis) ? Sequence.EndDate : dummy;
            model.Reminder = Sequence.Reminder;
            model.ReminderInMinutesBefore = Sequence.ReminderInMinutesBefore;
            model.Patient = Sequence.Patient;
            model.Schedule = Sequence.Schedule;
            model.Nurse = Sequence.Role != null && Sequence.Role.MachineName.Equals("_TITLE_N");
            Result = model;*/
            return null;
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
                .OrderBy(x => x.Weight).Asc
                .ThenBy(x => x.Name).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                .Where(x => x.MachineName == TaxonomicSchema.Delegation.Id).List();
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
        private IList<int> CreateTimes()
        {
            return new List<int>
            {
                6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
            };
        }

        #endregion
    }
}