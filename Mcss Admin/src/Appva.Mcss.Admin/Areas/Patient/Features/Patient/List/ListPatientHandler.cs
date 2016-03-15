// <copyright file="ListPatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using NHibernate.Type;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using System.IdentityModel.Claims;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Models;

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
        private readonly IPatientService patients;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPatientHandler"/> class.
        /// </summary>
        public ListPatientHandler(
            IPatientTransformer transformer,
            ITaxonFilterSessionHandler filtering,
            IPatientService patients)
        {
            this.transformer = transformer;
            this.filtering = filtering;
            this.patients = patients;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override ListPatientModel Handle(ListPatient message)
        {
            
            var isActive    = message.IsActive ?? true;
            var isDeceased  = message.IsDeceased ?? false;
            var pageSize    = 10;
            var pageIndex   = message.Page ?? 1;

            var result = this.patients.Search(
                new SearchPatientModel
                {
                    TaxonFilter = this.filtering.GetCurrentFilter().Id,
                    IsActive = isActive,
                    IsDeceased = isDeceased,
                    SearchQuery = message.SearchQuery
                },
                pageIndex,
                pageSize);            
           
            return new ListPatientModel
            {
                IsActive       = isActive,
                IsDeceased     = isDeceased,
                Items          = this.transformer.ToPatientList(result.Entities),
                PageNumber     = (int)result.CurrentPage,
                PageSize       = (int)result.PageSize,
                TotalItemCount = (int)result.TotalCount
            };
        }

        #endregion
    }
}