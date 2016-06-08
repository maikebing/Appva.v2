// <copyright file="PrintSchemaScheduleHandler.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Pdf;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintSchemaScheduleHandler : RequestHandler<PrintSchemaSchedule, FileContentResult>
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
        /// Initializes a new instance of the <see cref="PrintSchemaScheduleHandler"/> class.
        /// </summary>
        /// <param name="pdfService">The <see cref="IPdfPrintService"/></param>
        /// <param name="tenantService">The <see cref="ITenantService"/></param>
        public PrintSchemaScheduleHandler(IPdfPrintService pdfService, ITenantService tenantService)
        {
            this.pdfService    = pdfService;
            this.tenantService = tenantService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override FileContentResult Handle(PrintSchemaSchedule message)
        {
            ITenantIdentity tenant;
            this.tenantService.TryIdentifyTenant(out tenant);
            var fileName = string.Format("Signerade-handelser-{0}-{1}.pdf", tenant.Name.ToUrlFriendly(), DateTime.Now.ToFileTimeUtc());
            var bytes    = this.pdfService.CreateByTasks(fileName, message.StartDate, message.EndDate, message.Id, message.ScheduleSettingsId, message.OnNeedBasis, message.StandardSequences);
            return new FileContentResult(bytes, "application/pdf")
            {
                FileDownloadName = fileName
            };
        }

        #endregion
    }
}