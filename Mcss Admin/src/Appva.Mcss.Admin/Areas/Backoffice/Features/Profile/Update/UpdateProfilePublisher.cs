// <copyright file="UpdateProfilePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Delegation.Update
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

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

        /// <summary>
        /// The <see cref="ITaxonRepository"/>
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProfilePublisher"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
        /// <param name="taxonRepository">The <see cref="ITaxonRepository"/> implementation</param>
        public UpdateProfilePublisher(ITaxonomyService taxonomyService, ITaxonRepository taxonRepository)
        {
            this.taxonomyService = taxonomyService;
            this.taxonRepository = taxonRepository;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateProfileModel message)
        {
            var taxon = TaxonItem.FromTaxon(this.taxonRepository.Get(message.Id));
            taxon.Name = message.Name;
            taxon.Description = message.Description;
            taxon.IsActive = message.IsActive;

            if (taxon == null)
            {
                return false;
            }

            this.taxonomyService.Update(taxon, TaxonomicSchema.RiskAssessment);

            return true;
        }

        #endregion
    }
}