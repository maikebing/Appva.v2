// <copyright file="ImageController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mvc.Imaging;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("admin")]
    public class ImageController : Controller
    {
        #region Variables.

        /// <summary>
        /// The image processor.
        /// </summary>
        private readonly IImageProcessor imageProcessor;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="imageProcessor">The <see cref="IImageProcessor"/></param>
        public ImageController(IImageProcessor imageProcessor)
        {
            this.imageProcessor = imageProcessor;
        }

        #endregion

        #region Routes.

        #region Image.
        
        /// <summary>
        /// Returns the image if the user is authenticated and the image
        /// exists on disk.
        /// </summary>
        /// <returns>A <see cref="FileResult"/> if found, otherwise null</returns>
        [HttpGet, Route("image/{fileName}/{mimeType}")]
        public FileResult Resolve(string fileName, string mimeType)
        {
            return (User.Identity.IsAuthenticated) ? this.imageProcessor.Read(fileName, mimeType) : null;
        }

        #endregion

        #endregion
    }
}