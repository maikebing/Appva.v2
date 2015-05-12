using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using System.Web.Mvc;
using Appva.Mcss.Web.ViewModels;
using Appva.Persistence;
using Appva.Mcss.Admin.Domain.Repositories;
using Appva.Mcss.Admin.Application.Services;

namespace Appva.Mcss.Web.Mappers {

    /*public class SequencePageViewModelBuilder {

        public static CreateSequenceModel Create(IPersistenceContext session, Guid scheduleId)
        {
            return CreateOrUpdate(session, scheduleId, null);
        }

        public static CreateSequenceModel Update(IPersistenceContext session, Guid scheduleId, CreateSequenceModel model)
        {
            return CreateOrUpdate(session, scheduleId, model);
        }

        public static CreateSequenceModel Edit(IPersistenceContext session, Sequence sequence)
        {
            var roleService = new RoleService(new RoleRepository(session));
            var reminderRecipients = roleService.MembersOfRole("reminderrecipients");
            var schedule = session.Get<Schedule>(sequence.Schedule.Id);
            var delegations = session.QueryOver<Delegation>()
                .Where(x => x.IsActive == true && x.Pending == false)
                .OrderBy(t => t.Taxon.Parent).Asc
                .ThenBy(x => x.Name).Asc
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .And(x => x.Parent.Id == schedule.ScheduleSettings.DelegationTaxon.Id)
                .List();
            DateTime? dummy = null;
            var model = new CreateSequenceModel();
            model.Name = sequence.Name;
            model.Description = sequence.Description;
            model.StartDate = (sequence.OnNeedBasis) ? dummy : (sequence.Dates.IsEmpty()) ? sequence.StartDate : dummy;
            model.EndDate = (sequence.OnNeedBasis) ? dummy : (sequence.Dates.IsEmpty()) ? sequence.EndDate : dummy;
            model.RangeInMinutesBefore = sequence.RangeInMinutesBefore;
            model.RangeInMinutesAfter = sequence.RangeInMinutesAfter;
            model.Delegations = delegations.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            model.Dates = sequence.Dates;
            model.Hour = sequence.Hour;
            model.Minute = sequence.Minute;
            model.Interval = sequence.Interval;
            model.Intervals = new List<SelectListItem>() {
                new SelectListItem() { Text = "Varje dag", Value = "1" },
                new SelectListItem() { Text = "Varannan dag", Value = "2" },
                new SelectListItem() { Text = "Var 3:e dag", Value = "3" },
                new SelectListItem() { Text = "Var 4:e dag", Value = "4" },
                new SelectListItem() { Text = "Annan ...", Value = "0" }
            };
            model.Times = Enumerable.Range(1, 24).Select(x => new CheckBoxViewModel { Id = x, Checked = false }).ToList();
            if (sequence.Times.IsNotEmpty()) {
                var times = sequence.Times.Split(',');
                foreach (var time in times) {
                    var value = 0;
                    if (int.TryParse(time, out value)) {
                        foreach (var checkbox in model.Times) {
                            if (checkbox.Id == value) {
                                checkbox.Checked = true;
                            }
                        }
                    }
                }
            }
            model.OnNeedBasis = sequence.OnNeedBasis;
            model.OnNeedBasisStartDate = (sequence.OnNeedBasis) ? sequence.StartDate : dummy;
            model.OnNeedBasisEndDate = (sequence.OnNeedBasis) ? sequence.EndDate : dummy;
            model.Reminder = sequence.Reminder;
            model.ReminderInMinutesBefore = sequence.ReminderInMinutesBefore;
            model.Patient = sequence.Patient;
            model.Schedule = sequence.Schedule;
            return model;
        }

        private static CreateSequenceModel CreateOrUpdate(IPersistenceContext session, Guid scheduleId, CreateSequenceModel model)
        {
            var roleService = new RoleService(new RoleRepository(session));
            var reminderRecipients = roleService.MembersOfRole("reminderrecipients");
            var schedule = session.Get<Schedule>(scheduleId);
            var delegations = session.QueryOver<ScheduleSettings>()
                .JoinQueryOver<Taxon>(x => x.DelegationTaxon)
                .Where(x => x.Parent.Id == schedule.ScheduleSettings.DelegationTaxon.Id)
                .JoinQueryOver<Delegation>(x => x.Delegations)
                .Where(x => x.IsActive == true && x.Pending == false)
                .List();
            model = model.IsNull() ? new CreateSequenceModel() : model;
            model.Delegations = delegations.Select(x => new SelectListItem { Text = x.Name, Value = x.DelegationTaxon.ToString() }).ToList();
            model.Intervals = new List<SelectListItem>() {
                new SelectListItem() { Text = "Varje dag", Value = "1" },
                new SelectListItem() { Text = "Varannan dag", Value = "2" },
                new SelectListItem() { Text = "Var 3:e dag", Value = "3" },
                new SelectListItem() { Text = "Var 4:e dag", Value = "4" },
                new SelectListItem() { Text = "Annan ...", Value = "0" }
            };
            model.Times = Enumerable.Range(1, 24).Select(x => new CheckBoxViewModel { Id = x, Checked = false }).ToList();
            return model;
        }


    }*/

}