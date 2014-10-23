// <copyright file="ListTenantHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Api.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    //using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mcss.AuthorizationServer.Models.Handlers;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTenantHandler : PersistentRequestHandler<ListTenants, IList<TenantModel>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTenantHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ListTenantHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<TenantModel> Handle(ListTenants message)
        {
            var tenants = this.Persistence.QueryOver<Tenant>()
                .Where(x => x.IsActive == true).List();
            var retval = new List<TenantModel>();
            foreach(var tenant in tenants)
            {
                retval.Add(new TenantModel()
                {
                    Id = tenant.Id,
                    Slug = tenant.Slug.Name,
                    Identifier = tenant.Identifier,
                    HostName = "notused",
                    Name = tenant.Name,
                    Description = tenant.Description,
                    Logotype = new Appva.Mcss.AuthorizationServer.Models.Logotype(tenant.Image),
                    ConnectionString = (tenant.DatabaseConnection == null) ? null : tenant.DatabaseConnection.ConnectionString
                });
            }
            return retval;
        }

        #endregion
    }
}