// <copyright file="ImportController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Files.Features.Upload
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("files"), RoutePrefix("import")]
    public sealed class ImportController : Controller
    {
        #region Routes.

        #region Practitioners.

        /// <summary>
        /// Import practitioners.
        /// </summary>
        /// <param name="request">The <see cref="Identity{ImportPractitionerModel}"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("{id:guid}/practitioner")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult Practitioner(Identity<ImportPractitionerModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the import practitioner request.
        /// </summary>
        /// <param name="request">The <see cref="UploadFileModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("{id:guid}/practitioner")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult Practitioner(ImportPractitionerModel request)
        {
            return this.RedirectToAction("List", "Accounts", new { area = "Practitioner" });
        }

        #endregion

        #endregion
    }
}