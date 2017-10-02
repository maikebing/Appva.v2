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
    public interface ITaxonRepository : IRepository<Taxon>, 
        IUpdateRepository<Taxon>,
        ISaveRepository<Taxon>
    {
        /// <summary>
        /// Returns a collection of <see cref="Taxon"/> by <see cref="Taxonomy.Key"/>
        /// </summary>
        /// <param name="taxonomy">The taxonomy identifier</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> List(string taxonomy);

        /// <summary>
        /// Returns a collection of <see cref="Taxon"/> by identifiers.
        /// </summary>
        /// <param name="ids">The taxon ID:s to fetch</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> Pick(params Guid[] ids);

        /// <summary>
        /// Loads an taxonomy by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Taxonomy LoadTaxonomy(string machineName);

        /// <summary>
        /// Saves a taxon and sets the path if nessecary
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="setPath"></param>
        void Save(Taxon entity, bool setPath);
    }

    public sealed class TaxonRepository : Repository<Taxon>, ITaxonRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public TaxonRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region ITaxonRepository Members.

        /// <inheritdoc />
        public IList<Taxon> List(string identifier)
        {
            //// Since we have a flat structure, it's better for post processing to
            //// order by id, then roots will be sorted first, then by sort order.
            return this.Context
                .QueryOver<Taxon>()
                    .Where(x => x.IsActive)
                .OrderBy(x => x.Parent.Id).Asc
                .ThenBy (x => x.Weight).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.IsActive)
                      .And(x => x.MachineName == identifier)
                .List();
        }

        /// <inheritdoc />
        public IList<Taxon> Pick(params Guid[] ids)
        {
            return this.Context
                .QueryOver<Taxon>()
                    .Where(x => x.IsActive)
                      .AndRestrictionOn(x => x.Id)
                    .IsIn(ids)
                .OrderBy(x => x.Weight).Asc
                .List();
        }

        /// <inheritdoc />
        public Taxonomy LoadTaxonomy(string machineName)
        {
            return this.Context
                .QueryOver<Taxonomy>()
                    .Where(x => x.MachineName == machineName)
                .SingleOrDefault();
        }

        #endregion
        

        /// <inheritdoc />
        public void Save(Taxon entity)
        {
            this.Save(entity, false);
        }

        /// <inheritdoc />
        public void Save(Taxon entity, bool setPath)
        {
            this.Context.Save<Taxon>(entity);
            if (setPath)
            {
                entity.Path = entity.Parent != null ? string.Format("{0}.{1}", entity.Parent.Path, entity.Id) : entity.Id.ToString();
                this.Context.Update<Taxon>(entity);
            }
        }

        /// <inheritdoc />
        public void UpdateMeasurementObservation(Taxon entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.Context.Update<Taxon>(entity);
        }

    }
}