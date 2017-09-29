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

        #region List Files.

        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListUploadModel>))]
        [PermissionsAttribute(Permissions.FileUpload.ReadValue)]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Upload Files.

        [Route("upload")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<UploadFileModel>))]
        [PermissionsAttribute(Permissions.FileUpload.CreateValue)]
        public ActionResult Upload()
        {
            return this.View();
        }

        [Route("upload")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Upload")]
        [AlertSuccess("En ny fil har sparats!")]
        [PermissionsAttribute(Permissions.FileUpload.CreateValue)]
        public ActionResult Upload(UploadFileModel request)
        {
            return this.View();
        }

        #endregion

        #region Delete Files.

        [Route("delete/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Upload")]
        [AlertSuccess("Filen har tagits bort!")]
        [PermissionsAttribute(Permissions.FileUpload.DeleteValue)]
        public ActionResult Delete(DeleteFile request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}