// <copyright file="UpdateAccountPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Modles.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Core.Messaging.RazorMail;
    using Appva.Mvc.Localization;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateAccountPublisher : RequestHandler<UpdateAccount, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        /// <summary>
        /// The <see cref="IRazorMailService"/>.
        /// </summary>
        private readonly IRazorMailService mailer;

        /// <summary>
        /// The <see cref="IHtmlLocalizer"/>
        /// </summary>
        private readonly IHtmlLocalizer localizer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountPublisher"/> class.
        /// </summary>
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="taxonomies">The <see cref="ITaxonomyService"/></param>
        /// <param name="mailer">The <see cref="IRazorMailService"/></param>
        public UpdateAccountPublisher(
            IAccountService accounts,
            ISettingsService settings,
            ITaxonomyService taxonomies,
            IRazorMailService mailer,
            IHtmlLocalizer localizer)
        {
            this.accounts   = accounts;
            this.settings   = settings;
            this.taxonomies = taxonomies;
            this.mailer     = mailer;
            this.localizer  = localizer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateAccount message)
        {
            if (message.PersonalIdentityNumber == null)
            {
                throw new Exception("Cannot uppdate practioner with null value to unique identifer");
            }
            var account       = this.accounts.Find(message.Id);
            var taxonId       = message.Taxon.IsNotEmpty() ? message.Taxon.ToGuid() : this.taxonomies.Roots(TaxonomicSchema.Organization).Single().Id;
            var taxon         = this.taxonomies.Get(taxonId);
            var configuration = this.settings.MailMessagingConfiguration();
            var isMobileDevicePasswordEditable = this.settings.Find<bool>(ApplicationSettings.IsMobileDevicePasswordEditable);
            var previousPassword = account.DevicePassword;
            account.UpdatedAt = DateTime.Now;
            account.FirstName = message.FirstName.Trim().FirstToUpper();
            account.LastName  = message.LastName.Trim().FirstToUpper();
            account.FullName  = this.localizer["FullnamePattern", account.FirstName, account.LastName].ToString();
            if (isMobileDevicePasswordEditable)
            {
                account.DevicePassword = message.DevicePassword;
            }
            if (message.Email.IsNotEmpty())
            {
                account.EmailAddress = message.Email;
            }
            if (this.settings.IsSithsAuthorizationEnabled() || this.settings.Find(ApplicationSettings.IsHsaIdVisible))
            {
                account.HsaId = message.HsaId;
            }
            if (message.PersonalIdentityNumber.IsNotNull())
            {
                //// If PersonalIdentityNumber is changed, disabel sync
                if(account.IsSynchronized)
                {
                    account.IsSynchronized = account.PersonalIdentityNumber == message.PersonalIdentityNumber;
                }
                account.PersonalIdentityNumber = message.PersonalIdentityNumber;
                if (this.settings.Find<bool>(ApplicationSettings.GenerateUniqueIdentifierForAccount))
                {
                    account.UserName = message.PersonalIdentityNumber.Value;
                }
            }
            if (taxon.IsNotNull())
            {
                account.Taxon = taxon;
            }
            this.accounts.Update(account);
            //// If the the new password is null or empty, or previous password is unaltered, then
            //// then no reason to re-send an e-mail.
            if (message.DevicePassword.IsEmpty() || (previousPassword.IsNotEmpty() &&  previousPassword.Equals(message.DevicePassword)))
            {
                return true;
            }
            //// If e-mail is not enabled, don't continue.
            if (! configuration.IsMobileDeviceRegistrationMailEnabled)
            {
                return true;
            }
            /*this.mailer.Send(MailMessage.CreateNew()
                .Template(I18nUtils.GetEmailTemplatePath("UpdateUserMobileDeviceEmail"))
                .Model<RegistrationForDeviceEmail>(new RegistrationForDeviceEmail
                    {
                        Name = account.FullName,
                        Password = account.DevicePassword
                    })
                .To(account.EmailAddress)
                .Subject("Ditt MCSS konto har uppdaterats")
                .Build());*/
            return true;
        }

        #endregion
    }
}