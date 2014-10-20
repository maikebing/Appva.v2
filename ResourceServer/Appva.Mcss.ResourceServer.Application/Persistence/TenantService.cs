// <copyright file="TenantClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Core.Configuration;
    using Appva.Core.Extensions;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Newtonsoft.Json;

    #endregion

    public interface ITenantService
    {
        Tenant GetTenantById(object id);
        IList<Tenant> ListTenants();
        Client GetClientByTenantId(object id);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantService : ITenantService
    {
        #region Variables.

        private static readonly ResourceServerConfiguration Configuration;

        #endregion

        #region Constructor.

        static TenantService()
        {
            Configuration = ConfigurableApplicationContext.Get<ResourceServerConfiguration>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantService"/> class.
        /// </summary>
        public TenantService()
        {
        }

        #endregion

        #region ITenantService Members.

        public IList<Tenant> ListTenants()
        {
            var baseUri = Configuration.TenantServerUri;
            return this.Get<IList<Tenant>>(new Uri(baseUri, "tenants"));
        }

        public Tenant GetTenantById(object id)
        {
            var baseUri = Configuration.TenantServerUri;
            return this.Get<Tenant>(new Uri(baseUri, "tenant/{0}".FormatWith(id)));
        }

        public Client GetClientByTenantId(object id)
        {
            var baseUri = Configuration.TenantServerUri;
            return this.Get<Client>(new Uri(baseUri, "tenant/{0}/client".FormatWith(id)));
        }

        #endregion

        #region Private Functions.

        public T Get<T>(Uri uri) where T : class
        {
            var request = (HttpWebRequest) WebRequest.Create(uri);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    return new JsonSerializer().Deserialize<T>(jsonReader);
                }
            }
        }

        #endregion 
    }
    /// <summary>
    /// The tenant model.
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// The tenant id.
        /// </summary>
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant database connection.
        /// </summary>
        public string connection_string
        {
            get;
            set;
        }
    }

    /// <summary>
    /// The client model.
    /// </summary>
    public class Client
    {
        public string Identifier
        {
            get;
            set;
        }

        public string Secret
        {
            get;
            set;
        }

    }
}