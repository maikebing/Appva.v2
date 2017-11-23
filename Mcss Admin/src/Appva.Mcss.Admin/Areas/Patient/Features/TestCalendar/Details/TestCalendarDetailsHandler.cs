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

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        private readonly IScheduleService schedules;

        public TestCalendarDetailsHandler(ITaskService tasks, ISequenceService sequenceService, IScheduleService schedules)
        {
            this.tasks = tasks;
            this.sequenceService = sequenceService;
            this.schedules = schedules;
        }

        public override CalendarTask Handle(TestCalendarDetails message)
        {
            if (message.TaskId.IsNotEmpty())
            {
                return EventTransformer.TasksToEvent(this.tasks.Get(message.TaskId));
            }

            return this.sequenceService.GetActivityInSequence(message.SequenceId, message.EndTime);
        }
    }
}