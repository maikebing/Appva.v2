// <copyright file="InstallRoleToRoleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl.InstallRoleToRole
{
    #region Imports.

    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Resources;
    using Appva.Core.Extensions;
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
    using Appva.Caching.Providers;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InstallRoleToRoleHandler : RequestHandler<InstallRoleToRole, Dictionary<string, IList<string>>>
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

        /// <summary>
        /// The <see cref="ITenantService"/>
        /// </summary>
        private readonly ITenantService tenants;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallRoleToRoleHandler"/> class.
        /// </summary>
        public InstallRoleToRoleHandler(IRoleService roleService, ISettingsService settings, IPersistenceContext persistence, IRuntimeMemoryCache cache, ITenantService tenants)
        {
            this.roleService = roleService;
            this.settings    = settings;
            this.persistence = persistence;
            this.cache       = cache;
            this.tenants     = tenants;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override Dictionary<string, IList<string>> Handle(InstallRoleToRole notification)
        {
            var retval = new Dictionary<string, IList<string>>();
            //// Run for all tenants in  cache
            if (notification.InstallGlobal)
            {
                var entries = this.cache.List().Where(x => x.Key.ToString().StartsWith(CacheTypes.Persistence.FormatWith(string.Empty))).ToList();

                foreach (var entry in entries)
                {
                    var factory = entry.Value as ISessionFactory;
                    try
                    {
                        using (var context = factory.OpenSession())
                        using (var transaction = context.BeginTransaction())
                        {
                            var aclIsActiveSetting = context.QueryOver<Setting>().Where(x => x.MachineName == "Mcss.Core.Security.Acl.IsInstalled").SingleOrDefault();
                            if (aclIsActiveSetting != null && aclIsActiveSetting.Value == "true")
                            {
                                var tenant = this.tenants.Find(new TenantIdentifier(entry.Key.ToString().Replace(CacheTypes.Persistence.FormatWith(string.Empty), string.Empty)));

                                var installed = this.Install(context);
                                transaction.Commit();
                                retval.Add(tenant.Name, installed);
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        var error = string.Format("Installering misslyckades, felmeddelande: {0}  \n Stack trace: {1}", e.Message, e.StackTrace);
                        retval.Add(entry.Key.ToString(), new List<string>() { error });
                    }
                }
            }
            //// Do only current tenant
            else
            {
                var currentPermissions = this.persistence.QueryOver<Permission>().Select(x => x.Resource).List<string>();
                var installed = this.Install(this.persistence.Session);
                retval.Add("Denna kund", installed);
            }
            return retval;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Roles that should not get the permissions
        /// </summary>
        private static readonly string[] excludedRoles = { "_ADMIN_D", "_BA", "_DA" };

        /// <summary>
        /// Install roles.
        /// </summary>
        /// <returns></returns>
        private IList<string> Install(ISession context)
        {
            

            var roles = context.QueryOver<Role>()
                .WhereRestrictionOn(x => x.MachineName).Not.IsIn(excludedRoles)
                .List();
            var allRoles = context.QueryOver<Role>()
                .Where(x => x.IsVisible)
                .List();

            var retval = new List<string>();
            foreach (var role in roles)
            {
                role.Roles = allRoles;
                context.Update(role);
                retval.Add(role.Name);
            }

            return retval;
        }

        #endregion
    }
}