// <copyright file="AccountService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Core.Extensions;
    using Appva.Repository;
    using Validation;
    using Appva.Persistence;
    using Appva.Core.Resources;
    using NHibernate.Criterion;
    using Appva.Core.Messaging;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using System.Net.Mail;
    using System.Configuration;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Core.Logging;

    #endregion

    /// <summary>
    /// The <see cref="Account"/> service.
    /// </summary>
    public interface IAccountService : IService
    {
        /// <summary>
        /// Locates a user account by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        Account Find(Guid id);

        /// <summary>
        /// Locates a user account by its unique Personal Identity Number. 
        /// </summary>
        /// <param name="personalIdentityNumber">The unique Personal Identity Number</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber);

        /// <summary>
        /// Locates a user account by its unique user name. 
        /// </summary>
        /// <param name="userName">The unique user name</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        Account FindByUserName(string userName);

        /// <summary>
        /// Locates a user account by its HSA ID. 
        /// </summary>
        /// <param name="hsaId">The unique HSA id</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        Account FindByHsaId(string hsaId);

        /// <summary>
        /// Returns whether or not the user account is a member of at least one of the 
        /// specified roles.
        /// </summary>
        /// <param name="account">The user account to check</param>
        /// <param name="roles">
        /// At least one of the roles that the member must be a member of
        /// </param>
        /// <returns>True if the user is a member of any of the specified roles</returns>
        bool IsInRoles(Account account, params string[] roles);

        /// <summary>
        /// Returns whether or not the user account is a member of at least one of the 
        /// specified permissions.
        /// </summary>
        /// <param name="account">The user account to check</param>
        /// <param name="permissions">
        /// At least one of the permissions that the member must be a member of
        /// </param>
        /// <returns>
        /// True if the user is a member of any of the specified permissions
        /// </returns>
        bool HasPermissions(Account account, params string[] permissions);

        /// <summary>
        /// Returns the roles for the user account.
        /// </summary>
        /// <param name="account">The account to retrieve roles for</param>
        /// <returns>A collection of roles for the user account</returns>
        IList<Role> Roles(Account account);

        /// <summary>
        /// Returns the permissions for the user account.
        /// </summary>
        /// <param name="account">The account to retrieve permissions for</param>
        /// <returns>A collection of permissions for the user account</returns>
        IList<Permission> Permissions(Account account);

        /// <summary>
        /// Changes the old password to a new one.
        /// </summary>
        /// <param name="account">The account to change password</param>
        /// <param name="newPassword">The new password in clear text</param>
        /// <returns>True if successfully changes password</returns>
        void ChangePassword(Account account, string newPassword);

        /// <summary>
        /// InActivates the <see cref="Account"/>
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        void InActivate(Account account);

        /// <summary>
        /// Activates the inactivated <see cref="Account"/>
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        void Activate(Account account);

        /// <summary>
        /// Pause the <see cref="Account"/>
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        void Pause(Account account);

        /// <summary>
        /// Unpause the <see cref="Account"/>
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        void UnPause(Account account);

        /// <summary>
        /// Search for accounts to given search-criteria
        /// </summary>
        /// <param name="model">The search criteria</param>
        /// <param name="page">The page</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A <see cref="PageableSet"/> of <see cref="AccountModel"/></returns>
        PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10);

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        /// <param name="firstName">The account firstname</param>
        /// <param name="lastName">The account lastname</param>
        /// <param name="mail">The mail</param>
        /// <param name="mobileDevicePassword">The mobile device password</param>
        /// <param name="personalIdentityNumber">The <see cref="PersonalIdentityNumber"/></param>
        /// <param name="adress">The organisational <see cref="Taxon"/></param>
        /// <returns>The account id</returns>
        void Update(Account account, string firstName, string lastName, string mail, string mobileDevicePassword, PersonalIdentityNumber personalIdentityNumber, Taxon adress, string hsaId);

        /// <summary>
        /// Updates the roles for a user account.
        /// </summary>
        /// <param name="account">The user account to be updated</param>
        /// <param name="roles">The list of roles to be added</param>
        void UpdateRoles(Account account, IList<Role> roles);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="personalIdentityNumber"></param>
        bool ForgotPassword(string emailAddress, PersonalIdentityNumber personalIdentityNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="personalIdentityNumber"></param>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <param name="adress"></param>
        /// <param name="roles"></param>
        void Create(string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string emailAddress, string password, Taxon adress, IList<Role> roles, string hsaId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="personalIdentityNumber"></param>
        /// <param name="emailAddress"></param>
        /// <param name="webPassword"></param>
        /// <param name="adress"></param>
        /// <param name="roles"></param>
        /// <param name="devicePassword"></param>
        void CreateBackendAccount(string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string emailAddress, string webPassword, Taxon adress, IList<Role> roles, string hsaId, string devicePassword = null);

        /// <summary>
        /// Creates a proxy-entity from a guid
        /// </summary>
        /// <param name="id">The Guid</param>
        /// <returns></returns>
        Account Load(Guid id);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AccountService : IAccountService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<AccountService>();

        /// <summary>
        /// The <see cref="IAccountRepository"/> implementation.
        /// </summary>
        private readonly IAccountRepository repository;

        /// <summary>
        /// The <see cref="IRoleRepository"/> implementation.
        /// </summary>
        private readonly IRoleRepository roles;

        /// <summary>
        /// The <see cref="IPermissionRepository"/> implementation.
        /// </summary>
        private readonly IPermissionRepository permissions;

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation.
        /// </summary>
        private readonly IPersistenceContext persitence;

        /// <summary>
        /// The <see cref="ISimpleMailService"/>.
        /// </summary>
        private readonly ISimpleMailService mailService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IAccountRepository"/></param>
        /// <param name="roles">The <see cref="IRoleRepository"/></param>
        /// <param name="permissions">The <see cref="IPermissionRepository"/></param>
        /// <param name="persitence">The <see cref="IPersistenceContext"/></param>
        /// <param name="mailService">The <see cref="ISimpleMailService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        public AccountService(
            IAccountRepository repository, 
            IRoleRepository roles, 
            IPermissionRepository permissions, 
            IPersistenceContext persitence,
            ISimpleMailService mailService, 
            ISettingsService settingsService, 
            IAuditService auditing,
            IIdentityService identityService)
        {
            this.repository = repository;
            this.roles = roles;
            this.permissions = permissions;
            this.persitence = persitence;
            this.mailService = mailService;
            this.settingsService = settingsService;
            this.auditing = auditing;
            this.identityService = identityService;
        }

        #endregion

        #region IAccountService Members.

        /// <inheritdoc />
        public Account Find(Guid id)
        {
            return this.repository.Find(id);
        }

        /// <inheritdoc />
        public Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber)
        {
            return this.repository.FindByPersonalIdentityNumber(personalIdentityNumber);
        }

        /// <inheritdoc />
        public Account FindByUserName(string userName)
        {
            return this.repository.FindByUserName(userName);
        }

        /// <inheritdoc />
        public Account FindByHsaId(string hsaId)
        {
            return this.repository.FindByHsaId(hsaId);
        }

        /// <inheritdoc />
        public bool IsInRoles(Account account, params string[] roles)
        {
            return this.roles.IsInRoles(account, roles);
        }

        /// <inheritdoc />
        public bool HasPermissions(Account account, params string[] permissions)
        {
            return this.permissions.HasPermissions(account, permissions);
        }

        /// <inheritdoc />
        public IList<Role> Roles(Account account)
        {
            return this.roles.Roles(account);
        }

        /// <inheritdoc />
        public IList<Permission> Permissions(Account account)
        {
            return this.permissions.Permissions(account);
        }

        /// <inheritdoc />
        public void ChangePassword(Account account, string newPassword)
        {
            var salt = EncryptionUtils.GenerateSalt(DateTime.Now);
            var password = EncryptionUtils.Hash(newPassword, salt);
            account.ChangePassword(password, salt);
            this.repository.Update(account);
        }

        public bool ForgotPassword(string emailAddress, PersonalIdentityNumber personalIdentityNumber)
        {
            var account = this.repository.FindByPersonalIdentityNumber(personalIdentityNumber);
            if (account == null)
            {
                return false;
            }
            if (account.EmailAddress != emailAddress)
            {
                return false;
            }
            if (AccountUtils.IsBackendAccount(account) || this.HasPermissions(account, Common.Permissions.Admin.Login.Value))
            {
                var salt = EncryptionUtils.GenerateSalt(DateTime.Now);
                var password = EncryptionUtils.GeneratePassword();
                account.Salt = salt;
                account.AdminPassword = EncryptionUtils.Hash(password, salt);
                account.LastPasswordChangedDate = null;
                account.LockoutUntilDate = null;
                account.FailedPasswordAttemptsCount = 0;
                account.PasswordResetDate = DateTime.Now;
                this.repository.Update(account);
                var mail = new MailMessage("noreply@appva.se", account.EmailAddress)
                {
                    Subject = "Nytt lösenord till Appva MCSS",
                    Body = string.Format("<h3>Hej {0}!</h3><p>Ditt nya lösenord är {1}</p> <p>Har du ej begärt att få ett nytt lösenord var vänlig kontakta support@appva.se.</p>", account.UserName, password),
                    IsBodyHtml = true
                };
                this.mailService.Send(mail);
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10)
        {
            this.auditing.Read("läste medarbetarlista sida {0}", page);
            return this.repository.Search(model, page, pageSize);
        }

        /// <inheritdoc />
        public void UpdateRoles(Account account, IList<Role> newRoles)
        {
            var roles = newRoles ?? new List<Role>();
            if (! this.identityService.Principal.IsInRole(RoleTypes.Appva))
            {
                Log.Debug("The user is not of type {0} role", RoleTypes.Appva);
                //// If the current user is NOT an Appva role then make sure hidden roles are not removable 
                //// by adding the hidden roles to the 'new selected roles'.
                //// We do NOT add the static device or backend role simply because that is controlled by it's 
                //// permissions; e.g. Appva admin role will never be removed even if a non admin updates the 
                //// roles.
                var hiddenRoles = account.Roles.Where(x => x.IsVisible == false
                    && x.MachineName != RoleTypes.Device && x.MachineName != RoleTypes.Backend)
                    .Select(x => x).ToList();
                foreach (var hiddenRole in hiddenRoles)
                {
                    if (! roles.Contains(hiddenRole))
                    {
                        Log.Debug("Added hidden role {0}", hiddenRole.Name);
                        roles.Add(hiddenRole);
                    }
                }
            }
            //// Extract the permissions for the new roles (or combined).
            var permissions = this.permissions.ByRoles(roles);
            if (permissions.Any(x => x.Resource.Equals(Common.Permissions.Device.Login.Value)))
            {
                //// For backwards compatibility:
                //// If no device role has been added when the account has permission to access device -
                //// just add the device role.
                if (! roles.Any(x => x.MachineName.Equals(RoleTypes.Device)))
                {
                    var deviceRole = this.persitence.QueryOver<Role>().Where(x => x.MachineName == RoleTypes.Device).SingleOrDefault();
                    roles.Add(deviceRole);
                    Log.Debug("Added role {0}", deviceRole.Name);
                }
            }
            if (permissions.Any(x => x.Resource.Equals(Common.Permissions.Admin.Login.Value)))
            {
                //// For backwards compatibility:
                //// If no admin role has been added when the account has permission to access admin -
                //// just add the device role.
                if (! roles.Any(x => x.MachineName.Equals(RoleTypes.Backend)))
                {
                    var deviceRole = this.persitence.QueryOver<Role>().Where(x => x.MachineName == RoleTypes.Backend).SingleOrDefault();
                    roles.Add(deviceRole);
                    Log.Debug("Added role {0}", deviceRole.Name);
                }
                //// Create user name if none is set, this should be done during creation instead. 
                if (account.UserName == null)
                {
                    account.UserName = AccountUtils.CreateUserName(account.FirstName, account.LastName, GetUserNames());
                    account.Salt = EncryptionUtils.GenerateSalt(DateTime.Now);
                    account.AdminPassword = EncryptionUtils.Hash("abc123ABC", account.Salt);
                    Log.Debug("Generated new username {0}", account.UserName);
                }
            }
            //// Overwrite the new roles - no need to remove any roles per definition (device/backend).
            account.Roles = roles;
            this.repository.Update(account);
            this.auditing.Update("uppdaterade roller för {0} (REF: {1}).", account.FullName, account.Id);
        }

        public void Update(Account account, string firstName, string lastName, string emailAddress, string mobileDevicePassword, PersonalIdentityNumber personalIdentityNumber, Taxon adress, string hsaId)
        {
            var previousPassword = account.DevicePassword;
            account.UpdatedAt = DateTime.Now;
            account.FirstName = firstName;
            account.LastName = lastName;
            account.FullName = string.Format("{0} {1}", firstName.Trim(), lastName.Trim());
            account.DevicePassword = mobileDevicePassword;
            if (! emailAddress.IsNull())
            {
                account.EmailAddress = emailAddress;
            }
            if (! personalIdentityNumber.IsNull())
            {
                account.PersonalIdentityNumber = personalIdentityNumber;
            }
            if (! adress.IsNull())
            {
                account.Taxon = adress;
            }
            if (! hsaId.IsEmpty())
            {
                account.HsaId = hsaId;
            }
            this.repository.Update(account);
            this.auditing.Update("uppdaterade kontot för {0} (REF: {1}).", account.FullName, account.Id);
            if (previousPassword.IsNotEmpty() && previousPassword.NotEqual(mobileDevicePassword))
            {
                var mail = new MailMessage()
                {
                    Subject = ConfigurationManager.AppSettings.Get("EmailEditAccountSubject"),
                    Body = string.Format(ConfigurationManager.AppSettings.Get("EmailEditAccountBody"), account.FullName, account.DevicePassword, account.UserName),
                    IsBodyHtml = true
                };
                mail.To.Add(account.EmailAddress);
                this.mailService.Send(mail);
            }
        }

        /// <inheritdoc />
        public void InActivate(Account account)
        {
            account.IsActive = false;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("inaktiverade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void Activate(Account account)
        {
            account.IsActive = true;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("aktiverade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void Pause(Account account)
        {
            account.IsPaused = true;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("pausade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void UnPause(Account account)
        {
            account.IsPaused = false;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("avpausade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void Create(string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string emailAddress, string password, Taxon address, IList<Role> roles, string hsaId)
        {
            var roleList = roles.IsNull() ? new List<Role>() : roles;
            var account = new Account();
            account.FirstName = firstName.FirstToUpper();
            account.LastName = lastName.FirstToUpper();
            account.FullName = string.Format("{0} {1}", account.FirstName, account.LastName);
            account.PersonalIdentityNumber = personalIdentityNumber;
            account.EmailAddress = emailAddress;
            account.DevicePassword = password;
            account.Taxon = address;
            account.Roles = roleList;
            account.IsPaused = false;
            account.HsaId = hsaId;
            this.persitence.Save<Account>(account);
            this.auditing.Create("skapade ett konto för {0} (REF: {1}).", account.FullName, account.Id);
            var mailBody = this.settingsService.CreateAccountMailBody();
            if (mailBody.IsNotEmpty())
            {
                var mail = new MailMessage()
                {
                    Subject = ConfigurationManager.AppSettings.Get("EmailCreateAccountSubject"),
                    Body = string.Format(mailBody, account.FullName, account.DevicePassword),
                    IsBodyHtml = true
                };
                mail.To.Add(account.EmailAddress);
                this.mailService.Send(mail);
            }
        }

        /// <inheritdoc />
        public void CreateBackendAccount(string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string emailAddress, string webPassword, Taxon address, IList<Role> roles, string hsaId, string devicePassword = null)
        {
            var roleList = roles.IsNull() ? new List<Role>() : roles;
            roleList.Add(this.roles.Find(RoleTypes.Backend));
            var account = new Account();
            account.UserName = AccountUtils.CreateUserName(
                firstName,
                lastName,
                this.GetUserNames()
            );
            account.FirstName = firstName.FirstToUpper();
            account.LastName = lastName.FirstToUpper();
            account.FullName = string.Format("{0} {1}", account.FirstName, account.LastName);
            account.PersonalIdentityNumber = personalIdentityNumber;
            account.EmailAddress = emailAddress;
            account.Salt = EncryptionUtils.GenerateSalt(DateTime.Now);
            account.AdminPassword = EncryptionUtils.Hash(webPassword, account.Salt);
            account.DevicePassword = devicePassword;
            account.Taxon = address;
            account.Roles = roleList;
            account.IsPaused = false;
            account.HsaId = hsaId;
            this.persitence.Save<Account>(account);
            this.auditing.Create("skapade ett konto för {0} (REF: {1}).", account.FullName, account.Id);
            var mailBody = this.settingsService.CreateBackendAccountMailBody();
            if (mailBody.IsNotEmpty())
            {
                var mail = new MailMessage("noreply@appva.se", account.EmailAddress)
                {
                    Subject = ConfigurationManager.AppSettings.Get("EmailCreateAccountSubject"),
                    Body = string.Format(mailBody, account.FullName, account.DevicePassword, account.UserName, webPassword),
                    IsBodyHtml = true
                };
                this.mailService.Send(mail);
            }
        }

        /// <inheritdoc />
        public Account Load(Guid id)
        {
            return this.repository.Load(id);
        }

        #endregion

        #region Private Methods.

        private IList<string> GetUserNames()
        {
            return this.persitence.Session.CreateCriteria<Account>()
                .SetProjection(Projections.ProjectionList()
                .Add(Projections.Property("UserName")))
                .List<string>();
        }

        #endregion
    }
}