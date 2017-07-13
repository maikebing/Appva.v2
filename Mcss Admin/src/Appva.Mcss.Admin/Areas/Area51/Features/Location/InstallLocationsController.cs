// <copyright file="InstallLocationsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Extensions;
    using Appva.Caching.Providers;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Appva.Persistence;
    using Appva.Persistence.MultiTenant;
    using NHibernate;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("locations")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class LocationController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/>.</param>
        public LocationController(IPersistenceContext context, IRuntimeMemoryCache cache)
        {
            this.context = context;
            this.cache   = cache;
        }

        #endregion

        #region Routes.

        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost, Validate, Route("install-locations"), AlertInformation("Installerat adresser")]
        public ActionResult InstallLocations()
        {
            var factories = this.cache.List()
                .Where(x => x.Key.ToString()
                    .StartsWith(CacheTypes.Persistence.FormatWith(string.Empty)))
                .Select(x => x.Value as ISessionFactory)
                .ToList();
            foreach (var factory in factories)
            {
                using (var session     = factory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    this.InstallLocations(session);
                    transaction.Commit();
                }
            }
            return this.RedirectToAction("Index");
        }

        [HttpPost, Validate, Route("install-delegations"), AlertInformation("Installerat delegeringar")]
        public ActionResult InstallDelegations()
        {
            var factories = this.cache.List()
                .Where(x => x.Key.ToString()
                    .StartsWith(CacheTypes.Persistence.FormatWith(string.Empty)))
                .Select(x => x.Value as ISessionFactory)
                .ToList();
            foreach (var factory in factories)
            {
                using (var session = factory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    this.InstallDelegations(session);
                    transaction.Commit();
                }
            }
            return this.RedirectToAction("Index");
        }

        [HttpPost, Validate, Route("install-roles"), AlertInformation("Installerat roller")]
        public ActionResult InstallRoles()
        {
            var factories = this.cache.List()
                .Where(x => x.Key.ToString()
                    .StartsWith(CacheTypes.Persistence.FormatWith(string.Empty)))
                .Select(x => x.Value as ISessionFactory)
                .ToList();
            foreach (var factory in factories)
            {
                using (var session = factory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    this.InstallRoles(session);
                    transaction.Commit();
                }
            }
            return this.RedirectToAction("Index");
        }

        #endregion

        #region Private Members.

        private void InstallLocations(ISession session)
        {
            var accounts = session.QueryOver<Account>().List();
            var root     = session.QueryOver<Taxon>()
                            .Where(x => x.IsRoot)
                              .And(x => x.IsActive)
                            .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                                .Where(x => x.MachineName == TaxonomicSchema.Organization.Id)
                            .Take(1)
                            .SingleOrDefault();
            foreach (var account in accounts)
            {
                session.Save(Location.New(account, root));
            }
        }

        private void InstallDelegations(ISession session)
        {
            var roles = session.QueryOver<Role>()
                .Where(x => x.IsActive)
                  .And(x => x.IsVisible)
                .List();
            var delegations = session.QueryOver<Taxon>()
                            .Where(x => x.IsRoot)
                              .And(x => x.IsActive)
                            .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                                .Where(x => x.MachineName == TaxonomicSchema.Delegation.Id)
                            .List();
            foreach (var role in roles)
            {
                role.Delegations = delegations;
                session.Update(role);
            }
        }

        private void InstallRoles(ISession session)
        {
            var roles1 = session.QueryOver<Role>()
                .Where(x => x.IsActive)
                  .And(x => x.MachineName != "_DA")
                  .And(x => x.MachineName != "_BA")
                .List();
            var roles2 = session.QueryOver<Role>()
                .Where(x => x.IsActive)
                  .And(x => x.IsVisible)
                .List();
            foreach (var role in roles1)
            {
                role.Roles = roles2;
                session.Update(role);
            }
        }

        #endregion
    }
}