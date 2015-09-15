// <copyright file="MedicationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication
{
    #region Imports.

    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize]
    [RouteArea("patient"), RoutePrefix("{id:guid}/medication")]
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
        /// List all medications
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list")]
        [HttpGet, Dispatch()]
        public ActionResult List(ListMedication request)
        {
            return this.View();
        }

        /// <summary>
        /// Detailed view of an medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("details/{MedicationId}")]
        [HttpGet, Dispatch()]
        public ActionResult Details(DetailsMedication request)
        {
            return this.View();
        }

        /// <summary>
        /// Creates an sequence for an ordination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpGet, Dispatch("Details","Medication")]
        public ActionResult CreateSequence(CreateSequenceMedication request)
        {
            return this.View();
        }

        /// <summary>
        /// Detailed view of an medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update/{SequenceId:guid}")]
        [HttpGet, Dispatch()]
        public PartialViewResult UpdateSequence(UpdateSequenceMedication request)
        {
            return this.PartialView();
        }

        /// <summary>
        /// Detailed view of an medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update/{SequenceId:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Medication")]
        public PartialViewResult UpdateSequence(UpdateSequenceMedicationForm request)
        {
            return this.PartialView();
        }


        #endregion
    }
}