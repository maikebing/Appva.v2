// <copyright file="DeviceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
// <author><a href="mailto:christoffer.rosenqvist@invativa.se">Christoffer Rosenqvist</a></author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Http;
    using Appva.Core.Extensions;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Application.Persistence;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Appva.Repository;
    using Appva.WebApi.Filters;
    using Models;
    using Transformers;

    #endregion

    /// <summary>
    /// Device endpoint.
    /// </summary>
    [RoutePrefix("v1/device")]
    public class DeviceController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly IDeviceRepository deviceRepository;

        /// <summary>
        /// The <see cref="ISettingRepository"/>.
        /// </summary>
        private readonly ISettingRepository settingRepository;

        /// <summary>
        /// The <see cref="ITaxonRepository"/>.
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="tenantService">The <see cref="ITenantService"/></param>
        /// <param name="deviceRepository">The <see cref="IDeviceRepository"/></param>
        /// <param name="settingRepository">The <see cref="ISettingRepository"/></param>
        /// <param name="taxonRepository">The <see cref="ITaxonRepository"/></param>
        public DeviceController(ITenantService tenantService, IDeviceRepository deviceRepository, ISettingRepository settingRepository, ITaxonRepository taxonRepository)
        {
            this.tenantService = tenantService;
            this.deviceRepository = deviceRepository;
            this.settingRepository = settingRepository;
            this.taxonRepository = taxonRepository;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Returns the <code>Device</code> status: e.g. active, inactive, pending, etc.
        /// </summary>
        /// <param name="id">The device id</param>
        /// <returns>Stringified device status</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{id}/status")]
        public IHttpActionResult Status(Guid id)
        {
            //// TODO: This is semantically incorrect - should return this.NotFound();
            var device = this.deviceRepository.Get(id);
            return device.IsNull() ? this.Ok(new { status = "notfound" }) : this.Ok(new { status = device.Active ? "active" : "inactive" });
        }

        /// <summary>
        /// Returns a combination of tenant and <code>Device</code> settings.
        /// </summary>
        /// <param name="id">The device id</param>
        /// <returns>Tenant and <code>Device</code> settings</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{id}/settings")]
        public IHttpActionResult Settings(Guid id)
        {
            var device = this.deviceRepository.Get(id);
            if (device.IsNull())
            {
                //// TODO: This is semantically incorrect - should return this.NotFound();
                return this.Ok(new { status = "notfound" });
            }
            var paging = Pageable<Setting>.Over(x => x.Active == true && x.Namespace == "MCSS.Device");
            var settings = this.settingRepository.List(paging);
            if (settings.IsNull())
            {
                return this.NotFound();
            }
            var data = new Dictionary<string, object>() { { "status", device.Active ? "active" : "inactive" } };
            settings.Entities.ForEach(x => data.Add(x.Name, x.Value));
            return this.Ok(data);
        }

        /// <summary>
        /// Enrolls, a.k.a. saves, a <code>Device</code>
        /// </summary>
        /// <param name="deviceModel">The device model</param>
        /// <returns><code>Device</code> ID and name</returns>
        [AuthorizeToken(Scope.AdminOnly)]
        [HttpPost, Validate, Route("enroll")]
        public IHttpActionResult Enroll(DeviceModel deviceModel)
        {
            var device = DeviceTransformer.ToDevice(deviceModel);
            if (this.deviceRepository.GetByUuid(device.Uuid).IsNotNull())
            {
                //// TODO: Remove DEBUG code.
                #if !DEBUG
                return InternalServerError(new Exception(string.Format("UUID {0} is already enrolled.", device.Uuid)));
                #endif
            }
            var taxon = this.taxonRepository.Get(deviceModel.TaxonId);
            if (taxon.IsNull())
            {
                return this.InternalServerError(new Exception("No taxon found, attempting to rollback!"));
            }
            device.Taxon = taxon;
            var id = (Guid) this.deviceRepository.Save(device);
            if (id.IsEmpty())
            {
                return this.InternalServerError(new Exception("No taxon found, attempting to rollback!"));
            }
            var client = this.tenantService.GetClientByTenantId(this.User.Identity.Tenant());
            if (client.IsNull())
            {
                return this.InternalServerError(new Exception("No client found, attempting to rollback!"));
            }
            return this.Ok(new
            {
                Id = device.Id,
                Name = device.Name,
                ClientIdentifier = client.Identifier,
                ClientSecret = client.Secret
            });
        }

        #endregion
    }
}