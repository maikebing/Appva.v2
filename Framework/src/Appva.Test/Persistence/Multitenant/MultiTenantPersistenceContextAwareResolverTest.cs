// <copyright file="MultiTenantPersistenceContextAwareResolverTest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Persistence.Multitenant
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Appva.Caching.Providers;
    using Appva.Persistence.MultiTenant;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MultiTenantPersistenceContextAwareResolverTest
    {
        #region Variables.

        /// <summary>
        /// The cache bucket.
        /// </summary>
        private const string CacheBucket = "test";

        /// <summary>
        /// The test id.
        /// </summary>
        private static readonly Guid TestId = new Guid("758EC77C-3FF0-44E5-A565-3A516FFD31F4");

        /// <summary>
        /// The test identifier.
        /// </summary>
        private static readonly ITenantIdentifier TestIdentifier = new TenantIdentifier("FOOBAR");

        #endregion

        #region Tests.

        /// <summary>
        /// Test: Locate by Guid id.
        /// Expected Result: Tenant will be found and start build a SessionFactory.
        /// </summary>
        [Fact]
        public void CanLocateTenantByGuid_WillThrowANhibernateException()
        {
            Assert.Throws(typeof(NHibernate.MappingException), () =>
                {
                    var config = new MultiTenantDatasourceConfiguration
                    {
                        UseIdAsIdentifier = true
                    };
                    var cache = new RuntimeMemoryCache(CacheBucket);
                    var client = new TestTenantClient(TestId, null);
                    var datasource = new MultiTenantDatasource(client, cache, config);
                    var result = datasource.Locate(new TenantIdentifier(TestId.ToString()));
                });
        }

        /// <summary>
        /// Test: Locate by Guid id.
        /// Expected Result: Tenant will be not found and throw a TenantNotFoundException.
        /// </summary>
        [Fact]
        public void CanNotLocateTenantByGuid_WillThrowATenantNotFoundException()
        {
            Assert.Throws(typeof(TenantNotFoundException), () =>
            {
                var config = new MultiTenantDatasourceConfiguration
                {
                    UseIdAsIdentifier = true
                };
                var cache = new RuntimeMemoryCache(CacheBucket);
                var client = new TestTenantClient(TestId, null);
                var datasource = new MultiTenantDatasource(client, cache, config);
                var result = datasource.Locate(new TenantIdentifier(Guid.NewGuid().ToString()));
            });
        }

        /// <summary>
        /// Test: Locate by ITenantIdentifier id.
        /// Expected Result: Tenant will be found and start build a SessionFactory.
        /// </summary>
        [Fact]
        public void CanLocateTenantByITenantIdentifier_WillThrowANhibernateException()
        {
            Assert.Throws(typeof(NHibernate.MappingException), () =>
            {
                var config = new MultiTenantDatasourceConfiguration
                {
                    UseIdAsIdentifier = false
                };
                var cache = new RuntimeMemoryCache(CacheBucket);
                var client = new TestTenantClient(Guid.Empty, TestIdentifier);
                var datasource = new MultiTenantDatasource(client, cache, config);
                var result = datasource.Locate(TestIdentifier);
            });
        }

        /// <summary>
        /// Test: Locate by ITenantIdentifier id.
        /// Expected Result: Tenant will be not found and throw a TenantNotFoundException.
        /// </summary>
        [Fact]
        public void CanNotLocateTenantByITenantIdentifier_WillThrowATenantNotFoundException()
        {
            Assert.Throws(typeof(TenantNotFoundException), () =>
            {
                var config = new MultiTenantDatasourceConfiguration
                {
                    UseIdAsIdentifier = false
                };
                var cache = new RuntimeMemoryCache(CacheBucket);
                var client = new TestTenantClient(Guid.Empty, TestIdentifier);
                var datasource = new MultiTenantDatasource(client, cache, config);
                var result = datasource.Locate(new TenantIdentifier("BAZBIZ"));
            });
        }

        #endregion
    }

    #region Test Classes.

    /// <summary>
    /// An <see cref="ITenantDto"/> implementation for testing.
    /// </summary>
    public sealed class TestTenant : ITenantDto
    {
        #region ITenantDto Members.

        /// <inheritdoc />
        public Guid Id
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Identifier
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Name
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string HostName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string ConnectionString
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// An <see cref="ITenantClient"/> implementation for testing.
    /// </summary>
    public sealed class TestTenantClient : ITenantClient
    {
        #region Variabels.

        /// <summary>
        /// The id to return.
        /// </summary>
        private readonly Guid id;

        /// <summary>
        /// The <see cref="ITenantIdentifier"/> to return.
        /// </summary>
        private readonly ITenantIdentifier identifier;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTenantClient"/> class.
        /// </summary>
        /// <param name="id">The id to return in <see cref="TestTenantClient.Find"/></param>
        /// <param name="identifier">The id to return in <see cref="TestTenantClient.FindByIdentifier"/></param>
        public TestTenantClient(Guid id, ITenantIdentifier identifier)
        {
            this.id = id;
            this.identifier = identifier;
        }

        #endregion

        #region ITenantClient Members.

        /// <inheritdoc />
        public ITenantDto Find(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return null;
            }
            if (this.id != id)
            {
                return null;
            }
            return new TestTenant
            {
                Id = this.id
            };
        }

        /// <inheritdoc />
        public ITenantDto FindByIdentifier(ITenantIdentifier id)
        {
            if (id == null || string.IsNullOrWhiteSpace(id.Value))
            {
                return null;
            }
            if (this.identifier == null)
            {
                return null;
            }
            if (this.identifier.Value != id.Value)
            {
                return null;
            }
            return new TestTenant
            {
                Identifier = this.identifier.Value
            };
        }

        /// <inheritdoc />
        public IList<ITenantDto> List()
        {
            return null;
        }

        /// <inheritdoc />
        public IClientDto FindClientByTenantId(Guid id)
        {
            return null;
        }

        #endregion

        #region ITenantClientAsync Members.

        /// <inheritdoc />
        public async Task<ITenantDto> FindAsync(Guid id)
        {
            return await Task.FromResult<ITenantDto>(null);
        }

        /// <inheritdoc />
        public async Task<ITenantDto> FindByIdentifierAsync(Tenant.Identity.ITenantIdentifier id)
        {
            return await Task.FromResult<ITenantDto>(null);
        }

        /// <inheritdoc />
        public async Task<IList<ITenantDto>> ListAsync()
        {
            return await Task.FromResult<IList<ITenantDto>>(null);
        }

        /// <inheritdoc />
        public async Task<IClientDto> FindClientByTenantIdAsync(Guid id)
        {
            return await Task.FromResult<IClientDto>(null);
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            //// No ops!
        }

        #endregion
    }

    #endregion
}