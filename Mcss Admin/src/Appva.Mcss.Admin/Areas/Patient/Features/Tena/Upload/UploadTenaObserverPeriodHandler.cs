// <copyright file="UploadTenaObserverPeriodHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UploadTenaObserverPeriodHandler : RequestHandler<UploadTenaObserverPeriod, string>
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
        /// Initializes a new instance of the <see cref="CreateTenaObserverPeriodHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public UploadTenaObserverPeriodHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override string Handle(UploadTenaObserverPeriod message)
        {
            //var patientId = message.Id;

            var upload = this.tenaService.PostDataToTena(message.PeriodId);

            //var tenaId = this.patientService.Get(message.Id).TenaId.ToString();
            //var statusCode = this.tenaService.PostDataToTena(tenaId, message.PeriodId).StatusCode;
            //var statusMessage = string.Empty;

            //switch (statusCode)
            //{
            //    case System.Net.HttpStatusCode.Accepted:
            //        statusMessage = "Uppladdning lyckades";
            //        break;
            //    case System.Net.HttpStatusCode.BadRequest:
            //        statusMessage = "Listan är tom eller innehåller fel. Vänligen kontrollera innehållet.";
            //        break;
            //    case System.Net.HttpStatusCode.InternalServerError:
            //        statusMessage = "Ett fel uppstod. Var god försök igen. Om felet kvarstår, vänligen kontakta Appva Support.";
            //        break;
            //    default:
            //        statusMessage = "Ett oväntat fel uppstod. Var god försök igen. Om felet kvarstår, vänligen kontakta Appva Support.";
            //        break;
            //}

            return "OK";
        }

        #endregion
    }
}