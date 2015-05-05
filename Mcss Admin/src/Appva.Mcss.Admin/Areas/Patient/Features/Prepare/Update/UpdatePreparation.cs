// <copyright file="UpdatePreparation.cs" company="Appva AB">
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
    public sealed class UpdatePreparation : Identity<PrepareEditSequenceViewModel>
    {
        /// <summary>
        /// The <c>PreparedSequence</c> ID.
        /// </summary>
        public Guid PreparedSequenceId
        {
            get;
            set;
        }
    }
}