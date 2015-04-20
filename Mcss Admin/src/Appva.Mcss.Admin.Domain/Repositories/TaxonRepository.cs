// <copyright file="ITaxonRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// The <see cref="Taxon"/> repository.
    /// </summary>
    public interface ITaxonRepository : IIdentityRepository<Taxon>, IRepository
    {
        /// <summary>
        /// Returns a collection of <see cref="Taxon"/> by <see cref="Taxonomy.Key"/>
        /// </summary>
        /// <param name="identifier">The taxonomy identifier</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> List(string identifier);
    }

    public sealed class TaxonRepository : ITaxonRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> instance.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TaxonRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IIdentityRepository<Taxon> Members.

        /// <inheritdoc />
        public Taxon Find(Guid id)
        {
            return this.persistenceContext.Get<Taxon>(id);
        }

        #endregion

        #region ITaxonRepository Members.

        /// <inheritdoc />
        public IList<Taxon> List(string identifier)
        {
            //// Since we have a flat structure, it's better for post processing to
            //// order by id, then roots will be sorted first, then by sort order.
            return this.persistenceContext.QueryOver<Taxon>()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Parent.Id).Asc
                .ThenBy(x => x.Weight).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                .Where(x => x.IsActive)
                .And(x => x.MachineName == identifier)
                .List();
        }

        #endregion

        
    }
}