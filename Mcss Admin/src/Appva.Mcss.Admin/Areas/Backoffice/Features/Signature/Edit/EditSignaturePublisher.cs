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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
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
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
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

            var signatureImage = typeof(Signatures).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => x.GetValue(null) as SignatureImage).First(x => x.Image == message.Path);
            if (signatureImage == null)
            {
                throw new ArgumentException("Could not find signature image with path {0} to set correct weight");
            }
            
            signature.Update(new TaxonItem(Guid.Empty, message.Name, string.Empty, signatureImage.Image, signatureImage.CssClass, message.IsRoot, sort: signatureImage.Sort));
            this.taxonomyService.Update(signature, TaxonomicSchema.SignStatus);

            return true;
        }

        #endregion
    }
}