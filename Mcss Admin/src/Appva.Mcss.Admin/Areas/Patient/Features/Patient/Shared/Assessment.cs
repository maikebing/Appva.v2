// <copyright file="Assessment.cs" company="Appva AB">
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
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Assessment : Tickable
    {
        /// <summary>
        /// The description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The image path.
        /// </summary>
        public string ImagePath
        {
            get;
            set;
        }
    }
}