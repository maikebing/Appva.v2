// <copyright file="AddNewsPermissionsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl.AddNewsPermissions
{
    #region Imports.

    using Appva.Core.Contracts.Permissions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdatePermissionsHandler : NotificationHandler<UpdatePermissionsAcl>
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
        /// Initializes a new instance of the <see cref="UpdatePermissionsHandler"/> class.
        /// </summary>
        public UpdatePermissionsHandler(IRoleService roleService, ISettingsService settings, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.settings = settings;
            this.persistence = persistence;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(UpdatePermissionsAcl notification)
        {
            var currentPermissions = this.persistence.QueryOver<Permission>().Select(x => x.Resource).List<string>();
            this.UpdatePermissions(currentPermissions);
            return;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Update permissions.
        /// </summary>
        /// <returns></returns>
        private void UpdatePermissions(IList<string> permissions)
        {
            foreach (var type in typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
            {
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    if (field.FieldType != typeof(IPermission))
                    {
                        continue;
                    }
                    var keyAttr = field.GetCustomAttributes(typeof(KeyAttribute), false).SingleOrDefault() as KeyAttribute;
                    var nameAttr = field.GetCustomAttributes(typeof(NameAttribute), false).SingleOrDefault() as NameAttribute;
                    var descAttr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
                    var sortAttr = field.GetCustomAttributes(typeof(SortAttribute), false).SingleOrDefault() as SortAttribute;
                    var visiAttr = field.GetCustomAttributes(typeof(VisibilityAttribute), false).SingleOrDefault() as VisibilityAttribute;
                    var name = nameAttr.Value;
                    var description = descAttr.Value;
                    var sort = sortAttr != null ? sortAttr.Value : 0;
                    var isVisible = visiAttr != null ? visiAttr.Value == Visibility.Visible : true;
                    var permission = (IPermission)field.GetValue(null);
                    if (!permissions.Contains(permission.Value))
                    {
                        //// This i a new permission, lets add it
                        this.persistence.Save(new Permission(name, description, permission.Value, sort, isVisible));
                    }
                    else
                    {
                        //// This is an existing permission
                        //// TODO: Check if it needs to be updated
                    }
                }
            }
        }

        #endregion
    }
}