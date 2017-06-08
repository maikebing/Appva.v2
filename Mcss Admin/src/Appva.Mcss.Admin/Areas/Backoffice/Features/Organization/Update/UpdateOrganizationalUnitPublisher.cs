// <copyright file="UpdateOrganizationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Organization.Update
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateOrganizationalUnitPublisher : RequestHandler<UpdateOrganizationalUnitModel, bool>
    {
        #region Fields

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationalUnitPublisher"/> class.
        /// </summary>
        public UpdateOrganizationalUnitPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(UpdateOrganizationalUnitModel message)
        {
            var organizationalUnit = this.taxonomyService.Find(message.Id, TaxonomicSchema.Organization);
            if (organizationalUnit == null)
            {
                return false;
            }
            organizationalUnit.Update(message.Name, message.Description, message.Weight, null);
            this.taxonomyService.Update(organizationalUnit, TaxonomicSchema.Organization);

            return true;
        }

        #endregion
    }
}