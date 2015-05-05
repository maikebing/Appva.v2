// <copyright file="ListPatient.cs" company="Appva AB">
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
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListPatient : IRequest<ListPatientModel>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public string SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// The current page in the set.
        /// </summary>
        public int? Page
        {
            get;
            set;
        }

        /// <summary>
        /// Optional is active query filter - defaults to true.
        /// </summary>
        public bool? IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Optional is deceased query filter - defaults to false
        /// </summary>
        public bool? IsDeceased
        {
            get;
            set;
        }
    }
}