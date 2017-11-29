// <copyright file="ContainerBuilderExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;

    #endregion

    /// <summary>
    /// An <see cref="ITenantClient"/> implementation.
    /// </summary>
    internal sealed class MockedTenantWcfClient : ITenantClient
    {
        /// <inheritdoc />
        public void Dispose()
        {
            //// no op.
        }

        /// <inheritdoc />
        public ITenantDto Find(Guid id)
        {
            return FindAsync(id).Result;
        }

        /// <inheritdoc />
        public Task<ITenantDto> FindAsync(Guid id)
        {
            return FindByIdentifierAsync(null);
        }

        /// <inheritdoc />
        public ITenantDto FindByIdentifier(ITenantIdentifier id)
        {
            return FindByIdentifierAsync(id).Result;
        }

        /// <inheritdoc />
        public Task<ITenantDto> FindByIdentifierAsync(ITenantIdentifier id)
        {
            return Task<ITenantDto>.Run(() => new MockedTenantDto() as ITenantDto);
        }

        /// <inheritdoc />
        public IClientDto FindClientByTenantId(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IClientDto> FindClientByTenantIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<ITenantDto> List()
        {
            return ListAsync().Result;
        }

        /// <inheritdoc />
        public Task<IList<ITenantDto>> ListAsync()
        {
            return Task<IList<ITenantDto>>.Run(() => new List<ITenantDto> { new MockedTenantDto() } as IList<ITenantDto>);
        }
    }

    /// <summary>
    /// An <see cref="ITenantDto"/> implementation.
    /// </summary>
    internal sealed class MockedTenantDto : ITenantDto
    {
        /// <summary>
        /// The appsetting key.
        /// </summary>
        private const string AppSettingsKey = "TenantWcfClient:ConnectionString";

        /// <summary>
        /// Indicates development.
        /// </summary>
        private const string Development = "development";

        /// <inheritdoc />
        public Guid Id
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        /// <inheritdoc />
        public string Identifier
        {
            get
            {
                return Development;
            }
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return Development;
            }
        }

        /// <inheritdoc />
        public string HostName
        {
            get
            {
                return Development;
            }
        }

        /// <inheritdoc />
        public string ConnectionString
        {
            get
            {
                if (! string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings.Get(AppSettingsKey)))
                {
                    return ConfigurationManager.AppSettings.Get(AppSettingsKey);
                }
                throw new NullReferenceException(string.Format("Appsettings key: {0} is null or empty", AppSettingsKey));                
            }
        }

        /// <inheritdoc />
        public IStatus Status
        {
            get
            {
                return null;
            }
        }
    }
}