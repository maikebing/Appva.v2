// <copyright file="EditReasonPublisher.cs" company="Appva AB">
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

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal class EditReasonPublisher : RequestHandler<EditReasonModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditReasonPublisher"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        public EditReasonPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides

        /// <summary>
        /// Adds the new taxon to database
        /// </summary>
        /// <param name="message">The <see cref="EditReasonModel"/></param>
        /// <returns>true or false</returns>
        public override bool Handle(EditReasonModel message)
        {
            if (string.IsNullOrEmpty(message.Name) == false)
            {
                var taxon = new TaxonItem(message.Id, message.Name, string.Empty, string.Empty, string.Empty, 0, null);
                this.taxonomyService.Update(taxon, TaxonomicSchema.Withdrawal);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}