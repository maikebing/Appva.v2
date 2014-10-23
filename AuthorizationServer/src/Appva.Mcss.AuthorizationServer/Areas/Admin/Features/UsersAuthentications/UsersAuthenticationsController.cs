// <copyright file="UsersAuthenticationsController.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class UsersAuthenticationsController : AdminController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersAuthenticationsController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public UsersAuthenticationsController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion

        #region Routes.

        #region Create.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Hydrate, Dispatch, Route("user/{id:guid}/authentications/new")]
        public ActionResult Create(CreateUserAuthenticationUserId request)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Users"), Route("user/{id:guid}/authentications/new")]
        public ActionResult Create(CreateUserAuthentication request)
        {
            return View();
        }

        #endregion

        #endregion

    }
}