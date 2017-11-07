using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class CreateTestCalendarHandler : RequestHandler<Identity<CreateTestCalendar>, CreateTestCalendar>
    {
        private readonly IEventService eventService;
        private readonly ISettingsService settingsService;

        public CreateTestCalendarHandler(IEventService eventService, ISettingsService settingsService)
        {
            this.eventService = eventService;
            this.settingsService = settingsService;
        }

        public override CreateTestCalendar Handle(Identity<CreateTestCalendar> message)
        {
            var categories = this.eventService.GetCategories();
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

            return new CreateTestCalendar
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
    }
}