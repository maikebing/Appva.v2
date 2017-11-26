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

        /*
        /// <summary>
        /// Handles the import practitioner request.
        /// </summary>
        /// <param name="request">The <see cref="UploadFileModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("{id:guid}/practitioner")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("practitionerstatus", "import")]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult Practitioner(ImportPractitionerModel request)
        {
            return this.View();
        }
        */

        /// <summary>
        /// Start the import practitioners wizard.
        /// </summary>
        /// <param name="request">The <see cref="Identity{ImportPractitionerModel}"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/preview")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerPreview(Identity<PractitionerPreviewModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the practitioner preview request.
        /// </summary>
        /// <param name="request">The <see cref="Identity{ImportPractitionerModel}"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/preview")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("PractitionerSelection", "Import")]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerPreview(PractitionerPreviewModel request)
        {
            return this.View();
        }

        /// <summary>
        /// Import practitioners, step 1: row selection.
        /// </summary>
        /// <param name="request">The <see cref="PractitionerImportModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/selection")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerSelection(Identity<PractitionerSelectionModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the row selection request.
        /// </summary>
        /// <param name="request">The <see cref="PractitionerSelectionModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/selection")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("PractitionerOrganization", "Import")]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerSelection(PractitionerSelectionModel request)
        {
            return this.View();
        }

        /// <summary>
        /// Import practitioners, step 2: validate organization nodes.
        /// </summary>
        /// <param name="request">The <see cref="PractitionerImportModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/organization")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerOrganization(Identity<PractitionerOrganizationModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Import practitioners, step 3: validate roles.
        /// </summary>
        /// <param name="request">The <see cref="PractitionerImportModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/roles")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerRoles(PractitionerImportModel request)
        {
            return this.View();
        }

        /// <summary>
        /// Import practitioners, step 4: import and save.
        /// </summary>
        /// <param name="request">The <see cref="PractitionerImportModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("practitioners/{id:guid}/import")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ExecuteValue)]
        public ActionResult PractitionerImport(PractitionerImportModel request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}