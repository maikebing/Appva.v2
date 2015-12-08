// <copyright file="ListEventModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
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
    public sealed class ListEventModel
    {
        #region Properties.

        /// <summary>
        /// The patient id, optional
        /// </summary>
        public Guid? Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The startdate, optional
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The enddate, must be set
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// The category id, optional
        /// </summary>
        public Guid? Category
        {
            get;
            set;
        }

        #endregion
    }
}