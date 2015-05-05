// <copyright file="CreatePreparation.cs" company="Appva AB">
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
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreatePreparation : Identity<PrepareAddSequenceViewModel>
    {
        /// <summary>
        /// The <c>Schedule</c> ID.
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }
    }
}