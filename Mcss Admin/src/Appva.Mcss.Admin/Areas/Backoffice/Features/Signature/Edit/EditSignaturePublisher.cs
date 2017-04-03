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
    using Application.Common;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    #endregion

    internal sealed class EditSignaturePublisher : RequestHandler<EditSignatureModel, bool>
    {
        #region Properties.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSignaturePublisher"/> class.
        /// </summary>
        /// <param name="taxonomyService"></param>
        public EditSignaturePublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(EditSignatureModel message)
        {
            var signature = this.taxonomyService.Find(message.Id, TaxonomicSchema.SignStatus);

            if (signature == null)
            {
                return false;
            }

            signature.Update(message.Name, message.Path, message.IsRoot);
            this.taxonomyService.Update(signature, TaxonomicSchema.SignStatus);

            return true;
        }

        #endregion
    }
}