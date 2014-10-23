// <copyright file="Image.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Common.Domain;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// Represents an image.
    /// </summary>
    public class Image : ValueObject<Image>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="fileName">The image file name</param>
        /// <param name="mimeType">The image mime type</param>
        public Image(string fileName, string mimeType)
        {
            this.FileName = fileName;
            this.MimeType = mimeType.ToHex();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate</remarks>
        protected Image()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the image name.
        /// </summary>
        public virtual string FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the mime type.
        /// </summary>
        public virtual string MimeType
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override bool Equals(Image other)
        {
            return other != null &&
                object.Equals(this.FileName, other.FileName) &&
                object.Equals(this.MimeType, other.MimeType);
        }

        #endregion
    }
}