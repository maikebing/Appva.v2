// <copyright file="MedicationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Patient.Models;
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
    [RouteArea("patient"), RoutePrefix("{id:guid}/medication")]
    [PermissionsAttribute(Permissions.Medication.ReadValue)]
    public sealed class MedicationController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationController"/> class.
        /// </summary>
        public MedicationController()
        {
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Lists the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("list")]
        [PermissionsAttribute(Permissions.Medication.ReadValue)]
        [Dispatch]
        public ActionResult List(ListMedicationRequest request)
        {
            return this.View();
        }

        #endregion
    }
}