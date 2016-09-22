// <copyright file="DeviceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices
{
    #region Imports.

    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Areas.Devices.Features.Devices.List;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ReActivate;
    using Inactivate;

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

        #endregion
    }
}