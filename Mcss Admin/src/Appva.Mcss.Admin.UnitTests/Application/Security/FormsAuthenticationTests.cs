// <copyright file="FormsAuthenticationTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Application.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.UnitTests.Tools;
    using Appva.Tenant.Interoperability.Client;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class FormsAuthenticationTests : InMemoryDatabase
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsAuthenticationTests"/> class.
        /// </summary>
        public FormsAuthenticationTests()
        {
        }

        #endregion

        #region Tests.

        /// <summary>
        /// Test: Save an entity to the in memory database.
        /// Expected Result: The entity iproperly s stored.
        /// </summary>
        [Fact]
        public void X_ExpectY()
        {
            IAuthenticationResult result;
            var x = this.CreateNew().AuthenticateWithPersonalIdentityNumberAndPassword(new PersonalIdentityNumber("19010101-1111"), "password", out result);
            Assert.True(x);
        }

        #endregion

        #region Private Functions.

        public IFormsAuthentication CreateNew()
        {
            /*return new FormsAuthentication(
                new IdentityService(new Dictionary<string, object>()),
                new TenantService(new TestTenantClient(), new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("test")),
                new AccountService(new AccountRepository(this.PersistenceContext), new RoleRepository(this.PersistenceContext), new PermissionRepository(this.PersistenceContext), this.PersistenceContext),
                new SettingsService(new RuntimeMemoryCache("test"), new SettingsRepository(this.PersistenceContext)));*/
            return null;
        }

        #endregion
    }

    public sealed class TestTenantClient : ITenantClient
    {
        #region ITenantClient Members

        public ITenantDto Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public ITenantDto FindByIdentifier(string id)
        {
            throw new NotImplementedException();
        }

        public IList<ITenantDto> List()
        {
            throw new NotImplementedException();
        }

        public IClientDto FindClientByTenantId(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITenantClientAsync Members

        public System.Threading.Tasks.Task<ITenantDto> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<ITenantDto> FindByIdentifierAsync(string id)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IList<ITenantDto>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IClientDto> FindClientByTenantIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}