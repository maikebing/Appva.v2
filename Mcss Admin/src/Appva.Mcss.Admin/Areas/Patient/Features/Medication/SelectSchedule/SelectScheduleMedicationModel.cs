// <copyright file="SelectScheduleMedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SelectScheduleMedicationModel 
    {
        #region Properties.

        /// <summary>
        /// The patient id 
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The ordination id
        /// </summary>
        public string OrdinationId
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule for the medication
        /// </summary>
        public Guid Schedule
        {
            get;
            set;
        }

        #endregion

        #region Lists.

        /// <summary>
        /// The patients schedules
        /// </summary>
        public IList<SelectListItem> Schedules
        {
            get;
            set;
        }

        #endregion
    }
}