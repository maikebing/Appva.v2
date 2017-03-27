// <copyright file="EditGeneralSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    internal sealed class EditGeneralSettingsHandler : RequestHandler<Identity<EditGeneralSettingsModel>, EditGeneralSettingsModel>
    {

        #region Properties.
        private readonly ISettingsService settingsService;
        #endregion

        #region Constructor.
        public EditGeneralSettingsHandler(ISettingsService settingsService)
        {
            this.settingsService = settingsService;

        }
        #endregion


        #region RequestHandler Overrides.

        public override EditGeneralSettingsModel Handle(Identity<EditGeneralSettingsModel> message)
        {

            var settings = this.settingsService.List();

            var settingType = this.settingsService.List().Select(x => x.Type).ToList();



            var setting = settings.Where(x => x.Id == message.Id).SingleOrDefault();


            if (setting.Type == typeof(Boolean))
                return new EditGeneralSettingsModel
                {
                    boolValue = Convert.ToBoolean(setting.Value),
                    Name = setting.Name
                };

            if (setting.Type == typeof(String))
                return new EditGeneralSettingsModel
                {
                    stringValue = setting.Value,
                    Name = setting.Name
                };

            if (setting.Type == typeof(Int32))
                return new EditGeneralSettingsModel
                {
                    intValue = Convert.ToInt32(setting.Value),
                    Name = setting.Name
                };

            else
                return new EditGeneralSettingsModel
                {
                    Id = setting.Id,
                    Name = setting.Name

                };

        }
        #endregion
    }
}