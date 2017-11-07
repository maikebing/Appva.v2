using Appva.Mcss.Admin.Models;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class EditTestCalendar : Identity<TestEventViewModel>
    {
        /// <summary>
        /// The sequence ID.
        /// </summary>
        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The event date.
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }
    }
}