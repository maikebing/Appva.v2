using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Appva.Tenant.Identity;
using Appva.Tenant.Interoperability.Client;

namespace Appva.Mcss.Admin.Configuration
{
    public class MockedTenantWcfClient : ITenantClient
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ITenantDto Find(Guid id)
        {
            return FindAsync(id).Result;
        }

        public Task<ITenantDto> FindAsync(Guid id)
        {
            return FindByIdentifierAsync(null);
        }

        public ITenantDto FindByIdentifier(ITenantIdentifier id)
        {
            return FindByIdentifierAsync(id).Result;
        }

        public Task<ITenantDto> FindByIdentifierAsync(ITenantIdentifier id)
        {
            var task = Task<ITenantDto>.Run(() => MockedTenantDto.CreateDev());
            return task;
        }

        public IClientDto FindClientByTenantId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IClientDto> FindClientByTenantIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<ITenantDto> List()
        {
            return ListAsync().Result;
        }

        public Task<IList<ITenantDto>> ListAsync()
        {
            var task = Task<IList<ITenantDto>>.Run(() => new List<ITenantDto>() { MockedTenantDto.CreateDev() } as IList<ITenantDto>);
            return task;
        }
    }

    public class MockedTenantDto : ITenantDto
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Identifier
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string HostName
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get;
            set;
        }

        public IStatus Status
        {
            get;
            set;
        }

        public static ITenantDto CreateDev()
        {
            return new MockedTenantDto
            {
                Id = Guid.Empty,
                HostName = "development",
                Identifier = "development",
                Name = "Test",
                //ConnectionString = "Data Source=FREDRIKANDECC0F;Initial Catalog=AppvaTest;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"  // Standard connectionString 
                //ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\AppvaDev\\DB\\Test.mdf;Integrated Security=True;Connect Timeout=30" // Fredriks connectionString (OBS RÖR EJ!!!) 
                ConnectionString = "Server=localhost;Database=AppvaTest;Trusted_Connection=False;User ID = AppvaTest; Password=test"
            };
        }
    }
}