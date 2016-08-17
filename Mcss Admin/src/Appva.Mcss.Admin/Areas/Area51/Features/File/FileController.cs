// <copyright file="FilesController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.File
{
    using Admin.Models;
    using Application.Common;
    using GetFile;
    using Infrastructure.Attributes;
    using Infrastructure.Models;
    using List;
    using Mvc.Security;
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("files")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class FileController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        public FileController()
        {
        }

        #endregion

        #region Routes.

        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<IList<FileModel>>))]
        public ActionResult List()
        {
            return this.View();
        }

        [Route("{id:guid}/details")]
        [HttpGet, Dispatch]
        public ActionResult Details(Identity<FileDetailsModel> request)
        {
            return this.View();
        }

        #endregion
    }
}