// <copyright file="TenaController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Patient.Features.Tena
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>

    //[RouteArea("tena")]
    [RouteArea("patient"), RoutePrefix("{id:guid}/tena")]
    public sealed class TenaController : Controller
    {
        #region Routes.

        #region List Tena.

        /// <summary>
        /// Displays the observations list or the activation view.
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult List(ListTena request)
        {
            return this.View();
        }
        #endregion

        #region Activate Tena.

        /// <summary>
        /// Handles the activation request.
        /// </summary>
        /// <param name="request">The <see cref="ActivateTena"/>.</param>
        /// <returns>A <see cref="JsonResult"/>.</returns>
        [Route("activate")]
        [HttpPost, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ActivateValue)]
        public DispatchJsonResult Activate(ActivateTena request)
        {
            return this.JsonGet();
        }

        #endregion

        #endregion
    }
}