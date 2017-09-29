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
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListUploadModel
    {
        #region Properties.

        /// <summary>
        /// A collection of <see cref="DataFile"/>.
        /// </summary>
        public IList<DataFile> Files
        {
            get;
            set;
        }

        #endregion
    }
}