// <copyright file = "GeneralSettingsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

using System.Web.Mvc;
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings
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
    using System.Web;
    using System.Web.Mvc;
    #endregion

    [RouteArea("backoffice"), RoutePrefix("generalsettings")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class GeneralSettingsController : Controller
    {

        #region List.
        /// <summary>
        /// Lists the settings
        /// </summary>
        /// <returns></returns>

        [Route("list")]
        [Dispatch(typeof(Parameterless<ListGeneralSettingsModel>))]
        public ActionResult List()
        {

            return this.View();
        }
        #endregion

        #region Save.

        
        [Route("list/save")]
        [HttpPost]
        public JsonResult Save(ListGeneralSettingsModel request)
        {
          return  Json("string");
        }
        #endregion

        /*
        #region Edit.
        /// <summary>
        /// edits a setting
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/edit")]
        [HttpGet, Dispatch]
        public ActionResult Edit(Identity<EditGeneralSettingsModel> request)
        {
            return this.View();
        }
        */

        #region Update.

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("list/update")]
        [HttpPost]
        public ActionResult Update(FormCollection request)
        {
            return Json("Hello");
        }

        #endregion
    }
}