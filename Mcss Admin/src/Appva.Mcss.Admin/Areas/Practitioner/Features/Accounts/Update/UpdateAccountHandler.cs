// <copyright file="UpdateAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Ldap.Configuration;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateAccountHandler : RequestHandler<Identity<UpdateAccount>, UpdateAccount>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="accounts">The <see cref="IAccountService"/>.</param>
        /// <param name="settings">The <see cref="ISettingsService"/>.</param>
        /// <param name="taxonomies">The <see cref="ITaxonomyService"/>.</param>
        public UpdateAccountHandler(IIdentityService identityService, IAccountService accountService, ISettingsService settings, ITaxonomyService taxonomies)
        {
            this.identityService = identityService;
            this.accountService  = accountService;
            this.settings        = settings;
            this.taxonomies      = taxonomies;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateAccount Handle(Identity<UpdateAccount> message)
        {
            var id           = this.identityService.PrincipalId;
            var user         = this.accountService.Find(id);
            var account      = this.accountService.Find(message.Id);
            var ldapIsActive = this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled);
            var ldapConfig   = this.settings.Find<LdapConfiguration>(ApplicationSettings.LdapConfiguration);
            var selected     = account.Locations.Count > 0 ? 
                this.taxonomies.Find(account.Locations.First().Taxon.Id, TaxonomicSchema.Organization) :
                this.taxonomies.Roots(TaxonomicSchema.Organization).First();
            return new UpdateAccount
            {
                IsHsaIdFieldVisible                = (account.IsSynchronized && ldapIsActive) ? string.IsNullOrEmpty(ldapConfig.FieldHsaId) : this.settings.IsSithsAuthorizationEnabled() || this.settings.Find(ApplicationSettings.IsHsaIdVisible),
                IsMobileDevicePasswordEditable     = (account.IsSynchronized && ldapIsActive) ? string.IsNullOrEmpty(ldapConfig.FieldPin) && this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable) : this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable),
                IsMobileDevicePasswordFieldVisible = (account.IsSynchronized && ldapIsActive) ? string.IsNullOrEmpty(ldapConfig.FieldPin) && this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable) : this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable),
                IsUsernameVisible                  = this.settings.Find<bool>(ApplicationSettings.IsUsernameVisible),
                Taxons                             = TaxonomyHelper.CreateItems(user, selected, this.taxonomies.List(TaxonomicSchema.Organization)),
                Id                                 = account.Id,
                DevicePassword                     = account.DevicePassword,
                Email                              = account.EmailAddress,
                FirstName                          = account.FirstName,
                LastName                           = account.LastName,
                PersonalIdentityNumber             = account.PersonalIdentityNumber,
                Taxon                              = selected.Id.ToString(),
                Username                           = account.UserName,
                HsaId                              = account.HsaId,
                IsFirstNameFieldVisible            = account.IsSynchronized && ldapIsActive ? string.IsNullOrEmpty(ldapConfig.FieldFirstName) : true,
                IsLastNameFieldVisible             = account.IsSynchronized && ldapIsActive ? string.IsNullOrEmpty(ldapConfig.FieldLastName) : true,
                IsMailFieldVisible                 = account.IsSynchronized && ldapIsActive ? string.IsNullOrEmpty(ldapConfig.FieldMail) : true,
                RestrictUserToOrganizationTaxon    = account.Locations.Count > 0 && selected.Id == account.Taxon.Id,
                RestrictUserToOrganizationTaxonIsVisible = id == message.Id
            };
        }

        #endregion
    }
}