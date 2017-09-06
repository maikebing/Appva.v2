// <copyright file="TenaController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Signature
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("tena")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public class TenaController : Controller
    {
        #region Routes.

        #region List.

        /// <summary>
        /// List the Tena configuration options.
        /// </summary>
        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListTenaModel>))]
        [PermissionsAttribute(Permissions.Backoffice.ReadValue)]
        public ActionResult List(ListTenaModel request)
        {
            return this.View();
        }

        #endregion

        #region Update.

        /// <summary>
        /// Handles the update request.
        /// </summary>
        /// <param name="request">The <see cref="ListTenaModel"/>.</param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Backoffice.ReadValue)]
        public ActionResult Update(ListTenaModel request)
        {
            return this.View();
        }

        #endregion

        #region Install.

        /// <summary>
        /// Handles the install request.
        /// </summary>
        /// <param name="request">The <see cref="InstallTena"/>.</param>
        /// <returns></returns>
        [Route("install")]
        [HttpGet, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Backoffice.ReadValue)]
        public ActionResult Install(InstallTena request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}