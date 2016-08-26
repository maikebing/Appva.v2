// <copyright file="DelegationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Delegation
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("delegation")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class DelegationController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationController"/> class.
        /// </summary>
        public DelegationController()
        {
        }

        #endregion

        #region Routes.

        #region Index

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        #region Add organistation to all delegations

        /// <summary>
        /// Adds organisation to all 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("addorganisation")]
        [Dispatch("Index","Delegation")]
        public ActionResult AddOrganisation(AddOrganisation request)
        {
            return this.View();
        }

        #endregion

        #region Delete delegation settings

        /// <summary>
        /// Specify settings for delete delegation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("delete/settings")]
        [HttpGet, Dispatch(typeof(Parameterless<DeleteDelegationSettingsModel>))]
        public ActionResult DeleteDelegationSettings()
        {
            return this.View();
        }

        /// <summary>
        /// Specify settings for delete delegation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("delete/settings")]
        [HttpPost, Validate, Dispatch("index","delegation")]
        public ActionResult DeleteDelegationSettings(DeleteDelegationSettingsModel request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}