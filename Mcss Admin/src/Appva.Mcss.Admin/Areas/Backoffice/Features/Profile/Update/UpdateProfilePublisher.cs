// <copyright file="UpdateDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Delegation.Update
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateProfilePublisher : RequestHandler<UpdateProfileModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProfilePublisher"/> class.
        /// </summary>
        public UpdateProfilePublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateProfileModel message)
        {
            var profile = this.taxonomyService.Find(message.Id, TaxonomicSchema.RiskAssessment);
            if (profile == null)
            {
                return false;
            }
            profile.Update(message.Name, message.Description);
            this.taxonomyService.Update(profile, TaxonomicSchema.RiskAssessment);

            return true;
        }

        #endregion
    }
}