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
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Admin.Application.Common;

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

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IPermissionRepository"/> implementation</param>
        public PermissionService(
            ISettingsService settings,
            IPermissionRepository repository)
        {
            this.settings   = settings;
            this.repository = repository;
        }

        #endregion

        #region IPermissionService Members.

        /// <inheritdoc />
        public IList<Permission> List(string bySchema = null)
        {
            var permissions = bySchema.IsEmpty() ? 
                this.repository.List() :
                this.repository.Search(bySchema);

            return HideNotActivated(permissions);
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

        #region Private helpers.

        /// <summary>
        /// Hides the not activated permissions.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <returns></returns>
        private IList<Permission> HideNotActivated(IList<Permission> permissions)
        {
            var notInstalled = new List<string>();

            //// If article module not installed
            if (this.settings.Find<OrderListConfiguration>(ApplicationSettings.OrderListSettings).IsInstalled == false)
            {
                notInstalled.Add(Permissions.OrderList.CreateValue);
                notInstalled.Add(Permissions.OrderList.ReadValue);
                notInstalled.Add(Permissions.OrderList.UpdateValue);
                notInstalled.Add(Permissions.OrderList.DeleteValue);
                notInstalled.Add(Permissions.Article.ReadValue);
            }

            return permissions.Where(x => !notInstalled.Contains(x.Resource)).ToList();
        }

        #endregion
    }
}