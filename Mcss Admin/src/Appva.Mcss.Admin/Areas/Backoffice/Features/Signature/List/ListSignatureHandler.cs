// <copyright file="ListSignatureHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Infrastructure.Models;

    #endregion

    internal sealed class ListSignatureHandler : RequestHandler<Parameterless<ListSignatureModel>, ListSignatureModel>
    {
        #region Properties.

        private ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        public ListSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandlers Overrides.

        public override ListSignatureModel Handle(Parameterless<ListSignatureModel> message)
        {
            var signatureList = this.taxonomyService.List(TaxonomicSchema.SignStatus);

            return new ListSignatureModel
            {
                Options = signatureList
            };
        }

        #endregion
    }
}