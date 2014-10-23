// <copyright file="ProfilePicture.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using Appva.Mcss.AuthorizationServer.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ProfilePicture : Thumbnail
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePicture"/> class.
        /// </summary>
        /// <param name="image">An <see cref="Image"/></param>
        public ProfilePicture(Image image)
            : base(image)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePicture"/> class.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="mimeType">The mime type</param>
        public ProfilePicture(string fileName, string mimeType)
            : base(fileName, mimeType)
        {
        }

        #endregion
    }
}