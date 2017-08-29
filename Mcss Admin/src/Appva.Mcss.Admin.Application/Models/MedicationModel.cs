// <copyright file="MedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MedicationModel
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string StrenghtText
        {
            get;
            set;
        }

        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime? EndDate
        {
            get;
            set;
        }

        public string Form
        {
            get;
            set;
        }

        public string Dosage
        {
            get;
            set;
        }

        public PackageType PackageType
        {
            get;
            set;
        }

        public string ScheduleText
        {
            get;
            set;
        }

        #endregion
    }

    public enum PackageType
    {
        Dispenzed,
        Scheduled,
        NeedBased
    }
}