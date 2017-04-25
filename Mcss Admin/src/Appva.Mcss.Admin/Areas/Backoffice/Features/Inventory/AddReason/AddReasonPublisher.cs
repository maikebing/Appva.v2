// <copyright file="AddReasonPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal class AddReasonPublisher : RequestHandler<AddReasonModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AddReasonPublisher"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        public AddReasonPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        /// <summary>
        /// Adds the new taxon to database
        /// </summary>
        /// <param name="message">The Model</param>
        /// <returns>Bool</returns>
        public override bool Handle(AddReasonModel message)
        {
            if (string.IsNullOrEmpty(message.Name) == false)
            {
                var taxon = new TaxonItem(Guid.Empty, message.Name, string.Empty, string.Empty, string.Empty, 0, null);
                this.taxonomyService.Save(taxon, TaxonomicSchema.Withdrawal);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}