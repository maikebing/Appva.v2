// <copyright file="StatusTaxonController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.StatusTaxons
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Areas.Area51.Models;
using Appva.Mcss.Admin.Infrastructure.Attributes;
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
    [RouteArea("area51"), RoutePrefix("taxon")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class TaxonController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonController"/> class.
        /// </summary>
        public TaxonController()
        {
        }

        #endregion

        #region Routes.

        [Route("")]
        [HttpGet, Dispatch]
        public ActionResult Index(TaxonIndex request)
        {
            return this.View();
        }

        [Route("install/riskassesment")]
        [HttpPost, Validate, Dispatch("index","taxon")]
        public ActionResult InstallRiskAssesment(InstallRiskTaxons Request)
        {
            return this.View();
        }

        #endregion
    }
}