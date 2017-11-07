using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class TestCalendarDetails : IRequest<CalendarTask>
    {
        /// <summary>
        /// The task id
        /// </summary>
        public Guid TaskId
        {
            get;
            set;
        }

        /// <summary>
        /// The Sequence id
        /// </summary>
        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The Starttime of the event
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// The endtime of the event
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

    }
}