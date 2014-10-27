// <copyright file="UsersController.cs" company="Appva AB">
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
    using Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Filters;
    using Appva.Persistence;
    using Appva.Mcss.AuthorizationServer;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class UsersController : AdminController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public UsersController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion

        #region Routes.

        #region Details.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Dispatch, Route("user/{slug}/{id:guid}")]
        public ActionResult Details(DetailsUserId request)
        {
            return View();
        }

        #endregion

        #region List.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Dispatch, Route("users")]
        public ActionResult List(PageableQueryParams<Pageable<ListUser>> request)
        {
            return View();
        }

        #endregion

        #region Create.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Hydrate, Dispatch(typeof(NoParameter<CreateUser>)), Route("user/new")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Route("user/new"), Dispatch("Create", "UsersAuthentications")]
        public ActionResult Create(CreateUser request)
        {
            return View();
        }

        #endregion

        #endregion

        #region Child Actions.

        #region UserRoles

        [ChildActionOnly, Dispatch, Route("DetailsUserRoles")]
        public ActionResult DetailsUserRoles(DetailsUserRolesId request)
        {
            return PartialView();
        }

        #endregion

        #region UserAuthentications

        [ChildActionOnly, Dispatch, Route("DetailsUserAuthentications")]
        public ActionResult DetailsUserAuthentications(DetailsUserAuthenticationsId request)
        {
            return PartialView();
        }

        #endregion

        #region UserTenants

        [ChildActionOnly, Dispatch, Route("DetailsUserTenants")]
        public ActionResult DetailsUserTenants(DetailsUserTenantsId request)
        {
            return PartialView();
        }

        #endregion

        #endregion
    }
}