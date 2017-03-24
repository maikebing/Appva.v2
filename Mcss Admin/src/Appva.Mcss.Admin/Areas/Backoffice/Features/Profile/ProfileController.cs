// <copyright file="ProfileController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Profile
{

    #region Imports.
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Models.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("profile")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class ProfileController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        public ProfileController()
        {

        }

        #endregion

        #region Profile list.

        /// <summary>
        /// Profile list view
        /// </summary>
        /// <param name="request">Profile list</param>
        /// <returns>The list view as an ActionResult</returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Backoffice.ReadValue)]
        public ActionResult List(ProfileAssessment request)
        {
            return this.View();
        }

        /// <summary>
        /// Update a profile assessment.
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Dispatch]
        public ActionResult Update(Identity<UpdateProfileModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Update a profile assessment.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult Update(UpdateProfileModel request)
        {
            return this.RedirectToAction("list", new { Active = ListProfileHandler.RedirectActive });
        }

        [Route("install/profiles")]
        [HttpPost, Validate, Dispatch]
        public ActionResult InstallProfiles(InstallProfilesModel request)
        {
            return this.RedirectToAction("list");
        }

        #endregion
    }
}