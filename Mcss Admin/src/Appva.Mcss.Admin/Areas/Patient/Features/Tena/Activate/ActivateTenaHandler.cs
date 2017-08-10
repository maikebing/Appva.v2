// <copyright file="ActivateTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Linq;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ActivateTenaHandler : RequestHandler<ActivateTena, string>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateTenaHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public ActivateTenaHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override string Handle(ActivateTena message)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", this.tenaService.GetCredentials());
                response = Task.Run(() => client.GetAsync(this.tenaService.GetRequestUri() + message.ExternalId)).Result;
                //string token = GetTokenValue(response);
                string[] dataList = new string[2] { response.Content.ReadAsStringAsync().Result, response.StatusCode.ToString() };
                return JsonConvert.SerializeObject(dataList);
            }
        }

        private string GetTokenValue(HttpResponseMessage message)
        {
            string token; 

            if (message.Headers.Contains("Token"))
            {
                token = message.Headers.GetValues("Token").First();
            }
            else
            {
                token = null;
            }

            return token;
        }

        #endregion
    }
}