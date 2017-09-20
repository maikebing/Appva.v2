using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Measurements.List
{
    public class ListMeasurementsHandler : RequestHandler<Parameterless<ListMeasurementsModel>, ListMeasurementsModel>
    {
        private ISettingsService settings;

        public ListMeasurementsHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        public override ListMeasurementsModel Handle(Parameterless<ListMeasurementsModel> message)
        {
            return new ListMeasurementsModel
            {
                Names = new List<string> { "Vikt", "Längd", "P-Glukos", "Blodtryck" }
            };
        }
    }
}