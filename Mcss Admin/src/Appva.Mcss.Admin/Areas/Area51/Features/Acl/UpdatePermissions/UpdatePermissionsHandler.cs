// <copyright file="AddNewsPermissionsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl.AddNewsPermissions
{
    #region Imports.

    using Appva.Caching.Providers;
    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdatePermissionsHandler : RequestHandler<UpdatePermissionsAcl, Dictionary<string, string>>
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

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePermissionsHandler"/> class.
        /// </summary>
        public UpdatePermissionsHandler(IRoleService roleService, ISettingsService settings, IPersistenceContext persistence, IRuntimeMemoryCache cache)
        {
            this.roleService = roleService;
            this.settings = settings;
            this.persistence = persistence;
            this.cache = cache;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override Dictionary<string, string> Handle(UpdatePermissionsAcl notification)
        {
            var retval = new Dictionary<string, string>();
            //// Run for all tenants in  cache
            if (notification.UpdateGlobal)
            {
                var entries = this.cache.List().Where(x => x.Key.ToString().StartsWith(CacheTypes.Persistence.FormatWith(string.Empty))).ToList();

                foreach (var entry in entries)
                {
                    var factory = entry.Value as ISessionFactory;

                    using (var context = factory.OpenSession())
                    using (var transaction = context.BeginTransaction())
                    {
                        var aclIsActiveSetting = context.QueryOver<Setting>().Where(x => x.MachineName == "Mcss.Core.Security.Acl.IsInstalled").SingleOrDefault();
                        if(aclIsActiveSetting != null && aclIsActiveSetting.Value == "true")
                        {
                            var permissions = context.QueryOver<Permission>().Select(x => x.Resource).List<string>();
                            var insertedPermissions = this.UpdatePermissions(permissions, context);
                            transaction.Commit();
                            retval.Add(entry.Key.ToString(), string.Format("{0} behörigheter", insertedPermissions));
                        }
                        
                    }
                }
            }
            //// Do only current tenant
            else
            {
                var currentPermissions = this.persistence.QueryOver<Permission>().Select(x => x.Resource).List<string>();
                var addedCount = this.UpdatePermissions(currentPermissions, this.persistence.Session);
                retval.Add("Denna kund", string.Format("{0} behörigheter", addedCount));
            }
            
            return retval;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Update permissions.
        /// </summary>
        /// <returns></returns>
        private int UpdatePermissions(IList<string> permissions, ISession context)
        {
            var retval = 0;
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
                        context.Save(new Permission(name, description, permission.Value, sort, isVisible));
                        retval++;
                    }
                    else
                    {
                        //// This is an existing permission
                        //// TODO: Check if it needs to be updated
                    }
                }
            }
            return retval;
        }

        #endregion
    }
}