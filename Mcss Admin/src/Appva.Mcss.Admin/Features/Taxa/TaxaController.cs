﻿// <copyright file="TaxaController.cs" company="Appva AB">
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
    using System.Web.UI;
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
        [ ChildActionOnly, Route("TaxonFilter")]
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

        [HttpPost, Route("TaxonFilter")]
        public ActionResult TaxonFilter(FormCollection coll)
        {
            return this.TaxonFilter();
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

        #region Json

        /// <summary>
        /// Returns the taxa by parent id.
        /// </summary>
        /// <param name="id">Taxon parent id</param>
        /// <returns>JSON collection of taxa</returns>
        [Route("GetByParent")]
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult GetByParent(Guid id)
        {
            var taxons = this.taxonService.ListByParent(id);
            return this.Json(taxons.Select(x => new
            {
                key = x.Id,
                value = x.Name
            }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns taxa by optional parent id, defaults to root taxon.
        /// </summary>
        /// <param name="id">Optional parent id</param>
        /// <returns>JSON collection of taxa</returns>
        [Route("GetDefaultOrByParent")]
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult GetDefaultOrByParent(Guid? id)
        {
            var guid = (id.HasValue) ? id.Value : this.taxonService.Roots(TaxonomicSchema.Organization).First().Id;
            var taxons = this.taxonService.ListByParent(guid);
            return this.Json(taxons.Select(x => new
            {
                key = x.Id,
                value = x.Name
            }), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Remote Validation

        /// <summary>
        /// Validates a taxon string representation for models (e.g. patient/account).
        /// </summary>
        /// <param name="taxon">The string representation of a taxon guid</param>
        /// <returns>JSON representation of true or false. True if the taxon exist</returns>
        [Route("VerifyTaxon")]
        [HttpPost, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult VerifyTaxon(string taxon)
        {
            var retval = false;
            var guid = Guid.Empty;
            if (Guid.TryParse(taxon, out guid))
            {
                retval = this.taxonService.ListByParent(guid).Count == 0;
            }
            return Json(retval, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Quick validation for taxon validation. Simply tries to parse the string to
        /// a guid.
        /// </summary>
        /// <param name="taxon">The string representation of a taxon guid</param>
        /// <returns>True if a valid guid</returns>
        [Route("VerifyTaxonLazy")]
        [HttpPost, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult VerifyTaxonLazy(string taxon)
        {
            var retval = false;
            var guid = Guid.Empty;
            if (Guid.TryParse(taxon, out guid))
            {
                retval = true;
            }
            return Json(retval, JsonRequestBehavior.DenyGet);
        }

        #endregion
    }


}