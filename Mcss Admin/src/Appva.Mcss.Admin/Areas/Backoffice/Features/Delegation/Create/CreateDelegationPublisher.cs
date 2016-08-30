// <copyright file="CreateDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Delegation.Create
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateDelegationPublisher : RequestHandler<CreateDelegationModel, bool>
    {
        #region Field.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDelegationPublisher"/> class.
        /// </summary>
        public CreateDelegationPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(CreateDelegationModel message)
        {
            var category = this.taxonomyService.Find(message.CategoryId, TaxonomicSchema.Delegation);
            if(category == null)
            {
                return false;
            }
            this.taxonomyService.Save(new TaxonItem(Guid.Empty, message.Name, message.Description, string.Empty, string.Empty, parent: category), TaxonomicSchema.Delegation);
            return true;
        }

        #endregion
    }
}