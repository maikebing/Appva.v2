// <copyright file="FilterCache.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Web.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Web.Mappers;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// A filter cache helper.
    /// </summary>
    public class FilterCache
    {
        /// <summary>
        /// The taxon session cache key.
        /// </summary>
        const string TaxonCacheKey = "Taxon.Default.Cache";

        /// <summary>
        /// Returns a <see cref="Taxon"/> from the session.
        /// </summary>
        /// <param name="session">Nhibernate session</param>
        /// <returns>A <see cref="Taxon"/> if set</returns>
        public static Taxon Get(IPersistenceContext session)
        {
            if (HasCache())
            {
                return session.Get<Taxon>(HttpContext.Current.Session[TaxonCacheKey]);
            }
            return null;
        }

        /// <summary>
        /// Returns the taxon and sets it if it doesn't exist.
        /// </summary>
        /// <param name="account">The current authenticated account</param>
        /// <param name="session">The nhibernate session</param>
        /// <returns>Returns a <see cref="Taxon"/></returns>
        public static Taxon GetOrSet(Account account, IPersistenceContext session)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session[TaxonCacheKey].IsNull())
                {
                    var taxonomyService = new TaxonomyService(PatientMapper.RTC, new TaxonRepository(session));
                    var taxons = taxonomyService.List(TaxonomicSchema.Organization);
                    var accountHasTaxon = (account.Taxon.IsNotNull() && account.Taxon.Path.IsNotEmpty());
                    HttpContext.Current.Session[TaxonCacheKey] = (accountHasTaxon) ? account.Taxon.Id : taxonomyService.Roots(TaxonomicSchema.Organization).First().Id;
                }
            }
            return session.Get<Taxon>(HttpContext.Current.Session[TaxonCacheKey]);
        }

        /// <summary>
        /// Caches the taxon in the session.
        /// </summary>
        /// <param name="taxon">The <see cref="Taxon"/> to be cached</param>
        public static void Cache(Taxon taxon)
        {
            HttpContext.Current.Session[TaxonCacheKey] = taxon != null ? (Guid?)taxon.Id : null;
        }

        /// <summary>
        /// Returns whether or not the taxon is stored in the session.
        /// </summary>
        /// <returns>Returns true if the taxon id is in the session cache</returns>
        public static bool HasCache()
        {
            return HttpContext.Current.Session[TaxonCacheKey].IsNotNull();
        }
    }
}