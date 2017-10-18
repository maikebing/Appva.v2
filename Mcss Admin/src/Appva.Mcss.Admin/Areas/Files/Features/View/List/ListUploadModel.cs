// <copyright file="ListUploadModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListUploadModel : IRequest<ListUploadModel>
    {
        #region Properties.

        /// <summary>
        /// A collection of files.
        /// </summary>
        public IList<ListUpload> Files
        {
            get;
            set;
        }

        /// <summary>
        /// The list will output images only if true, or every other type if false.
        /// </summary>
        public bool? IsFilteredByImages
        {
            get;
            set;
        }

        #endregion
    }
}