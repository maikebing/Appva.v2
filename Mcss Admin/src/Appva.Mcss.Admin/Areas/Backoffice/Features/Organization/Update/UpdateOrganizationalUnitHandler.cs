// <copyright file="UpdateOrganizationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateOrganizationalUnitHandler : RequestHandler<Identity<UpdateOrganizationalUnitModel>, UpdateOrganizationalUnitModel>
    {
        #region Fields

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationalUnitHandler"/> class.
        /// </summary>
        public UpdateOrganizationalUnitHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override UpdateOrganizationalUnitModel Handle(Identity<UpdateOrganizationalUnitModel> message)
        {
            var organizationalUnit = this.taxonomyService.Find(message.Id, TaxonomicSchema.Organization);
            return new UpdateOrganizationalUnitModel
            {
                Id = message.Id,
                Name = organizationalUnit.Name,
                Description = organizationalUnit.Description,
                Weight = organizationalUnit.Sort
            };
        }

        #endregion
    }
}