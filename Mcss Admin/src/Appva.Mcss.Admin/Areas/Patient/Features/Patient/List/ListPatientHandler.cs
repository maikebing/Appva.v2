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

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPatientHandler"/> class.
        /// </summary>
        public ListPatientHandler(
            IAuditService auditing, 
            IPatientTransformer transformer, 
            ITaxonFilterSessionHandler filtering,
            ISettingsService settingsService,
            IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.transformer = transformer;
            this.filtering = filtering;
            this.persistence = persistence;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override ListPatientModel Handle(ListPatient message)
        {
            this.auditing.Read("genomförde en sökning i patientlistan på {0}.", message.SearchQuery);
            var isActive    = message.IsActive   ?? true;
            var isDeceased  = message.IsDeceased ?? false;
            var pageSize    = 10;
            var pageIndex   = message.Page ?? 1;
            var firstResult = (pageIndex - 1) * pageSize;
            Patient patient = null;
            var query = this.persistence.QueryOver(() => patient).Where(x => x.IsActive == isActive);
            if (isActive)
            {
                query.Where(x => x.Deceased == isDeceased);
            }
            if (message.SearchQuery.IsNotEmpty()) {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (message.SearchQuery.First(2).Is(Char.IsNumber)) {
                    expression = x => x.PersonalIdentityNumber.Value;
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
            Task taskAlias = null;
            PatientModel model = null;
            query = query.Left.JoinAlias(x => x.Tasks, () => taskAlias, Restrictions.Where(() => taskAlias.IsActive && taskAlias.Delayed && !taskAlias.DelayHandled));
            var items = query.Clone()
                .Select(
                    Projections.ProjectionList()
                        .Add(Projections.Group<Patient>(x => x.Id).WithAlias(() => model.Id))
                        .Add(Projections.Group<Patient>(x => x.IsActive).WithAlias(() => model.IsActive))
                        .Add(Projections.Group<Patient>(x => x.FirstName).WithAlias(() => model.FirstName))
                        .Add(Projections.Group<Patient>(x => x.LastName).WithAlias(() => model.LastName))
                        .Add(Projections.Group<Patient>(x => x.FullName).WithAlias(() => model.FullName))
                        .Add(Projections.Group<Patient>(x => x.PersonalIdentityNumber).WithAlias(() => model.PersonalIdentityNumber))
                        .Add(Projections.Group<Patient>(x => x.Deceased).WithAlias(() => model.IsDeceased))
                        .Add(Projections.Group<Patient>(x => x.Identifier).WithAlias(() => model.Identifier))
                        .Add(Projections.Group<Patient>(x => x.Taxon).WithAlias(() => model.Taxon))
                        .Add(Projections.SqlProjection("substring((SELECT '.' + convert(nvarchar(255),TaxonId) FROM SeniorAlerts Where PatientId = {alias}.Id FOR XML PATH('')), 2, 1000) as SeniorAlerts", new[] { "SeniorAlerts" }, new IType[] { NHibernateUtil.String }).WithAlias(() => model.SeniorAlerts))
                        .Add(Projections.Conditional(Restrictions.Eq(Projections.Group(() => taskAlias.IsActive), true), Projections.Constant(true), Projections.Constant(false)).WithAlias(() => model.HasUnattendedTask)))
                .OrderByAlias(() => model.HasUnattendedTask).Desc
                .ThenByAlias(() => model.LastName).Asc
                .TransformUsing(Transformers.AliasToBean<PatientModel>())
                .Skip(firstResult).Take(pageSize).List<PatientModel>();
            var seniorAlerts = this.settingsService.HasSeniorAlert() ? this.persistence.QueryOver<Taxon>()
                .Where(x => x.IsActive)
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == "SAI")
                    .List() : null;
            return new ListPatientModel
            {
                IsActive       = isActive,
                IsDeceased     = isDeceased,
                Items          = this.transformer.ToPatientList(items, seniorAlerts),
                PageNumber     = pageIndex,
                PageSize       = pageSize,
                TotalItemCount = query.Clone().Select(Projections.CountDistinct("Id")).SingleOrDefault<int>()
            };
        }

        #endregion
    }
}