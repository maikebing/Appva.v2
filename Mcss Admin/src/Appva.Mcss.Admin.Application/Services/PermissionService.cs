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

    public interface IPermissionService : IService
    {
        /// <summary>
        /// Returns a collection of <see cref="Permission"/>.
        /// </summary>
        /// <param name="maximumItems">
        /// Optional maximum amount of items to be retrieved, defaults to <see cref="long.MaxValue"/>
        /// </param>
        /// <returns>A collection of <see cref="Permission"/></returns>
        IList<Permission> List();

        /// <summary>
        /// Returns a filtered collection of <see cref="Permission"/> by specified 
        /// ID:s.
        /// </summary>
        /// <param name="ids">The ID:s to retrieve</param>
        /// <returns>A filtered collection of <see cref="Permission"/></returns>
        IList<Permission> ListAllIn(params Guid[] ids);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PermissionService : IPermissionService
    {
        #region Variables.

        /// <summary>
        /// The implemented <see cref="IPermissionRepository"/> instance.
        /// </summary>
        private readonly IPermissionRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IPermissionRepository"/> implementation</param>
        public PermissionService(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region IPermissionService Members.

        /// <inheritdoc />
        public IList<Permission> List()
        {
            return this.repository.List();
        }

        /// <inheritdoc />
        public IList<Permission> ListAllIn(params Guid[] ids)
        {
            return this.repository.ListAllIn(ids);
        }

        #endregion
    }
}