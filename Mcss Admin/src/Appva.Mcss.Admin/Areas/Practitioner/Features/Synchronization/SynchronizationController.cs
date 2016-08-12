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

        #region GetSynchronizedAccount.

        /// <summary>
        /// Displays the status of the sync for a specified account
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

        #region Activate LDAP sync.

        /// <summary>
        /// Activates the ldap synchronization for an account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("activate/{id:guid}/ldap")]
        [PermissionsAttribute(Permissions.Synchronization.CreateValue)]
        [HttpGet, Dispatch("GetSynchronizedAccount", "Synchronization")]
        public ActionResult ActivateLdap(ActivateLdap request)
        {
            return this.View();
        }

        #endregion

        #region Inactivate LDAP sync.

        /// <summary>
        /// Inactivates the ldap synchronization for an account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("inactivate/{id:guid}/ldap")]
        [PermissionsAttribute(Permissions.Synchronization.DeleteValue)]
        [HttpGet, Dispatch("GetSynchronizedAccount", "Synchronization")]
        public ActionResult InactivateLdap(InactivateLdap request)
        {
            return this.View();
        }

        #endregion

        #region Synchronize with Ldap

        /// <summary>
        /// Inactivates the ldap synchronization for an account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("inactivate/{id:guid}/ldap")]
        [PermissionsAttribute(Permissions.Synchronization.UpdateValue)]
        [HttpPost, Dispatch("GetSynchronizedAccount", "Synchronization")]
        public ActionResult SynchronizeLdap(SynchronizeLdap request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}