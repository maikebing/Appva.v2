// <copyright file="UpdateAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateAccountHandler : RequestHandler<Identity<UpdateAccount>, UpdateAccount>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/> implementation
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="ISettingsService"/> implementation
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/> implementation
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountHandler"/> class.
        /// </summary>
        public UpdateAccountHandler(IAccountService accounts, ISettingsService settings, ITaxonomyService taxonomies)
        {
            this.accounts = accounts;
            this.settings = settings;
            this.taxonomies = taxonomies;
        }

        #endregion

        #region RequestHandler overrides.

        public override UpdateAccount Handle(Identity<UpdateAccount> message)
        {
            var account = this.accounts.Find(message.Id);
            return new UpdateAccount
            {
                IsHsaIdFieldVisible = this.settings.GetAdminLogin().Equals("siths"),
                IsMobileDevicePasswordEditable = this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable),
                IsMobileDevicePasswordFieldVisible = this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable),
                IsUsernameVisible = this.settings.Find<bool>(ApplicationSettings.IsUsernameVisible),
                Taxons = TaxonomyHelper.SelectList(account.Taxon, this.taxonomies.List(TaxonomicSchema.Organization)),
                Id = account.Id,
                DevicePassword = account.DevicePassword,
                Email = account.EmailAddress,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PersonalIdentityNumber = account.PersonalIdentityNumber,
                Taxon = account.Taxon.Id.ToString(),
                Username = account.UserName,
                HsaId = account.HsaId
            };
        }

        #endregion
    }
}