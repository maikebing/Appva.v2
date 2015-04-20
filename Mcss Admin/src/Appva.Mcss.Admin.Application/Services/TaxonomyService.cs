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
        /// 
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
        ITaxa ListChildren(TaxonomicSchema schema);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ITaxa ListByParent(Guid id);
    }

    /// <summary>
    /// A <see cref="ITaxonomyService"/> implementation.
    /// </summary>
    public sealed class TaxonomyService : ITaxonomyService
    {
        #region Variables.

        /// <summary>
        /// The implemented <see cref="IRuntimeMemoryCache"/> instance.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

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
        public TaxonomyService(IRuntimeMemoryCache cache, ITaxonRepository repository)
        {
            this.cache = cache;
            this.repository = repository;
        }

        #endregion

        #region ITaxonomyService Members

        /// <inheritdoc />
        public ITaxon Find(Guid id)
        {
            throw new NotImplementedException();
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
            if (this.cache.Find<IList<ITaxon>>(schema.Id) == null)
            {
                CacheUtils.CopyAndCacheList<Taxon, ITaxon>(this.cache, schema.Id, this.repository.List(schema.Id), x =>
                     new TaxonItem(
                        x.Id,
                        x.Name,
                        x.Description,
                        x.Path,
                        x.Type,
                        x.Weight,
                        x.Parent == null ? (Guid?) null : x.Parent.Id));
            }
            return this.cache.Find<IList<ITaxon>>(schema.Id);
        }

        /// <inheritdoc />
        public ITaxa ListChildren(TaxonomicSchema schema)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITaxa ListByParent(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}