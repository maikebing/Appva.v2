// <copyright file="SynchronizationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Synchronization
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("Practitioner"), RoutePrefix("Synchronization")]
    [PermissionsAttribute(Permissions.Synchronization.ReadValue)]
    public sealed class SynchronizationController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationController"/> class.
        /// </summary>
        public SynchronizationController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [PermissionsAttribute(Permissions.Synchronization.ReadValue)]
        [HttpGet, Dispatch]
        public ActionResult GetSynchronizedAccount(GetSynchronizedAccount request)
        {
            return this.View();
        }

        #endregion
    }
}