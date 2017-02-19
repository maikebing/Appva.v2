// <copyright file="CreateDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Organization.Create
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateOrganizationalUnitPublisher : RequestHandler<CreateOrganizationalUnitModel, bool>
    {
        #region Field

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrganizationalUnitPublisher"/> class.
        /// </summary>
        public CreateOrganizationalUnitPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(CreateOrganizationalUnitModel message)
        {
            var parent = this.taxonomyService.Find(message.ParentId, TaxonomicSchema.Organization);
            if (parent == null)
            {
                return false;
            }

            this.taxonomyService.Save(new TaxonItem(Guid.Empty, message.Name, message.Description, string.Empty, message.Type, message.Weight, parent), TaxonomicSchema.Organization);
            return true;
        }

        #endregion
    }
}