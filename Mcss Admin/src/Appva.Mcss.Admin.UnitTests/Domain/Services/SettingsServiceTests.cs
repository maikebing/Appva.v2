// <copyright file="SettingsServiceTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using Appva.Persistence.Tests;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SettingsServiceTests : IClassFixture<SqlDatasource<SettingsServiceTestData>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="SqlDatasource{SettingsServiceTestData}"/>.
        /// </summary>
        private readonly SqlDatasource<SettingsServiceTestData> database;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsServiceTests"/> class.
        /// </summary>
        /// <param name="database">The <see cref="SqlDatasource{SettingsServiceTestData}"/></param>
        public SettingsServiceTests(SqlDatasource<SettingsServiceTestData> database)
        {
            this.database = database;
        }

        #endregion

        #region Tests.

        /// <summary>
        /// Test:    Returns that the risk assessment is enabled
        /// Expects: The risk assessment to be enabled.
        /// </summary>
        [Fact]
        public void Should_ReturnTrue_When_IncorrectMachineNameButCorrectFallback()
        {
            var cache   = new TenantAwareMemoryCache(new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("https://schemas.appva.se/tests"));
            var setting = new SettingsService(cache, new SettingsRepository(this.database.PersistenceContext), this.database.PersistenceContext);
            Assert.Equal(true, setting.HasSeniorAlert());
        }

        /// <summary>
        /// Test:    Find should not accept null params
        /// Expects: Throws an ArgumentException
        /// </summary>
        [Fact]
        public void Should_Throw_Exception_When_Parameter_Null()
        {
            var cache   = new TenantAwareMemoryCache(new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("https://schemas.appva.se/tests"));
            var setting = new SettingsService(cache, new SettingsRepository(this.database.PersistenceContext), this.database.PersistenceContext);

            Assert.Throws<ArgumentNullException>(() => setting.Find<bool>(null));
        }

        /// <summary>
        /// Test:    Settings should be updated in cache on upsert
        /// Expects: Find should return true after upsert
        /// </summary>
        [Fact]
        public void Should_Update_Cache_On_Upsert()
        {
            var cache = new TenantAwareMemoryCache(new DevelopmentTenantIdentificationStrategy(), new RuntimeMemoryCache("https://schemas.appva.se/tests"));
            var setting = new SettingsService(cache, new SettingsRepository(this.database.PersistenceContext), this.database.PersistenceContext);

            //// Caches false
            setting.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled);

            //// Update to true
            setting.Upsert<bool>(ApplicationSettings.IsLdapConnectionEnabled, true);

            Assert.True(setting.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled));
        }

        #endregion
    }

    /// <summary>
    /// Test data for <see cref="SettingsServiceTests"/>.
    /// </summary>
    public sealed class SettingsServiceTestData : DataPopulation
    {
        #region Variables.

        

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsServiceTestData"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public SettingsServiceTestData(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region Abstract Members.

        /// <inheritdoc />
        public override void Populate()
        {
            this.SaveAll(this.CreateSettings());
        }

        #endregion

        #region Private Data Initializations.

        /// <summary>
        /// Creates a collection of <c>Setting</c>
        /// </summary>
        /// <returns>A collection of <c>Setting</c></returns>
        private IList<Setting> CreateSettings()
        {
            return new List<Setting> 
            {
                Setting.CreateNew("MCSS.Features.SeniorAlert.IsActive", "MCSS.SeniorAlert", "IsActive", null, "True", typeof(bool)),
                Setting.CreateNew("MCSS.SeniorAlert.IsActive", "MCSS.SeniorAlert", "IsActive", null, "False", typeof(bool)),
                Setting.CreateNew("MCSS.IsActive", "MCSS.SeniorAlert", "IsActive", null, "False", typeof(bool)),
                Setting.CreateNew("MCSS.SeniorAlert", "MCSS.SeniorAlert", "IsActive", null, "False", typeof(bool)),
                Setting.CreateNew("MCSS.SeniorAlert.IsEnabled", "MCSS.SeniorAlert", "IsActive", null, "False", typeof(bool)),
            };
        }

        #endregion
    }
}