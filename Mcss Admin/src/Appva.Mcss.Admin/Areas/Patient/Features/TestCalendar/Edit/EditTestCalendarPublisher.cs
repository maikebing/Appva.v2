using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Models;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class EditTestCalendarPublisher : RequestHandler<TestEventViewModel, ListTestCalendar>
    {
        private readonly ISequenceService sequenceService;

        public EditTestCalendarPublisher(ISequenceService sequenceService)
        {
            this.sequenceService = sequenceService;
        }

        public override ListTestCalendar Handle(TestEventViewModel message)
        {
            if (message.Category.Equals("new"))
            {
                message.Category = this.sequenceService.CreateCategory(message.NewCategory).ToString();
            }
            this.sequenceService.Update(
                message.SequenceId,
                new Guid(message.Category),
                message.Description,
                message.StartDate,
                message.EndDate,
                message.StartTime,
                message.EndTime,
                message.Interval,
                message.IntervalFactor,
                message.SpecificDate,
                message.Signable,
                message.VisibleOnOverview,
                message.AllDay,
                message.PauseAnyAlerts,
                message.Absent
            );

            return new ListTestCalendar
            {
                Date = message.ChoosedDate,
                Id = message.PatientId
            };
        }
    }
}