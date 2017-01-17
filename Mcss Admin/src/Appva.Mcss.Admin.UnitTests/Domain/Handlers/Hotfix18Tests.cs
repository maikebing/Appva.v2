// <copyright file="Hotfix18Tests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Domain.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Caching.Providers;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Cryptography;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Model.Handlers;
    using Appva.Mcss.Admin.UnitTests.Helpers;
    using Appva.Persistence;
    using Appva.Persistence.Tests;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Hotfix18Tests : IClassFixture<SqlDatasource<Hotfix18TestData>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="SqlDatasource{Hotfix18TestData}"/>.
        /// </summary>
        private readonly SqlDatasource<Hotfix18TestData> database;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotfix18Tests"/> class.
        /// </summary>
        /// <param name="database">The <see cref="SqlDatasource{Hotfix18TestData}"/></param>
        public Hotfix18Tests(SqlDatasource<Hotfix18TestData> database)
        {
            this.database = database;
        }

        #endregion

        #region Tests.

        /// <summary>
        /// Test:    Whether or not the user name is created by default on user account creation.
        /// Expects: User name is auto generated.
        /// </summary>
        [Fact]
        public void Should_AddFallbackRoles_When_NotAlreadyInDeviceOrBackedRole()
        {
            var context = this.database.PersistenceContext;
            context.BeginTransaction();
            var cache = new TenantAwareMemoryCache(new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("https://schemas.appva.se/tests"));
            var setting = new SettingsService(cache, new SettingsRepository(context), context);
            var accountRepository = new AccountRepository(context);
            var roleRepository = new RoleRepository(context);
            var permissionRepository = new PermissionRepository(context);
            var httpContext = MockedHttpRequestBase.CreateNew();
            var accountService = new AccountService(
                    accountRepository,
                    roleRepository,
                    permissionRepository,
                    context,
                    setting,
                    new AuditService(context, httpContext),
                    new IdentityService(new Dictionary<string, object>()));
            var handler = new Hotfix18Handler(accountService, context);
            handler.Handle(new Models.Hotfix18());
            context.Commit(true);
            var newContext = this.database.PersistenceContext;
            var accounts   = newContext.QueryOver<Account>().List();
            var actual     = 0;
            foreach(var account in accounts)
            {
                var isInDevice = account.Roles.Any(x => x.MachineName == RoleTypes.Device);
                var isInBackend = account.Roles.Any(x => x.MachineName == RoleTypes.Backend);
                if (isInDevice && isInBackend)
                {
                    actual++;
                }
            }
            Assert.Equal(3, actual);
        }

        #endregion
    }

    /// <summary>
    /// Test data for <see cref="Hotfix18Tests"/>.
    /// </summary>
    public sealed class Hotfix18TestData : DataPopulation
    {
        #region Variables.

        /// <summary>
        /// The current principal ID.
        /// </summary>
        public static readonly IList<Guid> CurrentPrincipalIds = new List<Guid>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotfix18TestData"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public Hotfix18TestData(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region Abstract Members.

        /// <inheritdoc />
        public override void Populate()
        {
            var roles = Defaults.CreateRoles();
            var taxonomies = Defaults.CreateTaxonomies();
            var taxa = Defaults.CreateTaxa(taxonomies);
            var permissions = Defaults.CreatePermissions();
            var settings = Defaults.CreateSettings();
            this.SaveAll(settings);
            this.SaveAll(permissions);
            var role = roles.Where(x => x.MachineName == RoleTypes.Nurse).Single();
            role.Permissions = permissions;
            var principalId = this.Save(Defaults.CreatePrincipalAccount(roles));
            CurrentPrincipal.Ids.Add(principalId);

            this.SaveAll(new List<Account>
            {
                this.CreateNewPractitioner(roles.Where(x => x.MachineName == RoleTypes.Nurse).ToList()),
                this.CreateNewPractitioner(roles.Where(x => x.MachineName == RoleTypes.Device).ToList()),
                this.CreateNewPractitioner(roles.Where(x => x.MachineName == RoleTypes.Backend).ToList()),
                this.CreateNewPractitioner(roles.Where(x => x.MachineName == RoleTypes.Dietician).ToList()),
                this.CreateNewPractitioner(roles)
            });
            this.SaveAll(roles);
            this.SaveAll(taxonomies);
            this.SaveAll(taxa);
        }

        private Account CreateNewPractitioner(IList<Role> roles)
        {
            var fn = Hash.Random(8).ToBase64();
            var ln = Hash.Random(8).ToBase64();
            var pn = Hash.Random(8).ToBase64();
            return new Account
            {
                FirstName = fn,
                LastName  = ln,
                FullName  = fn + " " + ln,
                SymmetricKey = Hash.Random(8).ToBase64(),
                Roles = roles,
                PersonalIdentityNumber = new PersonalIdentityNumber(pn)
            };
        }

        #endregion
    }
}