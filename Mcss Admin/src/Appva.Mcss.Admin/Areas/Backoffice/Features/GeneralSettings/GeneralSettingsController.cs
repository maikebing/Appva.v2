// <copyright file = "GeneralSettingsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc.Security;
    using Domain.VO;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("generalsettings")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class GeneralSettingsController : Controller
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettingsController"/> class.
        /// </summary>
        /// <param name="settingService">The <see cref="ISettingsService"/> implementations</param>
        public GeneralSettingsController(ISettingsService settingService)
        {
            this.settingsService = settingService;
        }

        #endregion

        #region List.

        /// <summary>
        /// Lists the settings.
        /// </summary>
        /// <returns>ViewResult</returns>
        [Route("list")]
        [Dispatch(typeof(Parameterless<ListGeneralSettingsModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Update.

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="request">The <see cref="FormCollection"/></param>
        /// <returns>JsonResult</returns>
        [Route("list/update")]
        [HttpPost]
        public JsonResult Update(FormCollection request)
        {
            var settings = this.settingsService.List();
            var theSettingValue = string.Empty;
            Guid id = new Guid(request["item.Id"]);

            // JSON object PdfLookAndFeel
            if (request.AllKeys.Contains("item.PdfLookAndFeel"))
            {
                var backgroundcolor =   RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-bgcolor"])).Split(',');
                var fontcolor =         RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-fontcolor"])).Split(',');
                var bordercolor =       RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-bordercolor"])).Split(',');
                var headercolor =       RGBConverter(System.Drawing.ColorTranslator.FromHtml(request["pdf-headercolor"])).Split(',');

                var pdfLookAndFeel = PdfLookAndFeel.CreateNew(
                    request["pdf-logopath"],
                    request["pdf-footertext"],
                    PdfColor.CreateNew(Convert.ToByte(backgroundcolor[0]), Convert.ToByte(backgroundcolor[1]), Convert.ToByte(backgroundcolor[2])),
                    PdfColor.CreateNew(Convert.ToByte(fontcolor[0]), Convert.ToByte(fontcolor[1]), Convert.ToByte(fontcolor[2])),
                    PdfColor.CreateNew(Convert.ToByte(headercolor[0]), Convert.ToByte(headercolor[1]), Convert.ToByte(headercolor[2])),
                    PdfColor.CreateNew(Convert.ToByte(bordercolor[0]), Convert.ToByte(bordercolor[1]), Convert.ToByte(bordercolor[2])));

                pdfLookAndFeel.IsCustomFooterTextEnabled = this.ToBool(request["pdf-custFooter"]);
                pdfLookAndFeel.IsCustomLogotypeEnabled = this.ToBool(request["pdf-custLogotype"]);

                theSettingValue = JsonConvert.SerializeObject(pdfLookAndFeel);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
                return this.Json(null);
            }

            // JSON Object SecurityTokenConfiguration
            if (request.AllKeys.Contains("item.SecurityTokenConfig"))
            {
                SecurityTokenConfiguration securityTokenConfig;
                var regLifeTimeDays = TimeSpan.Parse(request["regDays"]);
                TimeSpan regLifetime;
                TimeSpan resetLifetime;
                TimeSpan.TryParse(request["sec-tokenRegLifeTime"], out regLifetime);
                TimeSpan.TryParse(request["sec-tokenResetLifetime"], out resetLifetime);

                if (regLifeTimeDays.Days > 0)
                {
                   regLifetime = regLifeTimeDays + regLifetime;
                }

                if (request["submitValue"] == "defaultTimes")
                {
                    securityTokenConfig = SecurityTokenConfiguration.CreateNew(
                        request["item.SecurityTokenConfig.Issuer"],
                        request["item.SecurityTokenConfig.Audience"]);
                }
                else
                {
                    securityTokenConfig = SecurityTokenConfiguration.CreateNew(
                        request["item.SecurityTokenConfig.Issuer"],
                        request["item.SecurityTokenConfig.Audience"],
                        regLifetime,
                        resetLifetime);
                }

                if (request["submitValue"] == "update")
                {
                    securityTokenConfig.SigningKey = request["sec-newSigningKeyHidden"];
                }

                theSettingValue = JsonConvert.SerializeObject(securityTokenConfig);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);

                if (request["submitValue"] == "defaultTimes")
                {
                    var times = Convert.ToString(securityTokenConfig.RegistrationTokenLifetime + "," + securityTokenConfig.ResetTokenLifetime);
                    return this.Json(times);
                }
                else
                {
                    return this.Json(securityTokenConfig.SigningKey);
                }
            }

            // JSON Object SecurityMailerConfiguration
            if (request.AllKeys.Contains("item.SecurityMailerConfig"))
            {
                var securityMailerConfig = SecurityMailerConfiguration.CreateNew(
                        this.ToBool(request["secmailconf-eventMail"]),
                        this.ToBool(request["secmailconf-regMail"]),
                        this.ToBool(request["secmailconf-resetPassMail"]),
                        this.ToBool(request["secmailconf-deviceRegMail"]),
                        this.ToBool(request["secmailconf-signing"]));

                theSettingValue = JsonConvert.SerializeObject(securityMailerConfig);

                var setting = settings.Where(x => x.Id == id).SingleOrDefault();
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
                return this.Json(null);
            }

            foreach (var item in request.Keys)
            {
                if (item.ToString().Contains("select") || item.ToString().Contains("value"))
                {
                    theSettingValue = request[item.ToString()];

                    var setting = settings.Where(x => x.Id == id).SingleOrDefault();

                    setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, theSettingValue);
                }

                if (item.ToString().Contains("check"))
                {
                    theSettingValue = request[item.ToString()];

                    var setting = settings.Where(x => x.Id == id).SingleOrDefault();

                    setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(this.ToBool(theSettingValue)));
                }
            }
            return this.Json(null);
        }

        #region Methods

        /// <summary>
        /// Converts HEX to RGB
        /// </summary>
        /// <param name="color">The color</param>
        /// <returns>RGB values</returns>
        private static String RGBConverter(System.Drawing.Color color)
        {
            return color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
        }

        /// <summary>
        /// Sets the request value to true if it contains the string "on". 
        /// A fix because a checkbox value returns null or "on" when using formcollection.
        /// </summary>
        /// <param name="requestValue">The request value</param>
        /// <returns>Bool</returns>
        private bool ToBool(string requestValue)
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

        #endregion

        #endregion
    }
}