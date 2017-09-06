// <copyright file="TaxonomyService.cs" company="Appva AB">
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
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// The taxonomy handler.
    /// </summary>
    public interface ITaxonomyService : IService
    {
        /// <summary>
        /// Finds a taxonomy by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ITaxon Find(Guid id, TaxonomicSchema schema);


        /// <summary>
        /// Finds a taxonomy by id without caching
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ITaxon Find(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        ITaxa Fetch(params Guid[] ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        IList<ITaxon> Roots(TaxonomicSchema schema);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        IList<ITaxon> List(TaxonomicSchema schema);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        IList<ITaxon> ListByFilter(TaxonomicSchema schema, bool? showActive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        IList<ITaxon> ListChildren(TaxonomicSchema schema);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<ITaxon> ListByParent(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<ITaxon> ListByParent(TaxonomicSchema schema, ITaxon parent);

        /// <summary>
        /// Loads a taxon from the id. OBS only creates a proxy, not ful entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Taxon Load(Guid id);

        Taxon Get(Guid id);
        IList<Taxon> ListIn(params Guid[] ids);

        /// <summary>
        /// Saves a Taxon to database
        /// </summary>
        /// <param name="taxon"></param>
        /// <param name="schema"></param>
        void Save(ITaxon taxon, TaxonomicSchema schema);

        /// <summary>
        /// Updates the taxon in database and cache
        /// </summary>
        /// <param name="taxon"></param>
        /// <param name="schema"></param>
        void Update(ITaxon taxon, TaxonomicSchema schema);
    }

    /// <summary>
    /// A <see cref="ITaxonomyService"/> implementation.
    /// </summary>
    public sealed class TaxonomyService : ITaxonomyService
    {
        #region Variables.

        /// <summary>
        /// The implemented <see cref="ITenantAwareMemoryCache"/> instance.
        /// </summary>
        private readonly ITenantAwareMemoryCache cache;

        /// <summary>
        /// The <see cref="ITaxonRepository"/> instance.
        /// </summary>
        private readonly ITaxonRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyService"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="repository">The <see cref="ITaxonRepository"/> instance</param>
        public TaxonomyService(ITenantAwareMemoryCache cache, ITaxonRepository repository)
        {
            this.cache = cache;
            this.repository = repository;
        }

        #endregion

        #region ITaxonomyService Members

        /// <inheritdoc />
        public ITaxon Find(Guid id, TaxonomicSchema schema)
        {
            return this.List(schema)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        /// <inheritdoc />
        public ITaxon Find(Guid id)
        {
            var item = this.repository.Find(id);
            return new TaxonItem(
                    item.Id,
                    item.Name,
                    item.Description,
                    item.Path,
                    item.Type,
                    item.IsRoot,
                    isActive: item.IsActive
                );
        }

        /// <inheritdoc />
        public ITaxa Fetch(params Guid[] ids)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<ITaxon> Roots(TaxonomicSchema schema)
        {
            return this.List(schema).Where(x => x.IsRoot).ToList();
        }

        /// <inheritdoc />
        public IList<ITaxon> List(TaxonomicSchema schema)
        {
            if (schema == null)
            {
                return new Taxa();
            }
            if (this.cache.Find<IList<ITaxon>>(schema.CacheKey) == null)
            {
                CacheUtils.CacheList<ITaxon>(this.cache, schema.CacheKey, this.HierarchyConvert(this.repository.List(schema.Id), null, null));
            }
            return this.cache.Find<IList<ITaxon>>(schema.CacheKey);
        }

        /// <inheritdoc />
        public IList<ITaxon> ListByFilter(TaxonomicSchema schema, bool? showActive = true)
        {
            if (schema == null)
            {
                return null;
            }

            return this.HierarchyConvert(this.repository.ListByFilter(schema.Id, showActive), null, null);
        }

        private IList<ITaxon> HierarchyConvert(IList<Taxon> list, Guid? parentId, ITaxon parent)
        {
            var retval = new List<ITaxon>();
            var items = parentId.HasValue ? 
                list.Where(x => x.Parent != null && x.Parent.Id == parentId ).ToList() :
                list.Where(x => x.Parent == null ).ToList();
            foreach(var item in items)
            {
                var taxon = new TaxonItem(
                        item.Id,
                        item.Name,
                        item.Description,
                        item.Path,
                        item.Type,
                        item.Weight,
                        parent,
                        item.IsActive);
                retval.Add(taxon);
                list.Remove(item);
                retval.AddRange(HierarchyConvert(list, taxon.Id, taxon));
            }
            
            return retval;
        }

        /// <inheritdoc />
        public IList<ITaxon> ListChildren(TaxonomicSchema schema)
        {
            return this.List(schema)
                .Where(x => x.IsRoot == false).ToList();
        }

        /// <inheritdoc />
        public IList<ITaxon> ListByParent(Guid id)
        {
            return this.List(TaxonomicSchema.Organization)
                .Where(x => x.ParentId == id).ToList();
        }

        /// <inheritdoc />
        public IList<ITaxon> ListByParent(TaxonomicSchema schema, ITaxon parent)
        {
            return this.List(schema)
                .Where(x => x.Parent == parent).ToList();
        }

        /// <inheritdoc />
        public Taxon Get(Guid id)
        {
            return this.repository.Get(id);
        }

        /// <inheritdoc />
        public IList<Taxon> ListIn(params Guid[] ids)
        {
            return this.repository.Pick(ids);
        }

        /// <inheritdoc />
        public Taxon Load(Guid id)
        {
            return this.repository.Load(id);
        }

        /// <inheritdoc />
        public void Save(ITaxon taxon, TaxonomicSchema schema)
        {
            this.repository.Save(new Taxon
            {
                Description = taxon.Description,
                IsRoot = taxon.IsRoot,
                Name = taxon.Name,
                Parent = taxon.ParentId.HasValue ? this.repository.Load(taxon.ParentId.Value) : null,
                Path = taxon.Path,
                Taxonomy = this.repository.LoadTaxonomy(schema.Id),
                Type = taxon.Type,
                Weight = taxon.Sort,
                IsActive = taxon.IsActive
            }, (schema == TaxonomicSchema.Delegation || schema == TaxonomicSchema.Organization));

            this.cache.Remove(schema.CacheKey);
        }

        /// <inheritdoc />
        public void Update(ITaxon taxon, TaxonomicSchema schema)
        {
            var t = this.repository.Get(taxon.Id);
            t.Description = taxon.Description;
            t.IsRoot = taxon.IsRoot;
            t.Name = taxon.Name;
            t.Path = taxon.Path;
            t.Type = taxon.Type;
            t.Weight = taxon.Sort;
            t.IsActive = taxon.IsActive;
            
            this.repository.Update(t);
            this.cache.Remove(schema.CacheKey);
        }

        #endregion
    }
}