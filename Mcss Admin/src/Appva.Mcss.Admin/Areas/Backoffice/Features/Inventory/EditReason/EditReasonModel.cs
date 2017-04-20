// <copyright file="EditReasonModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditReasonModel : IRequest<bool>
    {
        /// <summary>
        /// The reason Id
        /// </summary>
        public Guid Id
        {
            get; set;
        }

        /// <summary>
        /// The reason name
        /// </summary>
        public string Name
        {
            get; set;
        }
    }
}