// <copyright file="AdministrationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.se">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Administration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("administration")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public class AdministrationController : Controller
    {
        #region Variables.



        #endregion

        #region Constructors.

        public AdministrationController()
        {
        }

        #endregion

        #region Routes.

        #region List.

        //// Create a list action and views.

        #endregion

        #region Create.

        [Route("create")]
        [HttpGet, Dispatch(typeof(CreateAdministration))]
        public ActionResult Create(CreateAdministration request)
        {
            return this.View();
        }

        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "inventory")]
        public ActionResult Create(CreateAdministrationModel request)
        {
            return this.View();
        }

        #endregion

        #region Update.

        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Update(UpdateAdministration request)
        {
            return this.View();
        }

        /// <summary>
        /// Post for update unit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "inventory")]
        public ActionResult Update(UpdateAdministrationModel request)
        {
            return this.View();
        }

        #endregion

        #region Delete.

        [Route("{id:guid}/delete")]
        [HttpGet, Dispatch("list", "inventory")]
        public ActionResult Delete(DeleteAdministrationModel request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}