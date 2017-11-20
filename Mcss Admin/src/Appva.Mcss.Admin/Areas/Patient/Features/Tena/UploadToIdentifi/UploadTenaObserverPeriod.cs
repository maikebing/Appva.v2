// <copyright file="UploadTenaObserverPeriod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UploadTenaObserverPeriod : IAsyncRequest<UploadTenaObserverPeriodModel>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The external tena id.
        /// </summary>
        public Guid PeriodId
        {
            get;
            set;
        }
    }
}