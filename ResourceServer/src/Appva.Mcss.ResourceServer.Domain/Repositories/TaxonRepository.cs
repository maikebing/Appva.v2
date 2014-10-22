// <copyright file="TaxonRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// The taxon repository.
    /// </summary>
    public interface ITaxonRepository : IRepository<Taxon>
    {
        /// <summary>
        /// Returns matching taxa.
        /// </summary>
        /// <param name="query">TODO: query</param>
        /// <param name="isRoot">TODO: isRoot</param>
        /// <param name="taxonomy">TODO: taxonomy</param>
        /// <param name="filter">TODO: filter</param>
        /// <param name="count">TODO: count</param>
        /// <param name="cursor">TODO: cursor</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> Search(string query, bool? isRoot, List<string> taxonomy = null, string filter = null, int count = 200, int cursor = -1);

        /// <summary>
        /// Returns children of a <see cref="Taxon"/>.
        /// </summary>
        /// <param name="id">The <see cref="Taxon"/> id</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> GetChildren(Guid id);

        /// <summary>
        /// Returns the taxons in taxon that has a child.
        /// </summary>
        /// <param name="taxons">A collection of <see cref="Taxon"/></param>
        /// <returns>A collection of <see cref="Taxon"/> <see cref="Guid"/></returns>
        IList<Guid> GetParents(IList<Taxon> taxons);

        /// <summary>
        /// Counts patients for all given taxons.
        /// </summary>
        /// <param name="taxons">TODO: taxons</param>
        /// <returns>TODO: returns</returns>
        IDictionary<Guid, int> CountPatients(IList<Taxon> taxons);
    }

    /// <summary>
    /// Implementation of <see cref="ITaxonRepository"/>.
    /// </summary>
    public class TaxonRepository : Repository<Taxon>, ITaxonRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TaxonRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region ITaxonRepository Members.

        /// <inheritdoc />
        public IList<Taxon> Search(string query, bool? isRoot, List<string> taxonomy = null, string filter = null, int count = 200, int cursor = -1)
        {
            var sqlQuery = Where(x => x.Active);
            if (taxonomy != null && taxonomy.Count > 0)
            {
                sqlQuery.JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .WhereRestrictionOn(t => t.MachineName)
                    .IsIn(taxonomy);
            }
            if (filter.IsNotNull())
            {
                sqlQuery.Where(x => x.Path.IsLike(filter, MatchMode.Anywhere));
            }
            if (isRoot.HasValue)
            {
                if (filter.IsNotNull())
                {
                    sqlQuery.Where(x => x.IsRoot == isRoot || x.Id == new Guid(filter));
                }
                else
                {
                    sqlQuery.Where(x => x.IsRoot == isRoot);
                }
            }
            if (query.IsNotEmpty())
            {
                sqlQuery.Where(x => x.Name.IsLike(query, MatchMode.Anywhere) ||
                                    x.Description.IsLike(query, MatchMode.Anywhere));
            }
            return sqlQuery.Skip(cursor).Take(count).List();
        }

        /// <inheritdoc />
        public IList<Taxon> GetChildren(Guid id)
        {
            return Where(x => x.Active)
                .JoinQueryOver<Taxon>(x => x.Parent)
                    .Where(x => x.Id == id)
                .List();
        }

        /// <inheritdoc />
        public IList<Guid> GetParents(IList<Taxon> taxons)
        {
            var taxonArray = taxons.ToArray();
            var query = Where(x => x.Active)
                .AndRestrictionOn(x => x.Parent).IsIn(taxonArray)
                .Select(x => x.Parent.Id);

            return query.List<Guid>();
        }

        /// <inheritdoc />
        public IDictionary<Guid, int> CountPatients(IList<Taxon> taxons)
        {
            var taxonArray = taxons.Select(x => x.Id).ToArray();
            Taxon t = null;
            CountPatientModel pair = new CountPatientModel();
            var query = Alias(() => t)
                .WhereRestrictionOn(x => x.Id).IsIn(taxonArray)
                .SelectList(l => l
                    .Select(x => x.Id).WithAlias(() => pair.Id)
                    .SelectSubQuery(
                        QueryOver.Of<Patient>()
                            .Where(x => x.Taxon.Id == t.Id)
                            .And(x => x.Active)
                            .And(x => !x.Deceased)
                            .ToRowCountQuery())
                    .WithAlias(() => pair.Count))
                .TransformUsing(NHibernate.Transform.Transformers.AliasToBean<CountPatientModel>());

            return query.List<CountPatientModel>().ToDictionary(x => x.Id, x => x.Count);
        }

        #endregion
    }

    /// <summary>
    /// The count patient model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class CountPatientModel
    {
        /// <summary>
        /// The taxon id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The patient count.
        /// </summary>
        public int Count { get; set; }
    }
}