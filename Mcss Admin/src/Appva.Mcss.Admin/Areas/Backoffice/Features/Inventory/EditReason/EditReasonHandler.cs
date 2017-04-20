// <copyright file="EditReasonHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Admin.Models;
    using Application.Services;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditReasonHandler : RequestHandler<Identity<EditReasonModel>, EditReasonModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditReasonHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        public EditReasonHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <summary>
        /// Handles the EditReasonModel
        /// </summary>
        /// <param name="message">The <see cref="EditReasonModel"/> message</param>
        /// <returns>A new <see cref="EditReasonModel"/></returns>
        public override EditReasonModel Handle(Identity<EditReasonModel> message)
        {
            var taxon = this.taxonomyService.Get(message.Id);

            return new EditReasonModel
            {
                Id = taxon.Id,
                Name = taxon.Name
            };
        }

        #endregion
    }
}