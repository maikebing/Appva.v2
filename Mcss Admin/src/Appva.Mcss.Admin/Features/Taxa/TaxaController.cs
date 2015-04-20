// <copyright file="TaxaController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Taxa
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Features.Taxa.Filter;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("Taxa")]
    public sealed class TaxaController : Controller
    {
        #region Variables.

        private readonly IIdentityService identity;

        private readonly ITaxonomyService taxonService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxaController"/> class.
        /// </summary>
        public TaxaController(IIdentityService identity, ITaxonomyService taxonService)
        {
            this.identity = identity;
            this.taxonService = taxonService;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Returns a taxon select list for view filtering.
        /// </summary>
        /// <returns>A select list of available taxons for the specific user</returns>
        [HttpGet, ChildActionOnly, Route("TaxonFilter")]
        public PartialViewResult TaxonFilter()
        {
            if (! identity.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            var root = this.taxonService.Roots(TaxonomicSchema.Organization).FirstOrDefault();
            var taxons = this.taxonService.List(TaxonomicSchema.Organization);

            return PartialView(new TaxonFilter
            {
                RootId = root.Id,
                RootName = root.Name,
                Items = SelectList(root, taxons)
            });

            if (identity.Principal.Identity.IsAuthenticated)
            {
                /*
                var taxon = FilterCache.Get(Session);
                if (!FilterCache.HasCache())
                {
                    taxon = FilterCache.GetOrSet(account, Session);
                }
                var taxons = this.taxonService.Roots(TaxonomicSchema.Organization).SingleOrDefault;
                //var taxons = new TaxonomyService(Session).Find(HierarchyUtils.Organization);
                var rootTaxon = taxons.Where(x => x.IsRoot).SingleOrDefault();
                return PartialView(new GlobalFilterViewModel
                {
                    RootName = rootTaxon.Name,
                    RootId = rootTaxon.Id,
                    Items = TaxonomyHelper.SelectList(taxon, taxons)
                });
                */
            }
        }

        #endregion

        public static List<TaxonViewModel> SelectList(ITaxon selected, IList<ITaxon> taxa)
        {
            var retval = new List<TaxonViewModel>();
            var paths = selected.Path.Split('.').Reverse().ToList();
            var selectedXXX = selected.Id.ToString();
            foreach (var value in paths)
            {
                var label = string.Empty;
                var items = new List<SelectListItem>();
                foreach (var taxon in taxa)
                {
                    if (taxon.ParentId.HasValue && taxon.ParentId.ToString().Equals(value))
                    {
                        label = string.IsNullOrEmpty(taxon.Type) ? taxon.Type : string.Empty;
                        items.Add(new SelectListItem
                        {
                            Text = taxon.Name,
                            Value = taxon.Id.ToString()
                        });
                    }
                }
                if (items.Count > 0)
                {
                    retval.Add(new TaxonViewModel
                    {
                        Id = value,
                        Selected = selectedXXX,
                        Label = label,
                        OptionLabel = string.IsNullOrEmpty(label) ? label.ToLower() : string.Empty,
                        Taxons = items
                    });
                }
                selectedXXX = value;
            }
            retval.Reverse();
            return retval;
        }
    }


}