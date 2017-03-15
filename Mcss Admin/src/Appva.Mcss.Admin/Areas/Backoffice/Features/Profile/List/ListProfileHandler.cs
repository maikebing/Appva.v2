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
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Features.Profile.List;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System.Collections.Generic;

    #endregion

    internal sealed class ListProfileHandler : RequestHandler<Parameterless<ListProfileModel>, ListProfileModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProfileHandler"/> class.
        /// </summary>
        public ListProfileHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        /// <inheritdoc />
        public override ListProfileModel Handle(Parameterless<ListProfileModel> models)
        {


            return new ListProfileModel
            {
                Assessments = new List<ProfileAssessment>
                {
                    new ProfileAssessment { Id = 1, Name = "Fall", Description = "Kinfolk tacos aliquip health goth ethical helvetica sed listicle, veniam whatever.", Type = "icn-health-fall.png" },
                    new ProfileAssessment { Id = 2, Name = "Glad gubbe", Description = "Butcher sed messenger bag farm-to-table, id sustainable meditation.", Type = "icn-health-oral.png" },
                    new ProfileAssessment { Id = 3, Name = "Liggsår", Description = "Paleo et hoodie slow-carb deep v. Freegan irony chia hot chicken kombucha thundercats venmo, meditation waistcoat hella pop-up occupy schlitz culpa.", Type = "icn-health-pressure.png" },
                    new ProfileAssessment { Id = 3, Name = "Näring", Description = "Chillwave portland hell of enim. Mollit put a bird on it occaecat dreamcatcher twee, tumblr non cliche.", Type = "icn-health-weight.png" },
                    new ProfileAssessment { Id = 3, Name = "Diabetes", Description = "Observera diabetes", Type = "icn-warning-diabetes.png" },
                    new ProfileAssessment { Id = 3, Name = "Dubbelbemanning", Description = "Observera dubbelbemaning", Type = "icn-dualstaffing.png" },
                    new ProfileAssessment { Id = 3, Name = "Smitta", Description = "Observera smitta", Type = "icn-warning-infection.png" },
                    new ProfileAssessment { Id = 3, Name = "Waran", Description = "Observera waran", Type = "icn-warning-waran.png" },
                    new ProfileAssessment { Id = 3, Name = "Överkänslighet", Description = "Observera överkänslighet. För mer info, se journal.", Type = "ico-warning-hypersensitive.png" },
                    new ProfileAssessment { Id = 3, Name = "BPSD", Description = "Observera BPSD. För mer info, se journal.", Type = "ico-warning-bpsd.png" },
                    new ProfileAssessment { Id = 3, Name = "Blodförtunnande", Description = "Observera blodförtunnande", Type = "ico-warning-antiglukos.png" }
                }
            };
        }
    }
}