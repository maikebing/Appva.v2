using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure.Models;
using Newtonsoft.Json;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    public class UpdateAdministrationPublisher : RequestHandler<UpdateAdministrationModel, Parameterless<ListInventoriesModel>>
    {
        #region Variables.

        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        public UpdateAdministrationPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region Requesthandler overrides.

        public override Parameterless<ListInventoriesModel> Handle(UpdateAdministrationModel message)
        {
            var settings = this.settingsService.Find(ApplicationSettings.AdministrationUnitsWithAmounts);                
            var administration = settings.SingleOrDefault(x => x.Id == message.Id);
            administration.Name = message.Name;
            if (string.IsNullOrWhiteSpace(message.SpecificValues) && message.Max != null)
            {
                var max = message.Max.Value;
                var min = message.Min.HasValue ? message.Min.Value : 0;
                var step = message.Step.HasValue ? message.Step.Value : 1;
                var fractions = message.Fractions.HasValue ? message.Fractions.Value : 0;
                administration.CustomValues.Update(max, min, step, fractions, message.Unit);
            }
            if (!string.IsNullOrWhiteSpace(message.SpecificValues) && message.Max == null)
            {
                administration.CustomValues.Update(JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.SpecificValues.Replace(" ", ""))).ToList());
            }
            this.settingsService.Upsert<List<AdministrationValueModel>>(ApplicationSettings.AdministrationUnitsWithAmounts, settings);
            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}