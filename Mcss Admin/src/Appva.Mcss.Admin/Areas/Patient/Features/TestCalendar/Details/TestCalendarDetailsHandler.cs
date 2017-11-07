using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class TestCalendarDetailsHandler : RequestHandler<TestCalendarDetails, CalendarTask>
    {
        private readonly ITaskService tasks;
        private readonly IEventService events;
        private readonly IScheduleService schedules;

        public TestCalendarDetailsHandler(ITaskService tasks, IEventService events, IScheduleService schedules)
        {
            this.tasks = tasks;
            this.events = events;
            this.schedules = schedules;
        }

        public override CalendarTask Handle(TestCalendarDetails message)
        {
            if (message.TaskId.IsNotEmpty())
            {
                return EventTransformer.TasksToEvent(this.tasks.Get(message.TaskId));
            }

            return this.events.GetActivityInSequence(message.SequenceId, message.EndTime);
        }
    }
}