// <copyright file="TenantController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure.WebApi
{
    #region Imports.

    using System;
    using System.Web.Http;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Api.Models;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using System.Collections.Generic;
    using Appva.WebApi.Extensions;

    #endregion

    /// <summary>
    /// FIXME: Separate and remove Web api!
    /// </summary>
    [RoutePrefix("v1")]
    public class TenantController : DispatchApiController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantController"/> class.
        /// </summary>
        /// <param name="mediator">A <see cref="IMediator"/></param>
        public TenantController(IMediator mediator)
            : base(mediator)
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Returns the tenant by id.
        /// </summary>
        /// <param name="request">The tenant id</param>
        /// <returns>A single tenant</returns>
        [HttpGet, Route("tenant/{id:guid}")]
        public IHttpActionResult Tenant(Guid id)
        {
            if (! this.Request.IsLocal())
            {
                return this.BadRequest();
            }
            var model = this.Send<TenantModel>(new TenantId() { Id = id });
            if (model.IsNull())
            {
                this.NotFound();
            }
            return this.Ok(model);
        }

        /// <summary>
        /// Returns the client by tenant id.
        /// </summary>
        /// <param name="request">The tenant id</param>
        /// <returns>A single tenant</returns>
        [HttpGet, Route("tenant/{id:guid}/client")]
        public IHttpActionResult Client(Guid id)
        {
            if (!this.Request.IsLocal())
            {
                return this.BadRequest();
            }
            var model = this.Send<ClientModel>(new TenantIdForClient() { Id = id });
            if (model.IsNull())
            {
                this.NotFound();
            }
            return this.Ok(model);
        }

        /// <summary>
        /// Returns a collection of tenants.
        /// </summary>
        /// <returns>A collection of tenants</returns>
        [HttpGet, Route("tenants")]
        public IHttpActionResult Tenants()
        {
            if (!this.Request.IsLocal())
            {
                return this.BadRequest();
            }
            var model = this.Send<IList<TenantModel>>(new ListTenants());
            if (model.IsNull())
            {
                this.NotFound();
            }
            return this.Ok(model);
        }

        #endregion
    }
}