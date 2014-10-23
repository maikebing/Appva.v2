// <copyright file="PermissionsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Filters;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class PermissionsController : AdminController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public PermissionsController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Returns the a list of permissions.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Dispatch(typeof(ListPermission)), Route("permissions")]
        public ActionResult List()
        {
            return View();
        }

        #endregion

        #region Create.

        /// <summary>
        /// Returns the create form.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Hydrate, Dispatch(typeof(NoParameter<CreatePermission>)), Route("permission/new")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the posted form model and redirects.
        /// </summary>
        /// <param name="request">The form model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Permission"), Route("permission/new")]
        public ActionResult Create(CreatePermission request)
        {
            return View();
        }

        #endregion

        #endregion

    }
}