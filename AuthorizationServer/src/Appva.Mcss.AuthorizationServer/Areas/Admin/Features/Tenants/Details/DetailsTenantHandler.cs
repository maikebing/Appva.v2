// <copyright file="DetailsTenantHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DetailsTenantHandler : PersistentRequestHandler<DetailsTenantId, DetailsTenant>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsTenantHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DetailsTenantHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override DetailsTenant Handle(DetailsTenantId message)
        {
            var tenant = this.Persistence.Get<Tenant>(message.Id);
            return new DetailsTenant
            {
                IsActive = tenant.IsActive,
                Id = tenant.Id,
                Slug = tenant.Slug.Name,
                Resource = new MetaData(tenant),
                Identifier = tenant.Identifier,
                HostName = tenant.HostName,
                Name = tenant.Name,
                Description = tenant.Description,
                Logotype = new Logotype(tenant.Image),
                ConnectionString = (tenant.DatabaseConnection == null) ? null : tenant.DatabaseConnection.ConnectionString
            };
        }

        #endregion
    }
}