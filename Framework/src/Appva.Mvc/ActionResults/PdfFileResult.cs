// <copyright file="PdfFileResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System.Net.Mime;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfFileResult : FileResult
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfFileResult"/> class.
        /// </summary>
        public PdfFileResult(bool inline = true)
            : base("application/pdf")
        {
            this.ContentDisposition = new ContentDisposition
            {
                Inline = inline
            };
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The file content.
        /// </summary>
        public byte[] FileContents
        {
            get;
            private set;
        }

        public ContentDisposition ContentDisposition
        {
            get;
            private set;
        }

        #endregion

        #region FileResult Overrides.

        /// <inheritdoc />
        public override void ExecuteResult(ControllerContext context)
        {
            var result = context.Controller.ViewData.Model as FileContentResult;
            if (result != null)
            {
                this.FileContents     = result.FileContents;
                this.FileDownloadName = result.FileDownloadName;
                ContentDisposition.FileName = this.FileDownloadName;
                //// result.ExecuteResult(context);
                WriteFile(context.HttpContext.Response);
            }
        }

        /// <inheritdoc />
        protected override void WriteFile(HttpResponseBase response)
        {
            response.Clear();
            response.ContentType = ContentType;
            response.AddHeader("Content-Disposition", ContentDisposition.ToString());
            response.OutputStream.Write(this.FileContents, 0, this.FileContents.Length);
        }

        #endregion
    }
}