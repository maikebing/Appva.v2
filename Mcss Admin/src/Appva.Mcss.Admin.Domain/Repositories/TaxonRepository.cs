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
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;

    #endregion

    /// <summary>
    /// The <see cref="Taxon"/> repository.
    /// </summary>
    public interface ITaxonRepository : 
        IIdentityRepository<Taxon>, 
        IUpdateRepository<Taxon>,
        ISaveRepository<Taxon>, 
        IProxyRepository<Taxon>, 
        IRepository
    {
        /// <summary>
        /// Returns a collection of <see cref="Taxon"/> by <see cref="Taxonomy.Key"/>
        /// </summary>
        /// <param name="taxonomy">The taxonomy identifier</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> List(string taxonomy);

        /// <summary>
        /// Returns a collection of <see cref="Taxon"/> by <see cref="Taxonomy.Key"/>
        /// </summary>
        /// <param name="taxonomy">The taxonomy identifier</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        IList<Taxon> ListByFilter(string taxonomy, bool? showActive = true);

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

        /// <inheritdoc />
        public IList<Taxon> ListByFilter(string identifier, bool? showActive = true)
        {
            //// Since we have a flat structure, it's better for post processing to
            //// order by id, then roots will be sorted first, then by sort order.
            NHibernate.IQueryOver<Taxon, Taxon> context = this.persistenceContext.QueryOver<Taxon>();

            if (showActive == true)
            {
                context.Where(x => x.IsActive);
            }
            else if (showActive == false)
            {
                // Show inactive results.
                context.Where(x => !x.IsActive);
            }

            var retval = context.OrderBy(x => x.Parent.Id).Asc
                .ThenBy(x => x.Weight).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.IsActive)
                    .And(x => x.MachineName == identifier)
                .List();

            return retval;
        }

        /// <inheritdoc />
        public IList<Taxon> Pick(params Guid[] ids)
        {
            return this.persistenceContext.QueryOver<Taxon>()
                .Where(x => x.IsActive)
                .AndRestrictionOn(x => x.Id)
                .IsIn(ids)
                .OrderBy(x => x.Weight).Asc
                .List();
        }

        /// <inheritdoc />
        public Taxonomy LoadTaxonomy(string machineName)
        {
            return this.persistenceContext.QueryOver<Taxonomy>()
                .Where(x => x.MachineName == machineName)
                .SingleOrDefault();
        }

        #endregion

        
        #region IProxyRepository<Taxon> Members.

        /// <inheritdoc />
        public Taxon Load(Guid id)
        {
            return this.persistenceContext.Session.Load<Taxon>(id);
        }

        #endregion

        
        #region ISaveRepository<Taxon> Members.

        /// <inheritdoc />
        public void Save(Taxon entity)
        {
            this.Save(entity, false);
        }

        /// <inheritdoc />
        public void Save(Taxon entity, bool setPath)
        {
            this.persistenceContext.Save<Taxon>(entity);
            if (setPath)
            {
                entity.Path = entity.Parent != null ? string.Format("{0}.{1}", entity.Parent.Path, entity.Id) : entity.Id.ToString();
                this.persistenceContext.Update<Taxon>(entity);
            }
        }

        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        public void Update(Taxon entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.persistenceContext.Update<Taxon>(entity);
        }

        #endregion
    }
}