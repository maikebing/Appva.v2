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

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Models;

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

        /// <inheritdoc />
        public override ListProfileModel Handle(ProfileAssessment message)
        {
            var profile = new ListProfileModel();
            var schemes = this.taxonomyService.ListByFilter(TaxonomicSchema.RiskAssessment, message.Active);
            var assessments = new List<ProfileAssessment>();

            if (schemes != null)
            {
                foreach (var scheme in schemes)
                {
                    var isActive = this.taxonomyService.Get(scheme.Id).IsActive;
                    int? usedBy = null;

                    if (isActive)
                    {
                        usedBy = this.persistenceContext.QueryOver<SeniorAlerts>()
                        .Where(x => x.TaxonId == scheme.Id)
                        .List().Count;
                    }

                    var assessment = new ProfileAssessment
                    {
                        Id = scheme.Id,
                        Name = scheme.Name,
                        Description = scheme.Description,
                        Type = scheme.Type,
                        Active = isActive,
                        UsedBy = usedBy
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
            }

            RedirectActive = message.Active;
            profile.IsActive = message.Active;
            profile.Assessments = assessments;
            return profile;
        }
    }
}