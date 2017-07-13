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
    using Appva.Core.Logging;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate.Criterion;
    using Appva.Core.Extensions;
    using Appva.Cryptography;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain;

    #endregion

    /// <summary>
    /// The <see cref="Account"/> service.
    /// </summary>
    public interface IAccountService : IService
    {
        /// <summary>
        /// Returns the authenticated current principal.
        /// </summary>
        /// <returns>The current authenticated user.</returns>
        Account CurrentPrincipal();

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
        bool IsInAnyRoles(Account account, params string[] roles);

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
        bool HasAnyPermissions(Account account, params string[] permissions);

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
        /// Creates a unique user name for a user account.
        /// </summary>
        /// <param name="account">The account to generate a new user name</param>
        /// <returns>A unique user name for the account</returns>
        string CreateUniqueUserNameFor(Account account);

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
        /// Saves an account
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        /// <param name="location">The account location.</param>
        void Save(Account account, Location location);

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <param name="account">The <see cref="Account"/></param>
        void Update(Account account);

        /// <summary>
        /// Updates the roles for a user account.
        /// </summary>
        /// <param name="account">The user account to be updated</param>
        /// <param name="roles">The list of roles to be added</param>
        /// <param name="isAccountUpgradedForAdminAccess">If true then the user has got new roles which permits them to access admin</param>
        /// <param name="isAccountUpgradedForDeviceAccess">If true then the user has got new roles which permits them to access device</param>
        void UpdateRoles(Account account, IList<Role> roles, out bool isAccountUpgradedForAdminAccess, out bool isAccountUpgradedForDeviceAccess, Taxon location = null);

        /// <summary>
        /// Lists all accounts with expiring
        /// </summary>
        /// <param name="taxonFilter"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        IList<AccountModel> ListByExpiringDelegation(ITaxon taxonFilter, DateTime expiringDate, Guid? filterByIssuerId = null);

        /// <summary>
        /// Creates a proxy-entity from a guid
        /// </summary>
        /// <param name="id">The Guid</param>
        /// <returns></returns>
        Account Load(Guid id);

        /// <summary>
        /// Lists all the accounts
        /// </summary>
        /// <returns></returns>
        IList<Account> List();

        /// <summary>
        /// Returns locations for an account, never null
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        IList<Location> LocationsFor(Account account);
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
        /// The password format.
        /// </summary>
        private static readonly IDictionary<char[], int> PasswordFormat = new Dictionary<char[], int>
            {
                { "0123456789".ToCharArray(), 4 }
            };

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
        private readonly IPersistenceContext persistence;

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

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IAccountRepository"/></param>
        /// <param name="roles">The <see cref="IRoleRepository"/></param>
        /// <param name="permissions">The <see cref="IPermissionRepository"/></param>
        /// <param name="persitence">The <see cref="IPersistenceContext"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        public AccountService(
            IAccountRepository repository, 
            IRoleRepository roles, 
            IPermissionRepository permissions, 
            IPersistenceContext persitence,
            ISettingsService settingsService, 
            IAuditService auditing,
            IIdentityService identityService,
            ITaxonomyService taxonomies)
        {
            this.repository      = repository;
            this.roles           = roles;
            this.permissions     = permissions;
            this.persistence     = persitence;
            this.settingsService = settingsService;
            this.auditing        = auditing;
            this.identityService = identityService;
            this.taxonomies      = taxonomies;
        }

        #endregion

        #region IAccountService Members.

        /// <inheritdoc />
        public Account CurrentPrincipal()
        {
            return this.Find(this.identityService.PrincipalId);
        }

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
        public bool IsInAnyRoles(Account account, params string[] roles)
        {
            return this.roles.IsInAnyRoles(account, roles);
        }

        /// <inheritdoc />
        public bool HasAnyPermissions(Account account, params string[] permissions)
        {
            return this.permissions.HasAnyPermissions(account, permissions);
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
            var salt     = EncryptionUtils.GenerateSalt(DateTime.Now);
            var password = EncryptionUtils.Hash(newPassword, salt);
            account.ChangePassword(password, salt);
            this.auditing.ChangedPassword(account, "uppdaterade lösenord{0}", string.Empty);
            this.repository.Update(account);
        }

        /// <inheritdoc />
        public PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10)
        {
            this.auditing.Read("läste medarbetarlista sida {0}", page);
            var retval = this.repository.Search(model, page, pageSize);

            //// Fix to get locations in account model
            //// TODO: Add to main query
            var accounts  = retval.Entities.Select(x => x.Id).ToArray();
            var locations = this.persistence.QueryOver<Location>()
                .WhereRestrictionOn(x => x.Account.Id).IsIn(accounts)
                .List().GroupBy(x => x.Account.Id).ToDictionary(x => x.Key, g => g.ToList());

            foreach (var account in retval.Entities.Where(x => locations.ContainsKey(x.Id)))
            {
                account.Locations = locations[account.Id];
            }

            return retval;

        }

        /// <inheritdoc />
        public void Save(Account account, Location location)
        {
            this.persistence.Save(account);
            this.auditing.Create("skapade ett konto för {0} (REF: {1}).", account.FullName, account.Id);
            this.persistence.Save(location);
        }

        
        /// <inheritdoc />
        public void UpdateRoles(Account account, IList<Role> newRoles, out bool isAccountUpgradedForAdminAccess, out bool isAccountUpgradedForDeviceAccess, Taxon location = null)
        {
            var user            = this.CurrentPrincipal();
            var userAccessRoles = user.GetRoleAccess();
            
            isAccountUpgradedForAdminAccess = false;
            isAccountUpgradedForDeviceAccess = false;
            var roles = newRoles ?? new List<Role>();
            //// Extract the permissions for the new roles (or combined).
            var permissions         = this.permissions.ByRoles(roles);
            var previousPermissions = this.permissions.ByRoles(account.Roles);
            if (permissions.Any(x => x.Resource.Equals(Common.Permissions.Device.Login.Value)))
            {
                //// For backwards compatibility:
                //// If no device role has been added when the account has permission to access device -
                //// just add the device role.
                if (! roles.Any(x => x.MachineName.Equals(RoleTypes.Device)))
                {
                    var deviceRole = this.persistence.QueryOver<Role>().Where(x => x.MachineName == RoleTypes.Device).SingleOrDefault();
                    roles.Add(deviceRole);
                    Log.Debug("Added role {0}", deviceRole.Name);
                }
                //// If the account has no device password then they have just received new permissions.
                if (account.DevicePassword.IsEmpty())
                {
                    account.DevicePassword = this.settingsService.AutogeneratePasswordForMobileDevice() ? Password.Random(4, PasswordFormat) : null;
            }
                if (! previousPermissions.Any(x => x.Resource.Equals(Common.Permissions.Device.Login.Value)))
                {
                    isAccountUpgradedForDeviceAccess = true;
                }
            }
            if (permissions.Any(x => x.Resource.Equals(Common.Permissions.Admin.Login.Value)))
            {
                //// For backwards compatibility:
                //// If no admin role has been added when the account has permission to access admin -
                //// just add the device role.
                if (! roles.Any(x => x.MachineName.Equals(RoleTypes.Backend)))
                {
                    var deviceRole = this.persistence.QueryOver<Role>().Where(x => x.MachineName == RoleTypes.Backend).SingleOrDefault();
                    roles.Add(deviceRole);
                    Log.Debug("Added role {0}", deviceRole.Name);
                }
                //// Create user name if none is set, this should be done during creation instead. 
                if (account.UserName == null)
                {
                    account.UserName = this.CreateUniqueUserNameFor(account);
                    Log.Debug("Generated new username {0}", account.UserName);
                }
                if (! previousPermissions.Any(x => x.Resource.Equals(Common.Permissions.Admin.Login.Value)))
                {
                    isAccountUpgradedForAdminAccess = true;
                }
            }
            var difference = new List<Role>();
            foreach (var accountRole in account.Roles)
            {
                if (! userAccessRoles.Contains(accountRole))
                {
                    difference.Add(accountRole);
                }
            }
            foreach (var role in roles)
            {
                if (! difference.Contains(role))
                {
                    difference.Add(role);
                }
            }
            //// Overwrite the new roles - no need to remove any roles per definition (device/backend).
            account.Roles = difference;

            if (this.HasAnyPermissions(user, Common.Permissions.Practitioner.UpdateOrganizationPermissionValue) && location != null)
            {
                 var remove = account.Locations.Select(x => x).ToList();
                //// Remove any previous locations since it's stil 1-1.
                foreach (var previous in remove)
                {
                    this.persistence.Delete(previous);
                }
                //// Add the new location.
                this.persistence.Save(Location.New(account, location, 0));
                //// If the preferred taxon is set to something which outside of the new location
                //// make sure it updates too.
                if (account.Taxon != null)
                {
                    if (! account.Taxon.Path.ToLowerInvariant().Contains(location.Id.ToString().ToLowerInvariant()))
                    {
                        account.Taxon = location;
                    }
                }
                this.auditing.Update("uppdaterade organisations-behörighet för {0} till {1} (REF: {2}", account.Id, location.Name, location.Id);
            }
            this.repository.Update(account);
            this.auditing.Update("uppdaterade roller för {0}", account.Id);
        }

        /// <inheritdoc />
        public string CreateUniqueUserNameFor(Account account)
        {
            return this.CreateUserName(account.FirstName, account.LastName, this.ListAllUserNames());
        }

        /// <inheritdoc />
        public void Update(Account account)
        {
            this.repository.Update(account);
            this.auditing.Update("uppdaterade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void InActivate(Account account)
        {
            account.IsActive  = false;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("inaktiverade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void Activate(Account account)
        {
            account.IsActive  = true;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("aktiverade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void Pause(Account account)
        {
            account.IsPaused  = true;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("pausade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public void UnPause(Account account)
        {
            account.IsPaused  = false;
            account.UpdatedAt = DateTime.Now;
            this.repository.Update(account);
            this.auditing.Update("avpausade kontot för {0} (REF: {1}).", account.FullName, account.Id);
        }

        /// <inheritdoc />
        public Account Load(Guid id)
        {
            return this.repository.Load(id);
        }

        /// <inheritdoc />
        public IList<Account> List()
        {
            return this.repository.List();
        }

        /// <inheritdoc />
        public IList<AccountModel> ListByExpiringDelegation(ITaxon taxonFilter, DateTime expiringDate, Guid? filterByIssuerId = null)
        {
            var user = this.CurrentPrincipal();
            return this.repository.ListByExpiringDelegation(user, taxonFilter.Path, expiringDate, filterByIssuerId);
        }

        public IList<Location> LocationsFor(Account account)
        {
            if (account.Locations == null || account.Locations.Count() == 0)
            {
                var root = this.taxonomies.Roots(TaxonomicSchema.Organization).FirstOrDefault();
                var location = new Location(account, this.taxonomies.Load(root.Id));
                account.Locations = new List<Location> { location };
                this.persistence.Save<Location>(location);
                this.Update(account);
            }
            return account.Locations;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns all user names.
        /// </summary>
        /// <returns>A list of user names</returns>
        private IList<string> ListAllUserNames()
        {
            return this.persistence.Session.CreateCriteria<Account>()
                .SetProjection(Projections.ProjectionList()
                .Add(Projections.Property("UserName")))
                .List<string>();
        }

        /// <summary>
        /// Creates a unique username from firstname and lastname
        /// </summary>
        /// <param name="firstname">The first name</param>
        /// <param name="lastname">The last name</param>
        /// <param name="usernames">The user name list</param>
        /// <returns>A unique user name</returns>
        private string CreateUserName(string firstname, string lastname, IList<string> usernames)
        {
            var firstPart  = firstname.ToNullSafeLower().ToUrlFriendly();
            var secondPart =  lastname.ToNullSafeLower().ToUrlFriendly();
            var username = string.Format(
                "{0}{1}",
                (firstPart.Length  > 3) ? firstPart.Substring(0, 3)  : firstPart,
                (secondPart.Length > 3) ? secondPart.Substring(0, 3) : secondPart);
            if (! usernames.Contains(username))
            {
                return username;
            }
            var counter = 1;
            while (usernames.Contains(username + counter))
            {
                counter++;
            }
            return username + counter;
        }

        #endregion
    }
}