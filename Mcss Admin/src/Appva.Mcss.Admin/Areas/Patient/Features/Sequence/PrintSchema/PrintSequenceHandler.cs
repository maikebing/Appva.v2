// <copyright file="PrintSequenceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Application.Pdf;
    using Application.Services;
    using Appva.Core.Extensions;
    using Cqrs;
    using Tenant.Identity;

    #endregion

    /// <summary>
    /// The print sequence command handler.
    /// </summary>
    internal sealed class PrintSequenceHandler : RequestHandler<PrintSequence, FileContentResult>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPdfPrintService"/>.
        /// </summary>
        private readonly IPdfPrintService pdfService;

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintSequenceHandler"/> class.
        /// </summary>
        /// <param name="pdfService">The <see cref="IPdfPrintService"/></param>
        /// <param name="tenantService">The <see cref="ITenantService"/></param>
        public PrintSequenceHandler(IPdfPrintService pdfService, ITenantService tenantService)
        {
            this.pdfService    = pdfService;
            this.tenantService = tenantService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override FileContentResult Handle(PrintSequence message)
        {
            
            ITenantIdentity tenant;
            this.tenantService.TryIdentifyTenant(out tenant);
            var fileName = string.Format("Signeringslista-{0}-{1}.pdf", tenant.Name.ToUrlFriendly(), DateTime.Now.ToFileTimeUtc());
            var bytes = this.pdfService.CreateBySchedule(fileName, message.StartDate, message.EndDate, message.Id, message.ScheduleId, message.OnNeedBasis, message.StandardSequences);
            return new FileContentResult(bytes, "application/pdf")
            {
                FileDownloadName = fileName
            };
        }

        #endregion
    }
}