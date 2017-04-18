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
    using Core.Extensions;
    using Cryptography;
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
        #region Properties.
        private readonly ISettingsService settingsService;
        #endregion

        #region Constructor.
        public GeneralSettingsController(ISettingsService settingService)
        {
            this.settingsService = settingService;
        }
        #endregion

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

        #region Update.
        [Route("list/update")]
        [HttpPost]
        public void Update(FormCollection request)
        {
            var settings = this.settingsService.List();
            var theSettingValue = "";
            Guid id = new Guid(request["item.Id"]);

            #region JSON PdfLookAndFeel
            if (request.AllKeys.Contains("item.PdfLookAndFeel"))
            {
                var backgroundcolor =   RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-bgcolor"])).Split(',');
                var fontcolor =         RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-fontcolor"])).Split(',');
                var bordercolor =       RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-bordercolor"])).Split(',');
                var headercolor =       RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-headercolor"])).Split(',');

                var pdfLookAndFeel = PdfLookAndFeel.CreateNew(
                    request["item.PdfLookAndFeel.LogotypePath"],
                    request["item.PdfLookAndFeel.FooterText"],
                    PdfColor.CreateNew(Convert.ToByte(backgroundcolor[0]), Convert.ToByte(backgroundcolor[1]), Convert.ToByte(backgroundcolor[2])),
                    PdfColor.CreateNew(Convert.ToByte(fontcolor[0]), Convert.ToByte(fontcolor[1]), Convert.ToByte(fontcolor[2])),
                    PdfColor.CreateNew(Convert.ToByte(headercolor[0]), Convert.ToByte(headercolor[1]), Convert.ToByte(headercolor[2])),
                    PdfColor.CreateNew(Convert.ToByte(bordercolor[0]), Convert.ToByte(bordercolor[1]), Convert.ToByte(bordercolor[2]))
                    );

                    pdfLookAndFeel.IsCustomFooterTextEnabled = toBool(request["pdf-custFooter"]);
                    pdfLookAndFeel.IsCustomLogotypeEnabled = toBool(request["pdf-custLogotype"]);

                theSettingValue = JsonConvert.SerializeObject(pdfLookAndFeel);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
            }
            #endregion

            #region JSON SecurityTokenConfig
            if (request.AllKeys.Contains("item.SecurityTokenConfig"))
            {
                var regLifeTimeDays = TimeSpan.Parse(request["regDays"]);
                TimeSpan regLifetime;
                TimeSpan resetLifetime;
                TimeSpan.TryParse(request["sec-tokenRegLifeTime"], out regLifetime);
                TimeSpan.TryParse(request["sec-tokenResetLifetime"], out resetLifetime);

                if (regLifeTimeDays.Days > 0)
                {
                   regLifetime = regLifeTimeDays + regLifetime;
                }

                var securityTokenConfig = SecurityTokenConfiguration.CreateNew(
                        request["item.SecurityTokenConfig.Issuer"],
                        request["item.SecurityTokenConfig.Audience"],
                        regLifetime,
                        resetLifetime
                 );

                        if (request["newSigningKey"] != null)
                        {
                            securityTokenConfig.SigningKey = Hash.Random().ToBase64();
                        }
                        else
                        {
                            securityTokenConfig.SigningKey = request["item.SecurityTokenConfig.SigningKey"];
                        }

                theSettingValue = JsonConvert.SerializeObject(securityTokenConfig);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
            }
            #endregion

            #region JSON SecurityMailerConfig
            if (request.AllKeys.Contains("item.SecurityMailerConfig"))
            {
                var securityMailerConfig = SecurityMailerConfiguration.CreateNew(
                        toBool(request["secmailconf-eventMail"]),
                        toBool(request["secmailconf-regMail"]),
                        toBool(request["secmailconf-resetPassMail"]),
                        toBool(request["secmailconf-deviceRegMail"]),
                        toBool(request["secmailconf-signing"])
                     );

                theSettingValue = JsonConvert.SerializeObject(securityMailerConfig);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
            }
            #endregion

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
        }

        #region Methods
        /// <summary>
        /// Sets the request value to true if it contains the characters "on".
        /// </summary>
        /// <param name="requestValue"></param>
        /// <returns>true or false</returns>
        private bool toBool(string requestValue)
        {
            if (requestValue.Contains("on"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Converts HEX to RGB
        /// </summary>
        /// <param name="c"></param>
        /// <returns>RGB values</returns>
        private static String RGBConverter(System.Drawing.Color c)
       {
            return c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString();
       }
        #endregion

        #endregion
    }
}