// <copyright file="DeletePreparation.cs" company="Appva AB">
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
    public sealed class MarkPreparation : Identity<string>
    {
        /// <summary>
        /// The <c>PreparedSequence</c> ID.
        /// </summary>
        public Guid PreparedSequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The date.
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not to unmark the task.
        /// </summary>
        public bool UnMark
        {
            get;
            set;
        }
    }
}