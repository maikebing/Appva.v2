// <copyright file="QuickSearchPatient.cs" company="Appva AB">
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
using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class QuickSearchPatient
        : IRequest<IEnumerable<object>>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public string Term
        {
            get;
            set;
        }

        /// <summary>
        /// Is active filter.
        /// </summary>
        public bool? IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Is deceased filter.
        /// </summary>
        public bool? IsDeceased
        {
            get;
            set;
        }
    }
}