﻿// <copyright file="DelegationController.cs" company="Appva AB">
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
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DelegationController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationController"/> class.
        /// </summary>
        [RouteArea("area51"), RoutePrefix("")]
        [Permissions(Permissions.Area51.ReadValue)]
        public DelegationController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index()
        {
            return this.View();
        }

        [Route("addorganisation")]
        [Dispatch("Index","Delegation")]
        public ActionResult AddOrganisation(AddOrganisation request)
        {
            return this.View();
        }

        #endregion
    }
}