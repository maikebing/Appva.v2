// <copyright file="EhmController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Ehm
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("ehm")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class EhmController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EhmController"/> class.
        /// </summary>
        public EhmController()
        {
        }

        #endregion

        #region Routes.

        #region Index.

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [Dispatch(typeof(Parameterless<EhmSettingsModel>))]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        #region Update mock.

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("update/mock")]
        [HttpGet, Dispatch(typeof(Parameterless<UpdateMockModel>))]
        public ActionResult UpdateMock()
        {
            return this.View();
        }

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("update/mock")]
        [HttpPost, Dispatch("Index", "Ehm")]
        public ActionResult UpdateMock(UpdateMockModel request)
        {
            return this.View();
        }

        #endregion

        #region Update attributes.

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("update/attributes")]
        [HttpGet, Dispatch(typeof(Parameterless<UpdateAttributesModel>))]
        public ActionResult UpdateAttributes()
        {
            return this.View();
        }

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("update/attributes")]
        [HttpPost, Dispatch("Index", "Ehm")]
        public ActionResult UpdateAttributes(UpdateAttributesModel request)
        {
            return this.View();
        }

        #endregion


        #endregion
    }
}