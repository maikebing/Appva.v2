// <copyright file="DelegationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Delegation
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("delegation")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class DelegationController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationController"/> class.
        /// </summary>
        public DelegationController()
        {
        }

        #endregion

        #region Routes.

        #region List

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [Dispatch(typeof(Parameterless<ListDelegationModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Create delegation

        /// <summary>
        /// Create a new delegation
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/create")]
        [HttpGet, Dispatch]
        public ActionResult Create(Identity<CreateDelegationModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Create a new delegation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{categoryId:guid}/create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list","delegation")]
        public ActionResult Create(CreateDelegationModel request)
        {
            return this.View();
        }

        #endregion

        #region Update delegation

        /// <summary>
        /// Update a delegation
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Dispatch]
        public ActionResult Update(Identity<UpdateDelegationModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Create a new delegation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "delegation")]
        public ActionResult Update(UpdateDelegationModel request)
        {
            return this.View();
        }

        #endregion

        #region Settings

        /// <summary>
        /// Specify settings for delegations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("settings")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<DelegationSettingsModel>))]
        public ActionResult Settings()
        {
            return this.View();
        }

        /// <summary>
        /// Specify settings for delegations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("settings")]
        [HttpPost, Validate, Dispatch("list", "delegation")]
        public ActionResult Settings(DelegationSettingsModel request)
        {
            return this.View();
        }

        #endregion

        #region Create category

        /// <summary>
        /// Creates a new delegation category
        /// </summary>
        /// <returns></returns>
        [Route("category/create")]
        [HttpGet, Dispatch(typeof(Parameterless<CreateDelegationCategoryModel>))]
        public ActionResult CreateCategory()
        {
            return this.View();
        }

        /// <summary>
        /// Creates a new delegation category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("category/create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list","delegation")]
        public ActionResult CreateCategory(CreateDelegationCategoryModel request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}