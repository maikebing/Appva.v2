// <copyright file="TaxonController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Transformers;

    #endregion

    /// <summary>
    /// Taxon endpoint.
    /// </summary>
    [RoutePrefix("v1/taxon")]
    public class TaxonController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaxonRepository"/>.
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonController"/> class.
        /// </summary>
        /// <param name="taxonRepository">The <see cref="ITaxonRepository"/></param>
        public TaxonController(ITaxonRepository taxonRepository)
        {
            this.taxonRepository = taxonRepository;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Returns hydrated <code>Taxon</code> by id
        /// </summary>
        /// <param name="id">The taxon id</param>
        /// <returns><code>Taxon</code></returns>
        [AuthorizeToken(Scope.ReadWrite, Scope.ReadOnly)]
        [HttpGet, Route("{id:guid}")]
        public IHttpActionResult GetById(Guid id)
        {
            var taxon = this.taxonRepository.Get(id);
            if (taxon == null)
            {
                return this.NotFound();
            }
            var parents = this.taxonRepository.GetParents(new List<Taxon> { taxon });
            var patients = this.taxonRepository.CountPatients(new List<Taxon> { taxon });
            return this.Ok(TaxonTransformer.ToTaxon(taxon, parents.Contains(taxon.Id), patients.ContainsKey(taxon.Id) ? patients[taxon.Id] : 0 ));
        }

        /// <summary>
        /// Returns a collection of full hydrated <code>Taxon</code>
        /// </summary>
        /// <param name="type_ids">TODO: rename to typeId and use FromUri("type_id")</param>
        /// <param name="count">TODO: count</param>
        /// <param name="cursor">TODO: cursor</param>
        /// <returns>Collection of <code>Taxon</code></returns>
        [AuthorizeToken(Scope.ReadWrite, Scope.ReadOnly)]
        [HttpGet, Route("list")]
        public IHttpActionResult List([FromUri] List<string> type_ids, [FromUri] int count = 200, [FromUri] int cursor = -1)
        {
            var types = type_ids != null ? TaxonTransformer.FromTypeToTaxonomy(type_ids) : null;
            var taxons = this.taxonRepository.Search(string.Empty, null, types, count, cursor);
            var parents = this.taxonRepository.GetParents(taxons);
            var patients = this.taxonRepository.CountPatients(taxons);
            return this.Ok(TaxonTransformer.ToTaxon(taxons, parents, patients));
        }

        /// <summary>
        /// Returns a collection of fully hydrated <code>Taxon</code>
        /// </summary>
        /// <param name="type_ids">TODO: rename to typeIds and use FromUri("type_ids")</param>
        /// <param name="is_root">TODO: rename to isRoot and use FromUri("is_root")</param>
        /// <param name="query">TODO: query</param>
        /// <returns>Collection of <code>Taxon</code></returns>
        [AuthorizeToken(Scope.ReadWrite, Scope.ReadOnly)]
        [HttpGet, Route("search")]
        public IHttpActionResult Search([FromUri] List<string> type_ids, [FromUri] bool? is_root = null, [FromUri] string query = null)
        {
            var types = type_ids != null ? TaxonTransformer.FromTypeToTaxonomy(type_ids) : null;
            var taxons = this.taxonRepository.Search(query, is_root, types);
            var parents = this.taxonRepository.GetParents(taxons);
            var patients = this.taxonRepository.CountPatients(taxons);
            return this.Ok(TaxonTransformer.ToTaxon(taxons, parents, patients));
        }

        /// <summary>
        /// Returns all children of the given <code>Taxon</code>
        /// </summary>
        /// <param name="id">The taxon id</param>
        /// <param name="status_ids">TODO: rename to statusId and use FromUri("status_ids")</param>
        /// <returns>Collection of <code>Taxon</code></returns>
        [AuthorizeToken(Scope.ReadWrite, Scope.ReadOnly)]
        [HttpGet, Route("{id:guid}/children")]
        public IHttpActionResult Children(Guid id, [FromUri] List<string> status_ids)
        {
            var taxons = this.taxonRepository.GetChildren(id);
            var parents = this.taxonRepository.GetParents(taxons);
            var patients = this.taxonRepository.CountPatients(taxons);
            return this.Ok(TaxonTransformer.ToTaxon(taxons, parents, patients));
        }

        /// <summary>
        /// Returns the parent of the given <code>Taxon</code>
        /// </summary>
        /// <param name="id">The taxon id</param>
        /// <returns>Collection of <code>Taxon</code></returns>
        [AuthorizeToken(Scope.ReadWrite, Scope.ReadOnly)]
        [HttpGet, Route("{id:guid}/parent")]
        public IHttpActionResult Parent(Guid id)
        {
            var taxon = this.taxonRepository.Get(id);
            return this.Ok(TaxonTransformer.ToTaxon(taxon.Parent, true));
        }

        #endregion
    }
}
