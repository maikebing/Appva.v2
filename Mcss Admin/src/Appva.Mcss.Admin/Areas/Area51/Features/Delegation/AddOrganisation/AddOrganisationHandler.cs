// <copyright file="AddOrganisationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Caching.Providers;
    using Appva.Core.Resources;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Appva.Mcss.Admin.Areas.Area51.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddOrganisationHandler : RequestHandler<AddOrganisation,bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrganisationHandler"/> class.
        /// </summary>
        public AddOrganisationHandler(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        public override bool Handle(AddOrganisation message)
        {
            var entries = cache.List().Where(x => x.Key.ToString().StartsWith(CacheTypes.Persistence.FormatWith(string.Empty))).ToList();

            foreach (var entry in entries)
            {
                var factory = entry.Value as ISessionFactory;

                using (var context = factory.OpenSession())
                using (var transaction = context.BeginTransaction())
                {
                    var taxon = context.QueryOver<Taxon>()
                        .Where(x => x.IsRoot)
                        .JoinQueryOver(x => x.Taxonomy)
                        .Where(x => x.MachineName == "ORG")
                        .SingleOrDefault();
                    if(taxon != null)
                    {
                         var delegations = context.QueryOver<Delegation>()
                            .Where(x => x.OrganisationTaxon == null)
                            .List();

                        foreach(var d in delegations)
                        {
                            d.OrganisationTaxon = taxon;
                            context.Save(d);
                        }
                        transaction.Commit();
                    }
                }
            }
            return true;
        }
    }
}