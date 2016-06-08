// <copyright file="InstallAuditPermissionsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
	#region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Appva.Caching.Providers;
    using Appva.Core.Contracts.Permissions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Features.Area51.Cache;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;

	#endregion

	/// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
    public class InstallAuditPermissionsHandler : RequestHandler<InstallAuditPermissions, IsAuditPermissionsInstalled>
	{
		#region Variables.

		/// <summary>
        /// The <see cref="IPersistenceContext"/>.
		/// </summary>
        private readonly IPersistenceContext context;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="InstallAuditPermissionsHandler"/> class.
		/// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public InstallAuditPermissionsHandler(IPersistenceContext context)
		{
            this.context = context;
		}

		#endregion

		#region RequestHandler Overrides.

		/// <inheritdoc />
        public override IsAuditPermissionsInstalled Handle(InstallAuditPermissions message)
		{
            var permissions = this.context.QueryOver<Permission>().List();
            if (
                permissions.Where(x => x.Resource.Equals(Permissions.Reports.SignInValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.Reports.AuditReportNotificationValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.Reports.ExcludeFromAudit)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.Reports.HideAuditRecordFromPublic)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.CreateAllDemographicInformationSensitivityValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.UpdateAllDemographicInformationSensitivityValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.CreatePeopleofPublicInterestValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.UpdatePeopleofPublicInterestValue)).Any())
            {
                return new IsAuditPermissionsInstalled { IsInstalled = true };
            }
            this.context.Save(CreateNewPermission(typeof(Permissions.Reports), "SignIn"));
            this.context.Save(CreateNewPermission(typeof(Permissions.Reports), "AuditReportNotification"));
            this.context.Save(CreateNewPermission(typeof(Permissions.Reports), "ExcludeFromAudit"));
            this.context.Save(CreateNewPermission(typeof(Permissions.Reports), "HideAuditRecordFromPublic"));
            this.context.Save(CreateNewPermission(typeof(Permissions.SecurityConfidentiality), "CreatePeopleofPublicInterest"));
            this.context.Save(CreateNewPermission(typeof(Permissions.SecurityConfidentiality), "UpdatePeopleofPublicInterest"));
            this.context.Save(CreateNewPermission(typeof(Permissions.SecurityConfidentiality), "CreateAllDemographicInformationSensitivity"));
            this.context.Save(CreateNewPermission(typeof(Permissions.SecurityConfidentiality), "UpdateAllDemographicInformationSensitivity"));
            return new IsAuditPermissionsInstalled { IsInstalled = true };
		}

		#endregion

        #region Private Methods.

        /// <summary>
        /// Creates a new permission from name and class.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="fieldName">The field name</param>
        /// <returns>A new permission</returns>
        private Permission CreateNewPermission(Type type, string fieldName)
        {
            var field       = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var nameAttr    = field.GetCustomAttributes(typeof(NameAttribute), false).SingleOrDefault() as NameAttribute;
            var descAttr    = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
            var sortAttr    = field.GetCustomAttributes(typeof(SortAttribute), false).SingleOrDefault() as SortAttribute;
            var visiAttr    = field.GetCustomAttributes(typeof(VisibilityAttribute), false).SingleOrDefault() as VisibilityAttribute;
            var name        = nameAttr.Value;
            var description = descAttr.Value;
            var sort        = sortAttr != null ? sortAttr.Value : 0;
            var isVisible   = visiAttr != null ? visiAttr.Value == Visibility.Visible : true;
            var permission  = (IPermission) field.GetValue(null);
            return new Permission(name, description, permission.Value, sort, isVisible);
        }
        #endregion
    }
}