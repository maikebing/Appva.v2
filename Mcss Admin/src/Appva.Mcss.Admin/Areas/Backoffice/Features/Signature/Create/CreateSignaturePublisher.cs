using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Areas.Backoffice.Features.Signature.Create;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    internal sealed class CreateSignaturePublisher : RequestHandler<CreateSignatureModel, bool>
    {
        private readonly ITaxonomyService taxonomyService;

        public CreateSignaturePublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        public override bool Handle(CreateSignatureModel message)
        {



            var taxon = new TaxonItem(Guid.Empty, message.Name, string.Empty, message.Path, string.Empty, 0, parent: null);

            //if (message.IsRoot == true)
            //{

                //var tax = new Taxon
                //{
                //    Parent = null,
                //    Path = message.Path,
                //    Name = message.Name,
                //    IsRoot = true

                //};
            //}





            this.taxonomyService.Save(taxon, TaxonomicSchema.SignStatus);

            return true;

        }
    }
}