// <copyright file="CreateSequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Domain;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    public class ViewTestCalendarModel
    {
        /// <summary>
        /// The schedule ID.
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public PatientViewModel Patient
        {
            get;
            set;
        }
        public Date Current
        {
            get;
            set;
        }
        public Date Next
        {
            get;
            set;
        }
        public Date Previous
        {
            get;
            set;
        }

        public IList<TestCalendarWeek> Calendar
        {
            get;
            set;
        }
    }
}