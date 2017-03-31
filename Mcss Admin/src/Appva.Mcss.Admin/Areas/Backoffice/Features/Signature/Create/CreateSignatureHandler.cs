// <copyright file="CreateSignatureHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Signature.Create
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    internal sealed class CreateSignatureHandler : RequestHandler<Identity<CreateSignatureModel>, CreateSignatureModel>
    {
        #region Properties.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private ITaxonomyService taxonomyService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSignatureHandler"/> class.
        /// </summary>
        public CreateSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion
        
        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateSignatureModel Handle(Identity<CreateSignatureModel> message)
        {
            var signatures = typeof(Signatures).GetFields(BindingFlags.Public | BindingFlags.Static);
            var createSignature = new CreateSignatureModel();
            createSignature.Images = new Dictionary<string, string>();
            int btnIndex = 1;

            // Add distinct images to the list.
            foreach (var item in signatures)
            {
                createSignature.Images.Add("radioBtn" + btnIndex, Convert.ToString(item.GetValue(null)));
                btnIndex++;
            }

            return createSignature;
        }

        #endregion
    }
}