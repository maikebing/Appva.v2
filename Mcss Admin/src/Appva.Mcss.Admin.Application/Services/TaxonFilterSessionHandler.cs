// <copyright file="TaxonFilterSessionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Web;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITaxonFilterSessionHandler : IService
    {
        /// <summary>
        /// Returns the current filter. If no filter is set the user account
        /// default taxon will be used.
        /// </summary>
        /// <returns></returns>
        ITaxon GetCurrentFilter();

        /// <summary>
        /// Whether or not there is an active filter for the current user
        /// account.
        /// </summary>
        /// <returns>True if there is an active filter</returns>
        bool HasActiveFilter();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxonId"></param>
        void SetCurrentFilter(Guid taxonId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaxonFilterSessionHandler : ITaxonFilterSessionHandler
    {
        #region Variables.

        /// <summary>
        /// The taxon session key.
        /// </summary>
        private const string SessionKey = "https://schemas.appva.se/session/taxon";

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxaService;

        /// <summary>
        /// The HTTP context.
        /// </summary>
        private readonly HttpContextBase context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonFilterSessionHandler"/> class.
        /// </summary>
        public TaxonFilterSessionHandler(IIdentityService identityService, ITaxonomyService taxaService, HttpContextBase context)
        {
            this.identityService = identityService;
            this.taxaService = taxaService;
            this.context = context;
        }

        #endregion

        #region ITaxonFilterSessionHandler Members.

        /// <inheritdoc />
        public ITaxon GetCurrentFilter()
        {
            if (this.context == null)
            {
                return null;
            }
            var id = this.context.Session[SessionKey];
            if (id == null)
            {
                var claim = this.identityService.Principal.FindFirst(Core.Resources.ClaimTypes.Taxon);
                if (claim == null)
                {
                    return null;
                }
                return this.taxaService.Find(new Guid(claim.Value));
            }
            return this.taxaService.Find((Guid)id, TaxonomicSchema.Organization);
        }

        /// <inheritdoc />
        public bool HasActiveFilter()
        {
            //// Will always have an active filter.
            return true;
            //// var claim = this.identityService.Principal.FindFirst(Core.Resources.ClaimTypes.Taxon);
            //// return this.GetCurrentFilter().Id != new Guid(claim.Value);
        }

        /// <inheritdoc />
        public void SetCurrentFilter(Guid taxonId)
        {
            var taxon = this.taxaService.Find(taxonId);
            if (taxon == null)
            {
                return;
            }
            this.context.Session[SessionKey] = taxon.Id;
        }

        #endregion
    }
}