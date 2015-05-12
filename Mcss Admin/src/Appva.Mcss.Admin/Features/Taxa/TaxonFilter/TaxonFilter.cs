// <copyright file="TaxonFilter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Taxa.Filter
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaxonFilter
    {
        public Guid RootId
        {
            get;
            set;
        }

        public string RootName
        {
            get;
            set;
        }
        
        public Guid? Selected
        {
            get;
            set;
        }

        public IList<TaxonViewModel> Items
        {
            get;
            set;
        }
    }

    public class  TaxonViewModel
    {
        public TaxonViewModel()
        {
            Taxons = new List<SelectListItem>();
        }
        public string Id
        {
            get;
            set;
        }
        public string Selected
        {
            get;
            set;
        }
        public string Label
        {
            get;
            set;
        }
        public string OptionLabel
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> Taxons
        {
            get;
            set;
        }
    }
}