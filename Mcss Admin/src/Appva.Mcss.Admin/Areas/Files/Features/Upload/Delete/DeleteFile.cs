// <copyright file="DeleteFile.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeleteFile : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The file id.
        /// </summary>
        public Guid Id { get; set; }

        #endregion
    }
}