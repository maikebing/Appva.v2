// <copyright file="UploadController.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("files"), RoutePrefix("upload")]
    public sealed class UploadController : Controller
    {
        #region Routes.

        #region List.

        /// <summary>
        /// Displays a list of uploaded files.
        /// </summary>
        /// <param name="request">The <see cref="ListUploadModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ReadValue)]
        public ActionResult List(ListUploadModel request)
        {
            return this.View();
        }

        #endregion

        #region Download.

        /// <summary>
        /// Gets the download link for a specific file.
        /// </summary>
        /// <param name="request">The <see cref="DownloadFile"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("{id:guid}/download")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.FileUpload.ReadValue)]
        public ActionResult Download(DownloadFile request)
        {
            return this.File();
        }

        #endregion

        #region Upload.

        /// <summary>
        /// Uploads a file.
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("upload")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<UploadFileModel>))]
        [PermissionsAttribute(Permissions.FileUpload.CreateValue)]
        public ActionResult Upload()
        {
            return this.View();
        }

        /// <summary>
        /// Handles the upload file request.
        /// </summary>
        /// <param name="request">The <see cref="UploadFileModel"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("upload")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Upload")]
        [PermissionsAttribute(Permissions.FileUpload.CreateValue)]
        public ActionResult Upload(UploadFileModel request)
        {
            return this.View();
        }

        #endregion

        #region Delete.

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="request">The <see cref="DeleteFile"/> model.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("{id:guid}/delete")]
        [HttpGet, Dispatch("List", "Upload")]
        [PermissionsAttribute(Permissions.FileUpload.DeleteValue)]
        public ActionResult Delete(DeleteFile request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}