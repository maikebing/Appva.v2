// <copyright file="AddNewsPermissionsAcl.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl.AddNewsPermissions
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdatePermissionsAcl : IRequest<Dictionary<string, string>>
    {
        /// <summary>
        /// If permissions should be updated globally
        /// </summary>
        public bool UpdateGlobal
        {
            get;
            set;
        }
    }
}