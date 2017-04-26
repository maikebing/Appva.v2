// <copyright file="DeviceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Areas.Devices.Features.Devices.List;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Inactivate;
    using ReActivate;
    using System;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("devices"), RoutePrefix("")]
    public sealed class DevicesController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        public DevicesController()
        {
        }

        #endregion

        #region Routes.

        #region List

        [Route("list")]
        [HttpGet, Dispatch]
        public ActionResult List(ListDevice request)
        {
            return this.View();
        }

        #endregion

        #region Inactivate

        [Route("{id:guid}/inactivate")]
        [HttpGet, Dispatch("List", "Devices")]
        public ActionResult Inactivate(InactivateDevice request)
        {
            return this.View();
        }

        #endregion

        #region Reactivate

        [Route("{id:guid}/reactivate")]
        [HttpGet, Dispatch("List", "Devices")]
        public ActionResult Reactivate(ReactivateDevice request)
        {
            return this.View();
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit a device
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/edit")]
        [HttpGet, Dispatch]
        public ActionResult Edit(Identity<EditDeviceModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Edit a device
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/edit")]
        [HttpPost, Dispatch("List", "Devices")]
        public ActionResult Edit(EditDeviceModel request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}