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
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class GetResidentHandler : AsyncRequestHandler<GetResident, GetResidentModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="GetResidentHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public GetResidentHandler(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public override async Task<GetResidentModel> Handle(GetResident message)
        {
            var response = await this.tenaService.GetResidentAsync(message.ExternalId);

            return new GetResidentModel
            {
                FacilityName = response.FacilityName,
                RoomNumber = response.RoomNumber,
                Error = response.Message,
                TenaId = response.ExternalId
            };
        }

        #endregion

        
    }
}