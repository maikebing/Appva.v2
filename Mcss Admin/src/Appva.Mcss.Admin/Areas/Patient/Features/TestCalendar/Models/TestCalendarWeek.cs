using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public IList<CalendarDay> Days
        {
            get;
            set;
        }

        public List<CalendarTask> AllEvents
        {
            get;
            set;
        }

        #endregion
    }
}