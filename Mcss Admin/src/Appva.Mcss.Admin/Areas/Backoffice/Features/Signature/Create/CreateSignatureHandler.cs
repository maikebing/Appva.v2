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
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal sealed class CreateSignatureHandler : RequestHandler<Identity<CreateSignatureModel>, CreateSignatureModel>
    {
        private ITaxonomyService taxonomyService;

        public CreateSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        public override CreateSignatureModel Handle(Identity<CreateSignatureModel> message)
        {
            var list = this.taxonomyService.Roots(TaxonomicSchema.SignStatus);
            var createSignature = new CreateSignatureModel();
            createSignature.Images = new Dictionary<string, string>();
            int btnIndex = 1;

            foreach (var item in list.GroupBy(x => x.Path).Select(x => x.FirstOrDefault()))
            {
                createSignature.Images.Add("radioBtn" + btnIndex, item.Path);

                btnIndex++;
            }

            return createSignature;
        }
    }
}