// <copyright file="DetailsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.File.Details
{
    using Domain.Entities;
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DetailsModel
    {
        public string FileName
        {
            get;
            set;
        }

        public IList<DetailsItemModel> Accounts
        {
            get;
            set;
        }

        /// <summary>
        /// The address taxons.
        /// </summary>
        public IEnumerable<TaxonViewModel> Taxons
        {
            get;
            set;
        }

        /// <summary>
        /// A set of roles/titles to choose from.
        /// </summary>
        public IEnumerable<SelectListItem> Titles
        {
            get;
            set;
        }

        public void Validate()
        {

        }
    }

    public enum Status
    {
        OK,
        SSNError,
        EmailError
    }
}