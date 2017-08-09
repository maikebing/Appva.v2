

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

    internal sealed class ActivateTenaHandler : RequestHandler<ActivateTena, JsonResult>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

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
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public ActivateTenaHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler Overrides.

        public override JsonResult Handle(ActivateTena message)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", this.tenaService.GetCredentials());
                HttpResponseMessage response = Task.Run(() => client.GetAsync(this.tenaService.GetRequestUri())).Result;
                var token = string.Empty;

                if (response.Headers.Contains("Token"))
                {
                    token = response.Headers.GetValues("Token").First();
                }

                return null;
            }
        }

        #endregion

    }
}