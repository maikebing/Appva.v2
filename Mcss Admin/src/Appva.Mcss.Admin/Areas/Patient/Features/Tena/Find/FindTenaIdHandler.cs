// <copyright file="FindTenaIdHandler.cs" company="Appva AB">
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
    using System;
    using Newtonsoft.Json;
    using System.Net;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FindTenaIdHandler : RequestHandler<FindTenaId, string>
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
        /// Initializes a new instance of the <see cref="FindTenaIdHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public FindTenaIdHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override string Handle(FindTenaId message)
        {
            var response = this.tenaService.GetDataFromTena(message.ExternalId);
            var status = string.Empty;

            response.Key.StatusCode = this.tenaService.HasUniqueExternalId(message.ExternalId) == false ? HttpStatusCode.Conflict : response.Key.StatusCode;

            if (response.Key.StatusCode == HttpStatusCode.Conflict)
            {
                status = "Detta Identifi ID används redan.";
            }
            else if(response.Key.StatusCode == HttpStatusCode.NotFound)
            {
                status = "Inga boende hittades med angivet Identifi ID.";
            }
            else if(response.Key.StatusCode == HttpStatusCode.Unauthorized)
            {
                status = "Tjänsten har inte konfigurerats, kontakta Appvas support (kod: " + (int)HttpStatusCode.Unauthorized + ").";
            }
            else if(response.Key.StatusCode == HttpStatusCode.InternalServerError)
            {
                status = "Ett fel har inträffat, försök igen om en stund. Om felet kvarstår, kontakta Appvas support (kod: " + (int)HttpStatusCode.InternalServerError + ").";
            }

            return JsonConvert.SerializeObject(new { Content = response.Value, StatusMessage = status, StatusCode = (int)response.Key.StatusCode });
        }

        #endregion
    }
}