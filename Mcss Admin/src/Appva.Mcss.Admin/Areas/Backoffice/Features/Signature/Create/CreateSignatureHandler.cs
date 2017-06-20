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

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
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
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
        public CreateSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion
        
        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateSignatureModel Handle(Identity<CreateSignatureModel> message)
        {
            var signatures = typeof(Signatures).GetFields(BindingFlags.Public | BindingFlags.Static)
                .OrderBy(x => ((SignatureImage)x.GetValue(null)).Sort)
                .Select(x => ((SignatureImage)x.GetValue(null)).Image)
                .ToDictionary<string, string>(x => x.Split('.').First());

            return new CreateSignatureModel
            {
                Images = signatures
            };
        }

        #endregion
    }
}