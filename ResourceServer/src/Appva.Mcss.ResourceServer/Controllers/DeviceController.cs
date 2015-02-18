// <copyright file="DeviceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// <author>
//     <a href="mailto:christoffer.rosenqvist@invativa.se">Christoffer Rosenqvist</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Apis.TenantServer;
    using Application;
    using Application.Authorization;
    using Azure;
    using Core.Extensions;
    using Domain.Repositories;
    using Logging;
    using Mcss.Domain.Entities;
    using Models;
    using Repository;
    using Transformers;
    using WebApi.Filters;

    #endregion

    /// <summary>
    /// Device endpoint.
    /// </summary>
    [RoutePrefix("v1/device")]
    public class DeviceController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="DeviceController"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<DeviceController>();

        /// <summary>
        /// The <see cref="ITenantClient"/>.
        /// </summary>
        private readonly ITenantClient tenantClient;

        /// <summary>
        /// The <see cref="IPushNotification"/>.
        /// </summary>
        private readonly IPushNotification notification;

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
        /// <param name="tenantClient">The <see cref="ITenantClient"/></param>
        /// <param name="notification">The <see cref="IPushNotification"/></param>
        /// <param name="deviceRepository">The <see cref="IDeviceRepository"/></param>
        /// <param name="settingRepository">The <see cref="ISettingRepository"/></param>
        /// <param name="taxonRepository">The <see cref="ITaxonRepository"/></param>
        public DeviceController(ITenantClient tenantClient, IPushNotification notification, IDeviceRepository deviceRepository, ISettingRepository settingRepository, ITaxonRepository taxonRepository)
        {
            this.tenantClient = tenantClient;
            this.notification = notification;
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
            var data = new Dictionary<string, object> { { "status", device.Active ? "active" : "inactive" } };
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
                return this.InternalServerError(new Exception(string.Format("UUID {0} is already enrolled.", device.Uuid)));
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
            var client = this.tenantClient.GetClientByTenantId(this.User.Identity.Tenant());
            if (client.IsNull())
            {
                return this.InternalServerError(new Exception("No client found, attempting to rollback!"));
            }
            if (deviceModel.RemoteMessagingId.IsNotEmpty())
            {
                device.AzurePushId = this.notification.RegisterDevice(
                    deviceModel.RemoteMessagingId, 
                    new[] { "deviceId:" + device.Id });
                device.PushUuid = deviceModel.RemoteMessagingId;
            }
            return this.Ok(new
            {
                Id = device.Id,
                Name = device.Name,
                ClientIdentifier = client.Identifier,
                ClientSecret = client.Secret
            });
        }

        /// <summary>
        /// Update a device
        /// </summary>
        /// <param name="id">The <see cref="Device"/> id.</param>
        /// <param name="model">The update model</param>
        /// <returns>Http 200</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPost, Validate, Route("{id}/update")]
        public IHttpActionResult Update(Guid id, UpdateDeviceModel model)
        {
            Log.DebugFormat("Device id: {0} and remote messaging id: {1}", id, model.RemoteMessagingId);
            var device = this.deviceRepository.Get(id);
            if (!model.RemoteMessagingId.IsNotNull() || device.PushUuid == model.RemoteMessagingId)
            {
                return this.Ok();
            }
            if (device.AzurePushId.IsEmpty())
            {
                device.AzurePushId = this.notification.RegisterDevice(
                    model.RemoteMessagingId, 
                    new[] { "deviceId:" + device.Id });
            }
            else 
            {
                this.notification.UpdateDevice(device.AzurePushId, model.RemoteMessagingId);
            }
            device.PushUuid = model.RemoteMessagingId;
            return this.Ok();
        }

        /// <summary>
        /// Test-funktion för att debugga push
        /// TODO: Remove on launch
        /// </summary>
        /// <param name="id">The Device id</param>
        /// <returns>Http 200</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Validate, Route("{id}/push")]
        public IHttpActionResult Update(Guid id)
        {
            var model = new UpdateDeviceModel
            {
                RemoteMessagingId = "997187ed6249c5d62215179c8493be32bc7a81e5b9fe9a5bfa01abbfccfa13f3"
            };
            var device = this.deviceRepository.Get(id);
            if (model.RemoteMessagingId.IsNotNull())
            {
                if (device.AzurePushId.IsEmpty())
                {
                    device.AzurePushId = this.notification.RegisterDevice(
                        model.RemoteMessagingId, 
                        new[] { "deviceId:" + device.Id });
                }
                else 
                {
                    this.notification.UpdateDevice(device.AzurePushId, model.RemoteMessagingId);
                }
                device.PushUuid = model.RemoteMessagingId;
            }
            this.deviceRepository.Update(device);
            return this.Ok();
        }

        #endregion
    }
}