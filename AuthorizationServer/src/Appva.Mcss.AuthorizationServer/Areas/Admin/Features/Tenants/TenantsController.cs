// <copyright file="TenantsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mcss.AuthorizationServer.Models;
    using Http = Appva.Mcss.AuthorizationServer.Common;
    using Appva.Mvc.Filters;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class TenantsController : AdminController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TenantsController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion

        #region Routes.

        #region Details.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet, Dispatch, Route("tenant/{slug}/{id:guid}")]
        public ActionResult Details(Id<DetailsTenant> request)
        {
            return View();
        }

        #endregion

        #region List.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet, Dispatch, Route("tenants")]
        public ActionResult List(PageableQueryParams<Pageable<ListTenants>> request)
        {
            return View();
        }

        #endregion

        #region Create.

        /// <summary>
        /// Creates the form for the client.
        /// </summary>
        /// <returns><see cref="ViewResult"/></returns>
        [HttpGet, Hydrate, Route("tenant/register")]
        public ActionResult Create()
        {
            return View(new CreateTenant());
        }

        /// <summary>
        /// Creates a client and redirects to details.
        /// </summary>
        /// <param name="request">The client create model request</param>
        /// <returns><see cref="ViewResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [Dispatch("Details", "Tenants"), Route("tenant/register"), AlertSuccess("Tenant created successfully!")]
        public ActionResult Create(CreateTenant request)
        {
            return View();
        }

        #endregion

        #region Edit.

        /// <summary>
        /// Creates the form for the client.
        /// </summary>
        /// <returns><see cref="ViewResult"/></returns>
        [HttpGet, Hydrate, Dispatch, Route("tenant/{slug}/{id:guid}/edit")]
        public ActionResult Edit(Id<EditTenantRequest> request)
        {
            return View();
        }

        /// <summary>
        /// Creates the form for the client.
        /// </summary>
        /// <returns><see cref="ViewResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [Audit(), Dispatch("Details", "Tenants"), Route("tenant/{slug}/{id:guid}/edit"), AlertSuccess("Tenant edited successfully!")]
        public ActionResult Edit(EditTenantResponse request)
        {
            return View();
        }

        #endregion

        #endregion
    }
}