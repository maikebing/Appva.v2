﻿// <copyright file="InstallRoleToRole.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl.InstallRoleToRole
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
    public sealed class InstallRoleToRole : IRequest<Dictionary<string, IList<string>>>
    {
        /// <summary>
        /// If should install for all tenants
        /// </summary>
        public bool InstallGlobal
        {
            get;
            set;
        }
    }
}