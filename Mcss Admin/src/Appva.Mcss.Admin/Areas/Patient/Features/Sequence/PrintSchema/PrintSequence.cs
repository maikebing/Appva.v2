// <copyright file="PrintSequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The print sequence request.
    /// </summary>
    public sealed class PrintSequence : Identity<FileContentResult>
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

        /// <summary>
        /// Whether or not on needs base.
        /// </summary>
        public bool OnNeedBasis
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: This is unknown.
        /// </summary>
        public bool StandardSequences
        {
            get;
            set;
        }
    }
}