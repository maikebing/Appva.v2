// <copyright file="UploadFileModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Appva.Cqrs;
    

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UploadFileModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The uploaded file.
        /// </summary>
        [Required]
        public HttpPostedFileBase UploadedFile
        {
            get;
            set;
        }

        /// <summary>
        /// The title.
        /// </summary>
        [Required]
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

        #endregion
    }
}