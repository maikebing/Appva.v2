// <copyright file="PatientController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Tena
{
    #region Imports;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Areas.Patient.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>

    //[RouteArea("tena")]
    [RouteArea("patient"), RoutePrefix("{id:guid}/tena")]
    public sealed class TenaController : Controller
    {
        #region Routes

        #region List Tena

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Patient/Tena

        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult List(ListTena request)
        {
            return this.View();
        }
        #endregion

        #region Activate Tena

        /// <summary>
        /// Handles the activation request.
        /// </summary>
        /// <param name="request">The <see cref="ActivateTena"/>.</param>
        /// <returns>A <see cref="JsonResult"/>.</returns>
        [Route("activate")]
        [HttpPost, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Tena.ActivateValue)]
        public DispatchJsonResult Activate(ActivateTena request)
        {
            return this.JsonPost();
        }

        #endregion

        #endregion
    }

}