﻿// <copyright file="ListProfileHandler.cs" company="Appva AB">
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

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System.Reflection;
    using Appva.Mcss.Admin.Application.Models;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Models;
    using System;
    using NHibernate.Criterion;

    #endregion

    internal sealed class ListProfileHandler : RequestHandler<ProfileAssessment, ListProfileModel>
    {
        #region Fields.

        private readonly ITaxonomyService taxonomyService;
        private readonly ITaxonFilterSessionHandler filter;
        private readonly IPersistenceContext persistenceContext;
        public static bool? RedirectActive;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProfileHandler"/> class.
        /// </summary>
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

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListProfileModel Handle(ProfileAssessment message)
        {
            var profile = new ListProfileModel();
            var schemes = this.taxonomyService.ListByFilter(TaxonomicSchema.RiskAssessment, message.Active);
            var assessments = new List<ProfileAssessment>();
            var properties = typeof(Taxons).GetFields(BindingFlags.Public | BindingFlags.Static);
            int newItemsCount = 0;

            foreach (var prop in properties)
            {
                var property = (ITaxon)prop.GetValue(null);

                if(schemes.Where(x => x.Type == property.Type).FirstOrDefault() == null)
                {
                    newItemsCount++;
                }
            }

                Taxon seniorAlerts = null;
                Taxon organisation = null;

                foreach (var scheme in schemes)
                {
                    var isActive = this.taxonomyService.Get(scheme.Id).IsActive;
                    int? riskAssesmentsUsed = null;

                    if (isActive)
                    {
                        riskAssesmentsUsed = this.persistenceContext.QueryOver<Patient>()
                            .Where(x => x.Deceased == false)
                            .And(x => x.IsActive)
                            .JoinAlias(x => x.SeniorAlerts, () => seniorAlerts)
                            .Where(() => seniorAlerts.Id == scheme.Id)
                            .JoinAlias(x => x.Taxon, () => organisation)
                            .WhereRestrictionOn(() => organisation.Path).IsLike(filter.GetCurrentFilter().Path, MatchMode.Start)
                            .RowCount();
                    }

                    var assessment = new ProfileAssessment
                    {
                        Id = scheme.Id,
                        Name = scheme.Name,
                        Description = scheme.Description,
                        Type = scheme.Type,
                        Active = isActive,
                        UsedBy = riskAssesmentsUsed
                    };

                    if (message.Active != null && isActive == message.Active)
                    {
                        assessments.Add(assessment);
                    }
                    else if (message.Active == null)
                    {
                        assessments.Add(assessment);
                    }
                }

            string newItems = newItemsCount > 0 ? (newItemsCount == 1 ? "(1 ny)" : "(" + newItemsCount + " nya)") : string.Empty;

            RedirectActive = message.Active;
            profile.IsActive = message.Active;
            profile.NewAssessments = newItems;
            profile.Assessments = assessments;
            return profile;
        }

        #endregion
    }
}