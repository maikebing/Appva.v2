// <copyright file="InactivateSignatureModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    #endregion

    /// <summary>
    /// Inactivate Model
    /// </summary>
    public sealed class InactivateSignatureModel : IRequest<bool>
    {
        /// <summary>
        /// The signature Id
        /// </summary>
        public Guid Id { get; set; }
    }
}