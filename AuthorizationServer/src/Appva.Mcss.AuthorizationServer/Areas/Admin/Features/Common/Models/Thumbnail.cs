// <copyright file="Thumbnail.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Thumbnail
    {
        #region Variables.

        /// <summary>
        /// The thumbnail image file name.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// The thumbnail image mime type as hexadecimal.
        /// </summary>
        private readonly string mimeType;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Thumbnail"/> class.
        /// </summary>
        /// <param name="image">An <see cref="Image"/></param>
        public Thumbnail(Image image)
        {
            this.fileName = (image.IsNotNull()) ? image.FileName : null;
            this.mimeType = (image.IsNotNull()) ? image.MimeType : null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thumbnail"/> class.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="mimeType">The mime type</param>
        public Thumbnail(string fileName, string mimeType)
            : this(new Image(fileName, mimeType))
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the thumbnail file name.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Returns the thumbnail mime type.
        /// </summary>
        public string MimeType
        {
            get
            {
                return this.mimeType;
            }
        }

        #endregion
    }
}