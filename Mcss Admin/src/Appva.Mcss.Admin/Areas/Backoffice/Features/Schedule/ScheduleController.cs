// <copyright file="ScheduleController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Scehdule
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
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
    [RouteArea("Backoffice"),RoutePrefix("Schedule")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class ScheduleController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleController"/> class.
        /// </summary>
        public ScheduleController()
        {
        }

        #endregion

        #region Routes.

        #region List

        /// <summary>
        /// List all schedules
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [Dispatch(typeof(Parameterless<ListScheduleModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Details

        /// <summary>
        /// Details for a schedulesetting
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/details")]
        [HttpGet, Dispatch]
        public ActionResult Details(Identity<DetailsScheduleModel> request)
        {
            return this.View();
        }

        #endregion

        #region Update

        /// <summary>
        /// Creates the update view
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Update(Identity<UpdateScheduleModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the update request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("details","schedule")]
        public ActionResult Update(UpdateScheduleModel request)
        {
            return this.View();
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates the create view
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreateScheduleModel>))]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Handles the create request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("details", "schedule")]
        public ActionResult Create(CreateScheduleModel request)
        {
            return this.View();
        }

        #endregion

        #region Edit sign-options

        /// <summary>
        /// The edit signing options view
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/editsigningoptions")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult EditSigningOptions(Identity<EditSigningOptionsModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the edit signing options request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/editsigningoptions")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult EditSigningOptions(EditSigningOptionsModel request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Edit units

        /// <summary>
        /// The edit unit options view
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("editunits")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<EditUnitsModel>))]
        public ActionResult EditUnits()
        {
            return this.View();
        }

        /// <summary>
        /// Handles the edit unit options request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("editunits")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult EditUnits(EditUnitsModel request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #endregion
    }
}