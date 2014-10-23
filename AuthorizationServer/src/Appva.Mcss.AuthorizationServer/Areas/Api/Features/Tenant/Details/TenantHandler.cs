// <copyright file="TenantHandler.cs" company="Appva AB">
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
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mcss.AuthorizationServer.Models.Handlers;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class TenantHandler : PersistentRequestHandler<TenantId, TenantModel>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TenantHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override TenantModel Handle(TenantId message)
        {
            var tenant = this.Persistence.Get<Tenant>(message.Id);
            return new TenantModel()
            {
                Id = tenant.Id,
                Slug = tenant.Slug.Name,
                Identifier = tenant.Identifier,
                HostName = "notused", //tenant.HostName,
                Name = tenant.Name,
                Description = tenant.Description,
                Logotype = new Logotype(tenant.Image),
                ConnectionString = (tenant.DatabaseConnection == null) ? null : tenant.DatabaseConnection.ConnectionString
            };
        }

        #endregion
    }
}