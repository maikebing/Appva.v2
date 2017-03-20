// <copyright file="EditSignaturePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditSignaturePublisher : RequestHandler<EditSignatureModel, Identity<DetailsScheduleModel>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSigningOptionsPublisher"/> class.
        /// </summary>
        public EditSignaturePublisher(IScheduleService scheduleService, ITaxonomyService taxonomyService)
        {
            this.scheduleService = scheduleService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override Identity<DetailsScheduleModel> Handle(EditSignatureModel message)
        {
            return null;
        }

        #endregion
    }
}