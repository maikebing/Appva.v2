using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Domain;

namespace Appva.Mcss.Admin.Areas.Patient.Features.TestCalender.Models
{
    public class TestCalendarTask
    {
        /// <summary>
        /// The id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The sequence ID.
        /// </summary>
        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The repeat.
        /// </summary>
        public Repeat Repeat
        {
            get;
            set;
        }

        /// <summary>
        /// The event start date and time.
        /// </summary>
        public DateTime Start
        {
            get;
            set;
        }

        /// <summary>
        /// The event end date and time.
        /// </summary>
        public DateTime End
        {
            get;
            set;
        }

        /// <summary>
        /// The  name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The event description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The patient id
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The patient fullname
        /// </summary>
        public string PatientName
        {
            get;
            set;
        } 
    }
}