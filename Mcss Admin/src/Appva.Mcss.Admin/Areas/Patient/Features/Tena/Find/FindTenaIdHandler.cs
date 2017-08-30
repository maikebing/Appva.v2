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

    using System.Net;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Newtonsoft.Json;

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

            //response.StatusCode = this.tenaService.HasUniqueExternalId(message.ExternalId) == false ? HttpStatusCode.Conflict : response.StatusCode;

            //switch (response.StatusCode)
            //{
            //    case HttpStatusCode.BadRequest:
            //        status = "Inga boende hittades med angivet Identifi ID. (Felkod: " + (int)response.StatusCode + ").";
            //        break;
            //    case HttpStatusCode.Unauthorized:
            //        status = "Tjänsten är ej installerad korrekt. (Felkod: " + (int)response.StatusCode + ").";
            //        break;
            //    case HttpStatusCode.NotFound:
            //        status = "Inga boende hittades med angivet Identifi ID. (Felkod: " + (int)response.StatusCode + ").";
            //        break;
            //    case HttpStatusCode.Conflict:
            //        status = "Detta Identifi ID är redan i bruk.. (Felkod: " + (int)response.StatusCode + ").";
            //        break;
            //    case HttpStatusCode.InternalServerError:
            //        status = "Ett fel har inträffat, försök igen om en stund. Om felet kvarstår, kontakta Appvas support (Felkod: " + (int)response.StatusCode + ").";
            //        break;
            //    default:
            //        status = string.Empty;
            //        break;
            //}

            return JsonConvert.SerializeObject(new FindTenaIdModel {
                TenaId = message.ExternalId,
                RoomNumber = response.RoomNumber,
                FacilityName = response.FacilityName,
                StatusCode = 800, // (int)response.StatusCode
                StatusMessage = status
            });
        }

        #endregion
    }
}