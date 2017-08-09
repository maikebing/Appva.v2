

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Net;
    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// 
    /// </summary>

    internal sealed class ActivateTenaHandler : RequestHandler<ActivateTena, Task<JsonResult>>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateTenaHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public ActivateTenaHandler(IPatientService patientService, ISettingsService settingsService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler Overrides.

        public async override Task<JsonResult> Handle(ActivateTena message)
        {
            var settings = this.settingsService.Find(ApplicationSettings.TenaSettings);
            var credentials = this.tenaService.Base64Encode(settings.ClientId, settings.ClientSecret);

            using (var client = new HttpClient())
            {
                var url = "https://tenaidentifistage.sca.com/api/resident/" + message.ExternalId;
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", credentials);
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=\"" + credentials + "\"");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Basic {0}", credentials));
                var result = await client.GetAsync(url);
                var data = result.Content.ReadAsStringAsync();

                var token = data.Id.ToString();

                var requestData = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                };

                requestData.Headers.TryAddWithoutValidation("Authorization", String.Format("Bearer {0}", token));

                var results = await client.SendAsync(requestData);
                var test = await results.Content.ReadAsStringAsync();
            }

            return null;
        }

        #endregion

    }
}