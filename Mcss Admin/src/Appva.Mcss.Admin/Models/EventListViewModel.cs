using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    
    public class EventListViewModel {

        public EventListViewModel() {
        }
        
        public DateTime Current { get; set; }
        public DateTime Next { get; set; }
        public DateTime Previous { get; set; }
        public IList<Calendar> Calendar { get; set; }
        public PatientViewModel Patient { get; set; }
        public EventViewModel EventViewModel { get; set; }
        public IList<string> FilterList { get; set; }

        private int _iterator = 1;

        public int WeekIterator { 
            get { 
                return _iterator; 
            }
            set { 
                _iterator = value; 
            }
        }

        public IDictionary<int,ScheduleSettings> Categories { get; set; }

        public bool IsBeginningOfWeek() {
            return WeekIterator.Equals(1);
        }

        public bool IsEndOfWeek() {
            return WeekIterator.Equals(7);
        }

        public Dictionary<string, object> CalendarSettings { get; set; }
        public IList<ScheduleSettings> CategorySettings { get; set; }

    }

}