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
    using System.Web.UI;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("Taxa")]
    public sealed class TaxaController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler taxonFilterSessionHandler;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxaController"/> class.
        /// </summary>
        /// <param name="taxonFilterSessionHandler">The session filter handler.</param>
        /// <param name="identityService">The identity service.</param>
        /// <param name="taxonService">The taxon service.</param>
        public TaxaController(
            ITaxonFilterSessionHandler taxonFilterSessionHandler, 
            IIdentityService identityService, 
            ITaxonomyService taxonService,
            IAccountService  accountService)
        {
            this.taxonFilterSessionHandler = taxonFilterSessionHandler;
            this.identityService           = identityService;
            this.taxonService              = taxonService;
            this.accountService            = accountService;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Returns a taxon select list for view filtering.
        /// </summary>
        /// <returns>A select list of available taxons for the specific user</returns>
        [ChildActionOnly, Route("TaxonFilter")]
        [PermissionsAttribute(Permissions.Admin.LoginValue)]
        public PartialViewResult TaxonFilter()
        {
            if (! identityService.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            var id       = this.identityService.PrincipalId;
            var account  = this.accountService.Find(id);
            //account.Locations
            var selected = this.taxonFilterSessionHandler.GetCurrentFilter();
            var root     = this.taxonService.Roots(TaxonomicSchema.Organization).First();
            var taxons   = this.taxonService.List (TaxonomicSchema.Organization);
            return PartialView(
                TaxonomyHelper.CreateItems(account, selected, taxons) //SelectList((selected == null) ? root : selected, taxons)
            );
        }

        [HttpPost, Route("TaxonFilter")]
        public ActionResult TaxonFilter(FormCollection collection)
        {
            var guids = TaxonomyHelper.GetGuid(collection);
            if (guids.Count > 0)
            {
                this.taxonFilterSessionHandler.SetCurrentFilter(guids.First());
            }
            if (collection.Get("global-filter") != null)
            {
                return this.Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return this.TaxonFilter();
        }

        #endregion

        private IList<ITaxon> GetTaxonsFromOrganization(Domain.Entities.Account account, IList<ITaxon> organization)
        {
            var result = new List<ITaxon>();
            foreach (var location in account.Locations.OrderByDescending(x => x.Sort))
            {
                var taxon = organization.Where(x => x.Id == location.Id).SingleOrDefault();
                if (taxon == null)
                {
                    continue;
                }
                result.Add(taxon);
            }
            return result;
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
                key   = x.Id,
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
            var id = Guid.Empty;
            if (! Guid.TryParse(taxon, out id))
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
            var retval = this.taxonService.Find(id, TaxonomicSchema.Organization) != null;
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
            var id = Guid.Empty;
            return Json(Guid.TryParse(taxon, out id), JsonRequestBehavior.DenyGet);
        }

        #endregion
    }
}