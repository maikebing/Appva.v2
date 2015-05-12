// <copyright file="RenderChart.cs" company="Appva AB">
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
using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RenderChart : IRequest<IList<object[]>>
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
        /// Optional <see cref="ScheduleSetting"/> ID.
        /// </summary>
        public Guid? ScheduleSettingsId
        {
            get;
            set;
        }

        /// <summary>
        /// The start date.
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The end date.
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
    }
}