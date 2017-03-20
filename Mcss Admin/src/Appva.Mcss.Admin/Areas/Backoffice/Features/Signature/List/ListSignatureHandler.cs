
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Models;
    using Infrastructure.Models;
    using System;
    using Admin.Models;

    #endregion

    internal sealed class ListSignatureHandler : RequestHandler<Parameterless<ListSignatureModel>, ListSignatureModel>
    {
        private ITaxonomyService taxonomyService;

        public ListSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }


        public override ListSignatureModel Handle(Parameterless<ListSignatureModel> message)
        {

            var signatures = this.taxonomyService.List(TaxonomicSchema.SignStatus);


            return new ListSignatureModel
            {
                Options = signatures
            };

        }

       


    }
}