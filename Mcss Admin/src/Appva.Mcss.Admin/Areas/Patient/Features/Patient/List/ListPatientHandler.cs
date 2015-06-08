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
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using NHibernate.Criterion;
    using System.Linq;
    using System.Linq.Expressions;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Auditing;

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
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPatientHandler"/> class.
        /// </summary>
        public ListPatientHandler(IAuditService auditing, IPatientTransformer transformer, ITaxonFilterSessionHandler filtering, IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.transformer = transformer;
            this.filtering = filtering;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override ListPatientModel Handle(ListPatient message)
        {
            this.auditing.Read("genomförde en sökning i patientlistan på {0}.", message.SearchQuery);
            var isActive = message.IsActive ?? true;
            var isDeceased = message.IsDeceased ?? false;
            var pageSize = 10;
            var pageIndex = message.Page ?? 1;
            var firstResult = (pageIndex - 1) * pageSize;
            var query = this.persistence.QueryOver<Patient>().Where(x => x.IsActive == isActive);
            if (isActive)
            {
                query.Where(x => x.Deceased == isDeceased);
            }
            if (message.SearchQuery.IsNotEmpty()) {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (message.SearchQuery.First(2).Is(Char.IsNumber)) {
                    expression = x => x.PersonalIdentityNumber;
                }
                query.Where(Restrictions.On<Patient>(expression).IsLike(message.SearchQuery, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName);
            }
            if (this.filtering.HasActiveFilter())
            {
                var taxon = this.filtering.GetCurrentFilter();
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            }
            var items = query.OrderBy(x => x.HasUnattendedTasks).Desc
                .ThenBy(x => x.LastName).Asc
                .Skip(firstResult).Take(pageSize).Future<Patient>().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new ListPatientModel
            {
                IsActive = isActive,
                IsDeceased = isDeceased,
                Search = new SearchViewModel<PatientViewModel>()
                    {
                        Items = this.transformer.ToPatientList(items),
                        PageNumber = pageIndex,
                        PageSize = pageSize,
                        TotalItemCount = totalCount.Value
                    }
            };
        }

        #endregion
    }
}