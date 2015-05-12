﻿// <copyright file="InactivateSequence.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class InactivateSequence : IRequest<DetailsSchedule>
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
        /// The sequence ID.
        /// </summary>
        public Guid SequenceId
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
    }
}