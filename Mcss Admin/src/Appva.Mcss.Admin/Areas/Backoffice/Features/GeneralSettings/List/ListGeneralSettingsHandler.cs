// <copyright file="ListGeneralSettingsHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    internal sealed class ListGeneralSettingsHandler : RequestHandler<Parameterless<ListGeneralSettingsModel>, ListGeneralSettingsModel>
    {
        #region Properties.
        ISettingsService settingService;
        #endregion

        #region Constructor.
        public ListGeneralSettingsHandler(ISettingsService settingService)
        {
            this.settingService = settingService;
        }
        #endregion

        #region RequestHandler Overrides.

        public override ListGeneralSettingsModel Handle(Parameterless<ListGeneralSettingsModel> message)
        {
            var settings = this.settingService.List();
            var setting = new ListGeneralSettingsModel();

            setting.List = settings;
            return setting;
        }
        #endregion
    }
}