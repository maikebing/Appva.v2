// <copyright file="CreateSignaturePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using System.Reflection;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSignaturePublisher : RequestHandler<CreateSignatureModel, bool>
    {
        #region Fields

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSignaturePublisher"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
        public CreateSignaturePublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides

        /// <summary>
        /// Creates a new signing
        /// </summary>
        /// <param name="message">Passes the CreateSignatureModel</param>
        /// <returns>true or false</returns>
        public override bool Handle(CreateSignatureModel message)
        {
            var signature = typeof(Signatures).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => x.GetValue(null) as SignatureImage).First(x => x.Image == message.Path);
            if (signature == null)
            {
                throw new ArgumentException("Could not find signature image with path {0} to set correct weight");
            }

            var taxon = new TaxonItem(Guid.Empty, message.Name, string.Empty, signature.Image, signature.CssClass, message.IsRoot, sort: signature.Sort);

            this.taxonomyService.Save(taxon, TaxonomicSchema.SignStatus);
            return true;
        }

        #endregion
    }
}