using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Areas.Patient.Features.TestCalendar.Models;
using Appva.Mcss.Admin.Areas.Patient.Features.TestCalender.Models;

namespace Appva.Mcss.Admin.Application.Models
{
    public class TestCalendarWeek
    {
        #region Properties

        /// <summary>
        /// The number of the week
        /// </summary>
        public int WeekNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The days within the week
        /// </summary>
        public IList<TestCalendarDay> Days
        {
            get;
            set;
        }

        public List<TestCalendarTask> AllEvents
        {
            get;
            set;
        }

        #endregion
    }
}