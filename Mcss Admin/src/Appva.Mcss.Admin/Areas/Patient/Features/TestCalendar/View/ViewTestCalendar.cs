﻿using Appva.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class ViewTestCalendar : IRequest<ViewTestCalendarModel>
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime? Date { get; set; }
        public string Prev { get; set; }
        public string Next { get; set; }
    }
}