// <copyright file="ListProfileHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Reflection;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// List profile assessments.
    /// </summary>
    internal sealed class ListProfileHandler : RequestHandler<ProfileAssessment, ListProfileModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>
        /// </summary>
        private readonly ITaxonFilterSessionHandler filter;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProfileHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
        /// <param name="filter">The <see cref="ITaxonFilterSessionHandler"/> implementation</param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/> implementation</param>
        public ListProfileHandler(
            ITaxonomyService taxonomyService,
            ITaxonFilterSessionHandler filter,
            IPersistenceContext persistenceContext)
        {
            this.taxonomyService = taxonomyService;
            this.filter = filter;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Keeps track of the selected filtering option.
        /// </summary>
        public static bool? RedirectActive { get; set; }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListProfileModel Handle(ProfileAssessment message)
        {
            var profile = new ListProfileModel();
            var schemes = this.taxonomyService.ListByFilter(TaxonomicSchema.RiskAssessment, message.IsActive);
            var assessments = new List<ProfileAssessment>();
            var properties = typeof(Taxons).GetFields(BindingFlags.Public | BindingFlags.Static);

            int newItemsCount = this.persistenceContext.QueryOver<Taxon>()
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                .Where(x => x.MachineName == TaxonomicSchema.RiskAssessment.Id)
                .RowCount();

            Taxon seniorAlerts = null;
            Taxon organization = null;

            foreach (var scheme in schemes)
            {
                var isActive = this.taxonomyService.Get(scheme.Id).IsActive;
                int? usedByPatientsCount = null;

                if (isActive)
                {
                    usedByPatientsCount = this.persistenceContext.QueryOver<Patient>()
                        .Where(x => x.Deceased == false)
                        .And(x => x.IsActive)
                        .JoinAlias(x => x.SeniorAlerts, () => seniorAlerts)
                        .Where(() => seniorAlerts.Id == scheme.Id)
                        .JoinAlias(x => x.Taxon, () => organization)
                        .WhereRestrictionOn(() => organization.Path).IsLike(this.filter.GetCurrentFilter().Path, MatchMode.Start)
                        .RowCount();
                }

                var assessment = new ProfileAssessment
                {
                    Id = scheme.Id,
                    Name = scheme.Name,
                    Description = scheme.Description,
                    Type = scheme.Type,
                    IsActive = isActive,
                    UsedByPatientsCount = usedByPatientsCount
                };

                if (message.IsActive == null || message.IsActive == isActive)
                {
                    assessments.Add(assessment);
                }
            }

            newItemsCount = properties.Length - newItemsCount;
            string newItems = newItemsCount > 0 ? (newItemsCount == 1 ? "(1 ny)" : "(" + newItemsCount + " nya)") : string.Empty;

            RedirectActive = message.IsActive;
            profile.IsActive = message.IsActive;
            profile.NewAssessments = newItems;
            profile.Assessments = assessments;
            return profile;
        }

        #endregion
    }
}