// <copyright file="CreatePractitionerTests.cs" company="Appva AB">
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
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Models.Handlers;
    using Appva.Mcss.Admin.UnitTests.Helpers;
    using Appva.Persistence;
    using Appva.Persistence.Tests;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreatePractitionerTests : IClassFixture<SqlDatasource<CreatePractitionerTestData>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="SqlDatasource{CreatePractitionerTestData}"/>.
        /// </summary>
        private readonly SqlDatasource<CreatePractitionerTestData> database;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePractitionerTests"/> class.
        /// </summary>
        /// <param name="database">The <see cref="SqlDatasource{CreatePractitionerTestData}"/></param>
        public CreatePractitionerTests(SqlDatasource<CreatePractitionerTestData> database)
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
        public void Should_HaveAGeneratedUserName_When_UserAccountIsCreated()
        {
            var context = this.database.PersistenceContext;
            var cache   = new TenantAwareMemoryCache(new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("https://schemas.appva.se/tests"));
            var setting = new SettingsService(cache, new SettingsRepository(context), context);
            var accountRepository = new AccountRepository(context);
            var roleRepository = new RoleRepository(context);
            var permissionRepository = new PermissionRepository(context);
            var httpContext = MockedHttpRequestBase.CreateNew();
            var taxonRepo = new TaxonRepository(context);
            var accountService = new AccountService(
                    accountRepository,
                    roleRepository,
                    permissionRepository,
                    context,
                    setting,
                    new AuditService(context, httpContext),
                    new IdentityService(new Dictionary<string, object>()),
                    new TaxonomyService(cache, taxonRepo));
            var handler = new CreateAccountPublisher(
                accountService, 
                setting, 
                new RoleService(roleRepository), 
                new TaxonomyService(cache, new TaxonRepository(context)),
                new PermissionService(permissionRepository),
                MockedNoOpMailService.CreateNew(), 
                null,
                MockedHttpRequestBase.CreateNew());
            context.BeginTransaction();
            handler.Handle(new Models.CreateAccountModel
            {
                FirstName = "John",
                LastName  = "Doe",
                PersonalIdentityNumber = new PersonalIdentityNumber("19010101-0101")
            });
            handler.Handle(new Models.CreateAccountModel
            {
                FirstName = "John",
                LastName  = "Doe",
                PersonalIdentityNumber = new PersonalIdentityNumber("19010101-0102")
            });
            context.Commit(true);
            var account = new AccountRepository(this.database.PersistenceContext).FindByPersonalIdentityNumber(new PersonalIdentityNumber("19010101-0102"));
            Assert.NotNull(account);
            Assert.Equal("johdoe1", account.UserName);
        }

        /// <summary>
        /// Test:    That the user account created with the nurse role gets fallback roles added.
        /// Expects: Added _DA and _BA roles.
        /// </summary>
        [Fact]
        public void Should_AddFallbackRoles_When_UserAccountIsCreated()
        {
            var context = this.database.PersistenceContext;
            var cache = new TenantAwareMemoryCache(new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("https://schemas.appva.se/tests"));
            var setting = new SettingsService(cache, new SettingsRepository(context), context);
            var accountRepository = new AccountRepository(context);
            var roleRepository = new RoleRepository(context);
            var permissionRepository = new PermissionRepository(context);
            var httpContext = MockedHttpRequestBase.CreateNew();
            var taxonRepo = new TaxonRepository(context);
            var accountService = new AccountService(
                    accountRepository,
                    roleRepository,
                    permissionRepository,
                    context,
                    setting,
                    new AuditService(context, httpContext),
                    new IdentityService(new Dictionary<string, object>()),
                    new TaxonomyService(cache, taxonRepo));
            var handler = new CreateAccountPublisher(
                accountService,
                setting,
                new RoleService(roleRepository),
                new TaxonomyService(cache, new TaxonRepository(context)),
                new PermissionService(permissionRepository),
                MockedNoOpMailService.CreateNew(),
                null,
                MockedHttpRequestBase.CreateNew());
            context.BeginTransaction();
            handler.Handle(new Models.CreateAccountModel
            {
                FirstName = "John",
                LastName = "Doe",
                PersonalIdentityNumber = new PersonalIdentityNumber("19010101-0101"),
                TitleRole = roleRepository.List().Where(x => x.MachineName == RoleTypes.Nurse).SingleOrDefault().Id.ToString()
            });
            context.Commit(true);
            var account = new AccountRepository(this.database.PersistenceContext).FindByPersonalIdentityNumber(new PersonalIdentityNumber("19010101-0101"));
            Assert.NotNull(account);
            Assert.Equal(3, account.Roles.Count);
            Assert.NotNull(account.Roles.Where(x => x.MachineName == RoleTypes.Nurse).SingleOrDefault());
            Assert.NotNull(account.Roles.Where(x => x.MachineName == RoleTypes.Device).SingleOrDefault());
            Assert.NotNull(account.Roles.Where(x => x.MachineName == RoleTypes.Backend).SingleOrDefault());
        }

        #endregion
    }

    /// <summary>
    /// Test data for <see cref="CreatePractitionerTests"/>.
    /// </summary>
    public sealed class CreatePractitionerTestData : DataPopulation
    {
        #region Variables.

        /// <summary>
        /// The current principal ID.
        /// </summary>
        public static readonly IList<Guid> CurrentPrincipalIds = new List<Guid>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePractitionerTestData"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public CreatePractitionerTestData(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region Abstract Members.

        /// <inheritdoc />
        public override void Populate()
        {
            var roles       = Defaults.CreateRoles();
            var taxonomies  = Defaults.CreateTaxonomies();
            var taxa        = Defaults.CreateTaxa(taxonomies);
            var permissions = Defaults.CreatePermissions();
            var settings    = Defaults.CreateSettings();
            this.SaveAll(settings);
            this.SaveAll(permissions);
            var role = roles.Where(x => x.MachineName == RoleTypes.Nurse).Single();
            role.Permissions = permissions;
            var principalId = this.Save(Defaults.CreatePrincipalAccount(roles));
            CurrentPrincipal.Ids.Add(principalId);
            this.SaveAll(roles);
            this.SaveAll(taxonomies);
            this.SaveAll(taxa);
        }

        #endregion
    }
}