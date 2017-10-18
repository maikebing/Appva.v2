// <copyright file="ListUpload.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListUpload
    {
        #region Properties.

        /// <summary>
        /// The file id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The file name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// The file description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The file type.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// The file type image path.
        /// </summary>
        public string TypeImage
        {
            get;
            set;
        }

        /// <summary>
        /// The file size.
        /// </summary>
        public string Size
        {
            get;
            set;
        }

        /// <summary>
        /// The date when the file was saved.
        /// </summary>
        public string DateAdded
        {
            get;
            set;
        }

        /// <summary>
        /// The file properties.
        /// </summary>
        public FileUploadProperties Properties
        {
            get;
            set;
        }

        #endregion
    }
}