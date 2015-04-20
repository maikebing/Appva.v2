// <copyright file="PermissionService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PermissionService : IService
    {
        #region Variables.

        /// <summary>
        /// The implemented <see cref="IRuntimeMemoryCache"/> instance.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        /// <summary>
        /// The implemented <see cref="IPermissionRepository"/> instance.
        /// </summary>
        private readonly IPermissionRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="ICacheService"/> implementation</param>
        /// <param name="repository">The <see cref="IPermissionRepository"/> implementation</param>
        public PermissionService(IRuntimeMemoryCache cache, IPermissionRepository repository)
        {
            this.cache = cache;
            this.repository = repository;
        }

        #endregion

        #region Public Methods.

        public void PermissionsForUser(Account account)
        {
            
        }

        #endregion
    }
}