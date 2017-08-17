// <copyright file="CheckDate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CheckDate : Identity<string>
    {
        /// <summary>
        /// The Date to validate
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }
    }
}