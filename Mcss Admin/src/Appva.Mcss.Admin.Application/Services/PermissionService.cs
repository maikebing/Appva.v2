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
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPermissionService : IService
    {
        /// <summary>
        /// Returns a collection of <see cref="Permission"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Permission"/></returns>
        IList<Permission> List(string bySchema = null);

        /// <summary>
        /// Returns a filtered collection of <see cref="Permission"/> by specified 
        /// ID:s.
        /// </summary>
        /// <param name="ids">The ID:s to retrieve</param>
        /// <returns>A filtered collection of <see cref="Permission"/></returns>
        IList<Permission> ListAllIn(params Guid[] ids);

        /// <summary>
        /// Returns all permissions for the roles.
        /// </summary>
        /// <param name="roles">A collection of roles</param>
        /// <returns>A collection of role permissions</returns>
        IList<Permission> ListByRoles(IList<Role> roles);
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
        public IList<Permission> List(string bySchema = null)
        {
            if (bySchema.IsEmpty())
            {
                return this.repository.List();
            }

            return this.repository.Search(bySchema);
            
        }

        /// <inheritdoc />
        public IList<Permission> ListAllIn(params Guid[] ids)
        {
            return this.repository.ListAllIn(ids);
        }

        /// <inheritdoc />
        public IList<Permission> ListByRoles(IList<Role> roles)
        {
            return this.repository.ByRoles(roles);
        }

        #endregion
    }
}