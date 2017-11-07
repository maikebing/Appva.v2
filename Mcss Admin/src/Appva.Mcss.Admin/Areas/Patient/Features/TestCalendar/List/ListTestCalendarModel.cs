using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class ListTestCalendarModel
    {
        public DateTime Current { get; set; }
        public DateTime Next { get; set; }
        public DateTime Previous { get; set; }
        public IList<TestCalendarWeek> Calendar { get; set; }
        public PatientViewModel Patient { get; set; }
        public TestEventViewModel EventViewModel { get; set; }
        public IList<Guid> FilterList { get; set; }

        private int _iterator = 1;

        public int WeekIterator
        {
            get
            {
                return _iterator;
            }
            set
            {
                _iterator = value;
            }
        }

        public IDictionary<int, ScheduleSettings> Categories { get; set; }

        public bool IsBeginningOfWeek()
        {
            return WeekIterator.Equals(1);
        }

        public bool IsEndOfWeek()
        {
            return WeekIterator.Equals(7);
        }

        public Dictionary<string, object> CalendarSettings { get; set; }
        public IList<ScheduleSettings> CategorySettings { get; set; }
    }
}