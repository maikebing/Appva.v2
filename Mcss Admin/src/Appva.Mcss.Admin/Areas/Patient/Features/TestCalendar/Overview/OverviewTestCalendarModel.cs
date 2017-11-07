using Appva.Mcss.Admin.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class OverviewTestCalendarModel
    {
        public IList<CalendarTask> OngoingEvents
        {
            get;
            set;
        }

        public IList<CalendarTask> CommingEvents
        {
            get;
            set;
        }
    }
}