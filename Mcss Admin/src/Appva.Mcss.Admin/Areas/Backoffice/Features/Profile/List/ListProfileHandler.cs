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
    using Appva.Mcss.Admin.Areas.Backoffice.Features.Profile.List;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;

    #endregion

    internal sealed class ListProfileHandler : RequestHandler<Parameterless<ListProfileModel>, ListProfileModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProfileHandler"/> class.
        /// </summary>
        /// 
        public ListProfileHandler()
        {

        }

        public ListProfileHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        /// <inheritdoc />
        public override ListProfileModel Handle(Parameterless<ListProfileModel> models)
        {
            var schemes = this.taxonomyService.Roots(TaxonomicSchema.RiskAssessment);
            var assessments = new Dictionary<ITaxon, IList<ITaxon>>();
            foreach (var scheme in schemes)
            {
                assessments.Add(scheme, this.taxonomyService.ListByParent(TaxonomicSchema.RiskAssessment, scheme));
            }

            return new ListProfileModel
            {
                Assessments = assessments
                /*
                Assessments = new List<ProfileAssessment>
                {
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Fall", Description = "Kinfolk tacos aliquip health goth ethical helvetica sed listicle, veniam whatever.", Type = "icn-health-fall.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Glad gubbe", Description = "Butcher sed messenger bag farm-to-table, id sustainable meditation.", Type = "icn-health-oral.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Liggsår", Description = "Paleo et hoodie slow-carb deep v. Freegan irony chia hot chicken kombucha thundercats venmo, meditation waistcoat hella pop-up occupy schlitz culpa.", Type = "icn-health-pressure.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Näring", Description = "Chillwave portland hell of enim. Mollit put a bird on it occaecat dreamcatcher twee, tumblr non cliche.", Type = "icn-health-weight.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Diabetes", Description = "Observera diabetes", Type = "icn-warning-diabetes.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Dubbelbemanning", Description = "Observera dubbelbemaning", Type = "icn-dualstaffing.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Smitta", Description = "Observera smitta", Type = "icn-warning-infection.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Waran", Description = "Observera waran", Type = "icn-warning-waran.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Överkänslighet", Description = "Observera överkänslighet. För mer info, se journal.", Type = "ico-warning-hypersensitive.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "BPSD", Description = "Observera BPSD. För mer info, se journal.", Type = "ico-warning-bpsd.png" },
                    new ProfileAssessment { Id = Guid.NewGuid(), Name = "Blodförtunnande", Description = "Observera blodförtunnande", Type = "ico-warning-antiglukos.png" }
                }
                */
            };
        }
    }
}