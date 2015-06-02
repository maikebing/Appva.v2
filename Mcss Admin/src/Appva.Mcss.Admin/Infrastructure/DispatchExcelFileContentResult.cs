// <copyright file="DispatchExcelFileContentResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DispatchExcelFileContentResult : FileResult
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchExcelFileContentResult"/> class.
        /// </summary>
        public DispatchExcelFileContentResult()
            : base("application/vnd.ms-excel")
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// 
        /// </summary>
        public byte[] FileContents
        {
            get;
            private set;
        }

        #endregion

        #region FileResult Overrides.

        /// <inheritdoc />
        protected override void WriteFile(HttpResponseBase response)
        {
            response.OutputStream.Write(FileContents, 0, FileContents.Length);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var model = context.Controller.ViewData.Model;
            if (model is FileContentResult)
            {
                var result = model as FileContentResult;
                this.FileContents = result.FileContents;
                this.FileDownloadName = result.FileDownloadName;
                //var response = context.HttpContext.Response;
                //response.ContentType = this.ContentType;
                //response.OutputStream.Write(FileContents, 0, FileContents.Length);
                new FileContentResult(result.FileContents, this.ContentType)
                {
                    FileDownloadName = result.FileDownloadName
                }.ExecuteResult(context);
            }
        }

        #endregion
    }
}