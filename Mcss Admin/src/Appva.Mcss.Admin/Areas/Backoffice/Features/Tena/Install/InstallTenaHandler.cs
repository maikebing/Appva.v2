﻿// <copyright file="InstallTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Appva.Core.Contracts.Permissions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InstallTenaHandler : RequestHandler<InstallTena, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallTenaHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public InstallTenaHandler(IPersistenceContext persistence, ISettingsService settingsService)
        {
            this.persistence = persistence;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandlers overrides.

        /// <inheritdoc />
        public override bool Handle(InstallTena message)
        {
            var settings = this.settingsService.Find(ApplicationSettings.TenaSettings);
            var permissions = this.persistence.QueryOver<Permission>()
                .WhereRestrictionOn(x => x.Resource)
                    .IsLike("tena", MatchMode.Anywhere)
                        .List();

            if(permissions.Count == 0)
            {
                Install();
            }

            settings.IsInstalled = true;

            this.settingsService.Upsert(ApplicationSettings.TenaSettings, Domain.VO.TenaConfiguration.CreateNew(
                settings.ClientId, 
                settings.ClientSecret,
                settings.IsInstalled
            ));

            return true;
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private bool Install()
        {
            var permissions = new Dictionary<IPermission, Permission>();
            var hasFoundPermission = false;

            foreach (var type in typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (type.UnderlyingSystemType.Name == "Tena")
                {
                    hasFoundPermission = true;

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
                        var permission = (IPermission)field.GetValue(null);
                        permissions.Add(permission, new Permission(name, description, permission.Value, sort, isVisible));
                    }

                    if(hasFoundPermission)
                    {
                        break;
                    }
                }
            }

            foreach (var permission in permissions)
            {
                this.persistence.Save(permission.Value);
            }

            return true;
        }

        #endregion
    }
}