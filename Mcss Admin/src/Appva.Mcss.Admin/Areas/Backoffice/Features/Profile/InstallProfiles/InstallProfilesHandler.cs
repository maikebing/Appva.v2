using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Areas.Area51.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Repositories;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    public class InstallProfilesHandler : RequestHandler<InstallProfilesModel, TaxonIndex>
    {
        private readonly ITaxonomyService taxonomyService;


        public InstallProfilesHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        public override TaxonIndex Handle(InstallProfilesModel message)
        {
             var installedItems = this.taxonomyService.ListByFilter(TaxonomicSchema.RiskAssessment, null);


            foreach (var prop in typeof(Taxons).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var getTaxon = (ITaxon)prop.GetValue(null);
                var taxon = new TaxonItem(getTaxon.Id, getTaxon.Name, getTaxon.Description, getTaxon.Path, getTaxon.Type, 0, null, false) as ITaxon;

                if (installedItems.Where(x => x.Type == taxon.Type).SingleOrDefault() == null)
                {

                    this.taxonomyService.Save(taxon, TaxonomicSchema.RiskAssessment);


                }
            }

            return new TaxonIndex();
        }
    }
}