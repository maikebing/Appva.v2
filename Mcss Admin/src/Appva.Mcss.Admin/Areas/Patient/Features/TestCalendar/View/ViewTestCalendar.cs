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
    using Appva.Cqrs;
    using Appva.Domain;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class ViewTestCalendar : IRequest<ViewTestCalendarModel>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule ID.
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }

        /// <summary>
        /// The choosen date.
        /// </summary>
        public Date? Date
        {
            get;
            set;
        }
    }
}