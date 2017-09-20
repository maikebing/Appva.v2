// <copyright file="UpdateSequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.se">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using Appva.Cqrs;

    #endregion

    public class UpdateSchedule : IRequest<UpdateScheduleForm>
    {
        /// <summary>
        /// The Patient ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Schedule ID
        /// </summary>
        public Guid ScheduleId { get; set; }
    }
}