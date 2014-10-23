// <copyright file="ClientsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Filters;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ClientsController : AdminController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ClientsController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion

        #region Routes.

        #region Details.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">The client id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Dispatch, Route("application/{slug}/{id:guid}")]
        public ActionResult Details(DetailsClientId request)
        {
            return View();
        }

        #endregion

        #region List.

        /// <summary>
        /// Returns a paged list of all clients.
        /// </summary>
        /// <param name="request">The paging parameters</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Dispatch, Route("applications")]
        public ActionResult List(PageableQueryParams<Pageable<ListClients>> request)
        {
            return View();
        }

        #endregion

        #region Create.

        /// <summary>
        /// Returns the create form.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Hydrate, Dispatch(typeof(NoParameter<CreateClient>)), Route("application/register")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the posted form model and redirects to /clients/details/{id:guid}.
        /// </summary>
        /// <param name="request">The form model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Clients"), Route("application/register"), AlertSuccess("Application created successfully!")]
        public ActionResult Create(CreateClient request)
        {
            return View();
        }

        #endregion

        #region Update.
        /*
        /// <summary>
        /// Returns the update form.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Update(UpdateClientId request)
        {
            return View();
        }

        /// <summary>
        /// Handles the posted form model and redirects to /clients/details/{id:guid}.
        /// </summary>
        /// <param name="request">The form model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Clients"), AlertSuccess("Client created successfully!")]
        public ActionResult Update(UpdateClient request)
        {
            return View();
        }
        */
        #endregion

        #endregion

        #region Child Actions.

        #region Authorization Grants.

        /// <summary>
        /// The application authorization grants child action for details page.
        /// </summary>
        /// <param name="request">The child request</param>
        /// <returns>A partial view</returns>
        [ChildActionOnly, Dispatch, Route("application/{id:guid}/authorization-grants")]
        public ActionResult AuthorizationGrants(ClientAuthorizationGrantsId request)
        {
            return PartialView();
        }

        #endregion

        #region Tenants.

        /// <summary>
        /// The application tenants child action for details page.
        /// </summary>
        /// <param name="request">The child request</param>
        /// <returns>A partial view</returns>
        [ChildActionOnly, Dispatch, Route("application/{id:guid}/tenants")]
        public ActionResult Tenants(ClientTenantsId request)
        {
            return PartialView();
        }

        #endregion

        #region Scopes. 

        /// <summary>
        /// The application scopes child action for details page.
        /// </summary>
        /// <param name="request">The child request</param>
        /// <returns>A partial view</returns>
        [ChildActionOnly, Dispatch, Route("application/{id:guid}/scopes")]
        public ActionResult Scopes(ClientScopesId request)
        {
            return PartialView();
        }

        #endregion

        #endregion
    }
}