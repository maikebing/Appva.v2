// <copyright file = "GeneralSettingsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

using System.Web.Mvc;
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings
{
    using Application.Services.Settings;
    #region Imports.
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Domain.VO;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    [RouteArea("backoffice"), RoutePrefix("generalsettings")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class GeneralSettingsController : Controller
    {
        private readonly ISettingsService settingsService;

        public GeneralSettingsController(ISettingsService settingService)
        {
            this.settingsService = settingService;
        }

        #region List.
        /// <summary>
        /// Lists the settings
        /// </summary>
        /// <returns></returns>

        [Route("list")]
        [Dispatch(typeof(Parameterless<ListGeneralSettingsModel>))]
        public ActionResult List()
        {

            return this.View();
        }
        #endregion

        #region Save.

        
        [Route("list/update")]
        [HttpPost]
        public JsonResult Update(FormCollection request)
        {
            var settings = this.settingsService.List();
            var theSettingValue = "";
            Guid id = new Guid(request["item.Id"]);

            if (request.AllKeys.Contains("item.PdfLookAndFeel"))
            {
                var isCustomLogotype = request["pdf-custFooter"];
                var isCustomFooter = request["pdf-custLogotype"];
                var backgroundColor = request["pdf-bgcolor"];
                var fontColor = request["pdf-fontcolor"];
                var borderColor = request["pdf-bordercolor"];
                var headerColor = request["pdf-headercolor"];



            }

            if (request.AllKeys.Contains("item.SecurityTokenConfig"))
            {

            }

            if (request.AllKeys.Contains("item.SecurityMailerConfig"))
            {
               var mailSigning =    request["secmailconf-signing"];
               var deviceRegMail =  request["secmailconf-deviceRegMail"];
               var regMail =        request["secmailconf-regMail"];
               var resetPassMail =  request["secmailconf-resetPassMail"];
               var eventMail =      request["secmailconf-eventMail"];

                if (mailSigning.Contains("on"))
                {
                    mailSigning = "true";
                }
                if (deviceRegMail.Contains("on"))
                {
                    deviceRegMail = "true";
                }
                if (regMail.Contains("on"))
                {
                    regMail = "true";
                }
                if (resetPassMail.Contains("on"))
                {
                    resetPassMail = "true";
                }
                if (eventMail.Contains("on"))
                {
                    eventMail = "true";
                }

                var securityMailerConfig = SecurityMailerConfiguration.CreateNew(
                        Convert.ToBoolean(eventMail),
                        Convert.ToBoolean(regMail),
                        Convert.ToBoolean(resetPassMail),
                        Convert.ToBoolean(deviceRegMail),
                        Convert.ToBoolean(mailSigning)
                     );

                theSettingValue = JsonConvert.SerializeObject(securityMailerConfig);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
            }


            foreach (var item in request.Keys)
            {
                // select = string, value = int, check = bool
                if (item.ToString().Contains("select") || item.ToString().Contains("value") || item.ToString().Contains("check"))
                {
                    theSettingValue = request[item.ToString()];

                    if (theSettingValue.Contains("on"))
                    {
                        theSettingValue = "true";
                    }

                    var setting = settings.Where(x => x.Id == id).SingleOrDefault();

                    setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
                }
            }

          return  Json("string");
        }
        #endregion

        /*
        #region Edit.
        /// <summary>
        /// edits a setting
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/edit")]
        [HttpGet, Dispatch]
        public ActionResult Edit(Identity<EditGeneralSettingsModel> request)
        {
            return this.View();
        }
        */

        #region Update.

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        

        #endregion
    }
}