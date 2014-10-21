// <copyright file="TenantService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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

    /// <summary>
    /// The tenant service. 
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// Returns a tenant by id.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Tenant"/></returns>
        Tenant GetTenantById(object id);

        /// <summary>
        /// Returns a collection of <see cref="Tenant"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Tenant"/></returns>
        IList<Tenant> ListTenants();

        /// <summary>
        /// Returns a client by tenant id.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Client"/></returns>
        Client GetClientByTenantId(object id);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantService : ITenantService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ResourceServerConfiguration"/>.
        /// </summary>
        private static readonly ResourceServerConfiguration Configuration;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes static members of the <see cref="TenantService" /> class.
        /// </summary>
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

        /// <inheritdoc />
        public IList<Tenant> ListTenants()
        {
            var baseUri = Configuration.TenantServerUri;
            return this.Get<IList<Tenant>>(new Uri(baseUri, "tenants"));
        }

        /// <inheritdoc />
        public Tenant GetTenantById(object id)
        {
            var baseUri = Configuration.TenantServerUri;
            return this.Get<Tenant>(new Uri(baseUri, "tenant/{0}".FormatWith(id)));
        }

        /// <inheritdoc />
        public Client GetClientByTenantId(object id)
        {
            var baseUri = Configuration.TenantServerUri;
            return this.Get<Client>(new Uri(baseUri, "tenant/{0}/client".FormatWith(id)));
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Make a syncronous HTTP request.
        /// </summary>
        /// <typeparam name="T">The type to return</typeparam>
        /// <param name="uri">The full uri</param>
        /// <returns>A {T}</returns>
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
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Tenant
    {
        /// <summary>
        /// The tenant id.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant database connection.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public string connection_string
        {
            get;
            set;
        }
    }

    /// <summary>
    /// The client model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Client
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string Secret
        {
            get;
            set;
        }
    }
}