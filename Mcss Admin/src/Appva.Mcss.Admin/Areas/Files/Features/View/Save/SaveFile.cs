// <copyright file="SaveFile.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SaveFile : IRequest<FileContentResult>
    {
        /// <summary>
        /// The file id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }
    }
}