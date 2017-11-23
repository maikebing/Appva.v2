// <copyright file="InstallAclHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InstallAclHandler : NotificationHandler<InstallAcl>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallAclHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public InstallAclHandler(IRoleService roleService, ISettingsService settings, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.settings = settings;
            this.persistence = persistence;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(InstallAcl notification)
        {
            if (this.settings.IsAccessControlListInstalled())
            {
                return;
            }
            var permissions = this.CreatePermissions();
            this.CreateRolePermissions(permissions);
            this.settings.Upsert(ApplicationSettings.IsAccessControlInstalled, true);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates permissions.
        /// </summary>
        /// <returns></returns>
        private IDictionary<IPermission, Permission> CreatePermissions()
        {
            var permissions = new Dictionary<IPermission, Permission>();
            foreach (var type in typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
            {
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    if (field.FieldType != typeof(IPermission))
                    {
                        continue;
                    }
                    var nameAttr = field.GetCustomAttributes(typeof(NameAttribute), false).SingleOrDefault() as NameAttribute;
                    var descAttr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
                    var sortAttr = field.GetCustomAttributes(typeof(SortAttribute), false).SingleOrDefault() as SortAttribute;
                    var visiAttr = field.GetCustomAttributes(typeof(VisibilityAttribute), false).SingleOrDefault() as VisibilityAttribute;
                    var name = nameAttr.Value;
                    var description = descAttr.Value;
                    var sort = sortAttr != null ? sortAttr.Value : 0;
                    var isVisible = visiAttr != null ? visiAttr.Value == Visibility.Visible : true;
                    var permission = (IPermission) field.GetValue(null);
                    permissions.Add(permission, new Permission(name, description, permission.Value, sort, isVisible));
                }
            }
            foreach (var permission in permissions)
            {
                this.persistence.Save(permission.Value);
            }
            return permissions;
        }

        /// <summary>
        /// Add all permissions to all roles - this MUST be edited manually later.
        /// </summary>
        /// <param name="permissions"></param>
        private void CreateRolePermissions(IDictionary<IPermission, Permission> permissions)
        {
            var roles = this.roleService.List();
            foreach (var role in roles)
            {
                role.Permissions = permissions.Where(x => x.Key != Permissions.Area51.Read).Select(x => x.Value).ToList();
                if (role.MachineName == RoleTypes.Appva)
                {
                    role.Permissions.Add(permissions[Permissions.Area51.Read]);
                }
            }
        }

        
        #endregion
    }
}