

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
    internal sealed class CheckDateHandler : RequestHandler<CheckDate, string>
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
        public CheckDateHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override string Handle(CheckDate message)
        {
            string json_result = "ok";
            string json_message = "";
            if(this.tenaService.HasConflictingDate(message.Id, message.Date))
            {
                json_result = "fail";
                json_message = "Det här datumet överlappar tidigare perioder";
            }
            return JsonConvert.SerializeObject(new { Content = json_result, Message = json_message});
        }

        #endregion
    }
}