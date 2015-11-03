using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;


namespace Appva.Mcss.Admin.Application.Models
{

    public class Calendar {

        public bool IsWithinMonth { get; set; }
        public bool IsToday { get; set; }
        public DateTime Date { get; set; }
        public IList<CalendarTask> Events { get; set; }

        public int NumberOfEvents { get; set; }
    }

}
