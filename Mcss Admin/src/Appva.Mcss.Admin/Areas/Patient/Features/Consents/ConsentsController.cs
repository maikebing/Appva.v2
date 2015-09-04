// <copyright file="ConsentsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Consents
{
    #region Imports.

    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize]
    [RouteArea("patient"), RoutePrefix("{id:guid}/consents")]
    public sealed class ConsentsController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsentsController"/> class.
        /// </summary>
        public ConsentsController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Create consents
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create")]
        [HttpGet, Dispatch()]
        public ActionResult Create(Identity<CreateConsent> request)
        {
            return this.View();
        }

        /// <summary>
        /// Create consents
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create")]
        [HttpPost, Dispatch()]
        public ActionResult Create(CreateConsent request)
        {
            return this.View();
        }

        #endregion
    }
}