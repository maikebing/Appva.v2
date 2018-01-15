// <copyright file="PractitionerRolesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerRolesHandler : RequestHandler<Identity<PractitionerRolesModel>, PractitionerRolesModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerRolesHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        public PractitionerRolesHandler(
            IFileService fileService, 
            ISettingsService settingsService,
            IIdentityService identityService,
            IAccountService accountService)
        {
            this.fileService = fileService;
            this.settingsService = settingsService;
            this.identityService = identityService;
            this.accountService = accountService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerRolesModel Handle(Identity<PractitionerRolesModel> message)
        {
            var file = this.fileService.Get(message.Id);
            var model = new PractitionerRolesModel();
            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);

            if (file == null ||
                properties.PractitionerImportProperties == null ||
                properties.PractitionerImportProperties.Roles == null)
            {
                return model;
            }

            var id = this.identityService.PrincipalId;
            var user = this.accountService.Find(id);
            model.Roles = this.ToViewModel(properties.PractitionerImportProperties.Roles, user.GetRoleAccess());
            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Gets a list of <see cref="PractitionerRoleViewModel"/>.
        /// </summary>
        /// <param name="importedRoles">The imported roles.</param>
        /// <param name="roles">The list of roles.</param>
        /// <returns>A collection of <see cref="PractitionerRoleViewModel"/>.</returns>
        private IEnumerable<PractitionerRoleViewModel> ToViewModel(IList<KeyValuePair<string, Guid>> importedRoles, IEnumerable<Role> roles)
        {
            foreach (var importedRole in importedRoles)
            {
                var view = new PractitionerRoleViewModel
                {
                    Label = importedRole.Key,
                    Roles = new List<SelectListItem>()
                };

                foreach (var role in roles)
                {
                    var isSelected = role.Name.ToLower() == importedRole.Key.ToLower().Trim();
                    var id = role.Id.ToString();

                    if (isSelected)
                    {
                        view.Id = id;
                    }

                    view.Roles.Add(new SelectListItem
                    {
                        Text = role.Name,
                        Value = id,
                        Selected = isSelected
                    });
                }

                yield return view;
            }
        }

        #endregion
    }
}