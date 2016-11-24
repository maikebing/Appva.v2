// <copyright file="DeletePreparationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Organization.Delete
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteOrganizationalUnitPublisher : RequestHandler<DeleteOrganizationalUnitModel, bool>
    {
        #region Fields

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteOrganizationalUnitPublisher"/> class.
        /// </summary>
        public DeleteOrganizationalUnitPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }


        #endregion
    
        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(DeleteOrganizationalUnitModel message)
        {            
            // this.taxonomyService.Delete(message.Id, TaxonomicSchema.Organization);
            return true;
        }

        #endregion
    }
}