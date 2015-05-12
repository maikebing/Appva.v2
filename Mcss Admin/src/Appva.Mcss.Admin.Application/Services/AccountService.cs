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
        /// Search for accounts to given search-criteria
        /// </summary>
        /// <param name="model">The search criteria</param>
        /// <param name="page">The page</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A <see cref="PageableSet"/> of <see cref="AccountModel"/></returns>
        PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10);

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="firstName">The account firstname</param>
        /// <param name="lastName">The account lastname</param>
        /// <param name="mail">The mail</param>
        /// <param name="mobileDevicePassword">The mobile device password</param>
        /// <param name="personalIdentityNumber">The <see cref="PersonalIdentityNumber"/></param>
        /// <param name="adress">The organisational <see cref="Taxon"/></param>
        /// <returns>The account id</returns>
        Guid Create(string firstName, string lastName, string mail, string mobileDevicePassword, PersonalIdentityNumber personalIdentityNumber, Taxon adress);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AccountService : IAccountService
    {
        #region Variables.

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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IAccountRepository"/> implementation</param>
        /// <param name="roles">The <see cref="IRoleRepository"/> implementation</param>
        /// <param name="permissions">The <see cref="IPermissionRepository"/> implementation</param>
        /// <param name="persitence">The <see cref="IPersistenceContext"/> implementation</param>
        public AccountService(IAccountRepository repository, IRoleRepository roles, IPermissionRepository permissions, IPersistenceContext persitence)
        {
            this.repository = repository;
            this.roles = roles;
            this.permissions = permissions;
            this.persitence = persitence;
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
            var password = EncryptionUtils.Hash(newPassword, account.Salt);
            account.ChangePassword(password, salt);
            this.repository.Update(account);
            //// TODO: Should send a mail here with a confirmation that the password has changed.
        }

        /// <inheritdoc />
        public PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10)
        {
            return this.repository.Search(model, page, pageSize);
        }

        /// <inheritdoc />
        public Guid Create(string firstName, string lastName, string mail, string mobileDevicePassword, PersonalIdentityNumber personalIdentityNumber, Taxon adress)
        {
            if (mail.IsNull())
            {
                throw new ArgumentNullException("Account must have mail");
            }
            if (personalIdentityNumber.IsNull())
            {
                throw new ArgumentNullException("Account must have PersonalIdentiyNumber");
            }
            if (adress.IsNull())
            {
                throw new ArgumentNullException("Account must have organisational adress");
            }

            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = string.Format("{0} {1}", firstName.Trim(), lastName.Trim()),
                DevicePassword = mobileDevicePassword,
                EmailAddress = mail,
                Taxon = adress
            };
            return (Guid)this.persitence.Save<Account>(account);
            
        }

        #endregion
    }
}