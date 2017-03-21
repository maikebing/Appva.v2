using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Signature.Create
{
   


    internal sealed class CreateSignatureHandler : RequestHandler<Identity<CreateSignatureModel>, CreateSignatureModel>
    {
        ITaxonomyService taxonomyService;

        public CreateSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }


        public override CreateSignatureModel Handle(Identity<CreateSignatureModel> message)
        {
            var list = this.taxonomyService.Roots(TaxonomicSchema.SignStatus);

            return new CreateSignatureModel
            {
                Images = list
            };
           

        }
    }
}