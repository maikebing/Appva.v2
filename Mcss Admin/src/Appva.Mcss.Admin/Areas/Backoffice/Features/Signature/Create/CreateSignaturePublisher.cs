﻿// <copyright file="CreateSignaturePublisher.cs" company="Appva AB">
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

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using System;

    #endregion

    internal sealed class CreateSignaturePublisher : RequestHandler<CreateSignatureModel, bool>
    {
        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSignaturePublisher"/> class.
        /// </summary>
        public CreateSignaturePublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        public override bool Handle(CreateSignatureModel message)
        {

            var taxon = new TaxonItem(Guid.Empty, message.Name, string.Empty, message.Path, string.Empty, message.IsRoot);

            if (message.Path == null)
            {
                return false;
            }

            this.taxonomyService.Save(taxon, TaxonomicSchema.SignStatus);
            return true;
        }
    }
}