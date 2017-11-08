﻿using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Models
{
    public class ViewTestCalendarModel
    {
        public PatientViewModel Patient { get; set; }
        public DateTime Current { get; set; }
        public DateTime Next { get; set; }
        public DateTime Previous { get; set; }
        public IList<CalendarWeek> Calendar { get; set; }
        // public Dictionary<int, ScheduleSettings> Categories { get; set; }
        public IList<ScheduleSettings> CategorySettings { get; set; }
    }
}