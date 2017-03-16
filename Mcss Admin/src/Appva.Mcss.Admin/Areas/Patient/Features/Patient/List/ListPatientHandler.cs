// <copyright file="ListPatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListPatientHandler : RequestHandler<ListPatient, ListPatientModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPatientHandler"/> class.
        /// </summary>
        /// <param name="transformer">The <see cref="IPatientTransformer"/></param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/></param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        public ListPatientHandler(
            IPatientTransformer transformer,
            ITaxonFilterSessionHandler filtering,
            IPatientService patientService)
        {
            this.transformer    = transformer;
            this.filtering      = filtering;
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override ListPatientModel Handle(ListPatient message)
        {
            var isActive    = message.IsActive   ?? true;
            var isDeceased  = message.IsDeceased ?? false;
            var pageSize    = 10;
            var pageIndex   = message.Page ?? 1;
            var result = this.patientService.Search(
                new SearchPatientModel
                {
                    TaxonFilter = this.filtering.GetCurrentFilter().Path,
                    IsActive    = isActive,
                    IsDeceased  = isDeceased,
                    SearchQuery = message.SearchQuery
                }, 
                pageIndex,
                pageSize);            
            return new ListPatientModel
            {
                IsActive       = isActive,
                IsDeceased     = isDeceased,
                Items          = this.transformer.ToPatientList(result.Entities),
                PageNumber     = (int) result.CurrentPage,
                PageSize       = (int) result.PageSize,
                TotalItemCount = (int) result.TotalCount
            };
        }

        #endregion
    }
}