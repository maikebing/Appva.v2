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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using System.Collections.Generic;

    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    // using Appva.Mcss.Admin.Areas.Backoffice.Features.Profile.List;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Domain.Models;
    using Infrastructure;
    using System;
    using System.Collections.Generic;

    #endregion

    internal sealed class ListProfileHandler : RequestHandler<ProfileAssessment, ListProfileModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        private readonly ITaxonFilterSessionHandler filter;
        private readonly ITaxonFilterSessionHandler patientService;

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProfileHandler"/> class.
        /// </summary>
        /// 

        public ListProfileHandler(ITaxonomyService taxonomyService, 
                                    ITaxonFilterSessionHandler filter
                                   )
        {
            this.taxonomyService = taxonomyService;
            this.filter = filter;
        }

        #endregion

        /// <inheritdoc />
        public override ListProfileModel Handle(ProfileAssessment message)                       //<ListProfileModel> models)
        {
            var schemes = this.taxonomyService.ListByFilter(TaxonomicSchema.RiskAssessment, message.Active);
            var assessments = new List<ProfileAssessment>();
            

            foreach (var scheme in schemes)
            {
                var assessment = new ProfileAssessment
                {
                    Id = scheme.Id,
                    Name = scheme.Name,
                    Description = scheme.Description,
                    Type = scheme.Type,
                    Active = this.taxonomyService.Get(scheme.Id).IsActive


                };

                
                assessments.Add(assessment);

            }



            return new ListProfileModel
            {
                Assessments = assessments


            };
        }
    }
}