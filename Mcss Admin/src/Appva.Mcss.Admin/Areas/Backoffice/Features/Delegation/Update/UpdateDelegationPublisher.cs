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
    internal sealed class UpdateDelegationPublisher : RequestHandler<UpdateDelegationModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDelegationPublisher"/> class.
        /// </summary>
        public UpdateDelegationPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateDelegationModel message)
        {
            var delegation = this.taxonomyService.Find(message.Id, TaxonomicSchema.Delegation);
            if (delegation == null)
            {
                return false;
            }
            delegation.Update(message.Name, message.Description);
            this.taxonomyService.Update(delegation, TaxonomicSchema.Delegation);

            return true;
        }

        #endregion
    }
}