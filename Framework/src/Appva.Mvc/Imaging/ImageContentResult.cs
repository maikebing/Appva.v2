// <copyright file="ImageContentResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Imaging
{
    #region Imports.

    using System;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ImageContentResult : FileContentResult
    {
        #region Variables.

        /// <summary>
        /// Cache expiration in minutes.
        /// </summary>
        private readonly int expires = -1;

        /// <summary>
        /// Last updated date time.
        /// </summary>
        private readonly DateTime? lastModified;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageContentResult"/> class.
        /// </summary>
        /// <param name="fileContents">The byte array to send to the response</param>
        /// <param name="contentType">The content type to use for the response</param>
        /// <param name="expires">The cache expiration in minutes</param>
        /// <param name="lastModified">The last modified date</param>
        /// <exception cref="ArgumentNullException">If the fileContents parameter is null</exception>
        public ImageContentResult(byte[] fileContents, string contentType, int expires = -1, DateTime? lastModified = null)
            : base(fileContents, contentType)
        {
            this.expires = expires;
            this.lastModified = lastModified;
            this.FileDownloadName = Guid.NewGuid().ToString();
        }

        #endregion

        #region FileContentResult Override.

        /// <inheritdoc />
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ClearHeaders();
            base.ExecuteResult(context);
            context.HttpContext.Response.Expires = this.expires;
            if (this.lastModified.HasValue)
            {
                context.HttpContext.Response.AddHeader("Last-Modified", this.lastModified.Value.ToUniversalTime().ToString("R")); 
            }
            context.HttpContext.Response.Cache.SetCacheability(HttpCacheability.Private);
        }

        #endregion
    }
}