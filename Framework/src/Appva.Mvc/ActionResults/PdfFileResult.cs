// <copyright file="DispatchPdfFileResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

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
        /// Initializes a new instance of the <see cref="DispatchPdfFileResult"/> class.
        /// </summary>
        public PdfFileResult()
            : base("application/pdf")
        {
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

        #endregion

        #region FileResult Overrides.

        /// <inheritdoc />
        protected override void WriteFile(HttpResponseBase response)
        {
            response.OutputStream.Write(FileContents, 0, FileContents.Length);
        }

        /// <inheritdoc />
        public override void ExecuteResult(ControllerContext context)
        {
            var result = context.Controller.ViewData.Model as FileContentResult;
            if (result != null)
            {
                this.FileContents = result.FileContents;
                this.FileDownloadName = result.FileDownloadName;
                result.ExecuteResult(context);
            }
        }

        #endregion
    }
}