// <copyright file="InventoryController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Inventory
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("inventory")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class InventoryController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        public InventoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region List

        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListInventoriesModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Add

        /// <summary>
        /// Add inventory unit
        /// </summary>
        /// <returns></returns>
        [Route("add")]
        [HttpGet, Dispatch(typeof(Parameterless<AddInventoryUnitModel>))]
        public ActionResult Add()
        {
            return this.View();
        }

        /// <summary>
        /// Post for add inventory unit
        /// </summary>
        /// <returns></returns>
        [Route("add")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list","inventory")]
        public ActionResult Add(AddInventoryUnitModel request)
        {
            return this.View();
        }

        #endregion

        #region Update
        /// <summary>
        /// Update unit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Update(Identity<UpdateInventoryUnitModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Post for update unit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list","inventory")]
        public ActionResult Update(UpdateInventoryUnitModel request)
        {
            return this.View();
        }

        #endregion

        #region Delete

        [Route("{id:guid}/delete")]
        [HttpGet, Dispatch("list", "inventory")]
        public ActionResult Delete(Identity<Parameterless<ListInventoriesModel>> request)
        {
            return this.View();
        }

        #endregion

        #region AddReason
        /// <summary>
        /// Add new withdrawal reason
        /// </summary>
        /// <returns></returns>
        [Route("addreason")]
        [HttpGet, Dispatch(typeof(Parameterless<AddReasonModel>))]
        public ActionResult AddReason()
        {
            return this.View();
        }

        /// <summary>
        /// Post new withdrawal reason
        /// </summary>
        /// <returns></returns>
        [Route("addreason")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "inventory")]
        public ActionResult AddReason(AddReasonModel request)
        {
            return this.View();
        }
        #endregion

        #region EditReason
        /// <summary>
        /// Get for edit reason
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/editreason")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult EditReason(Identity<EditReasonModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Post for edit reason
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/editreason")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "inventory")]
        public ActionResult EditReason(EditReasonModel request)
        {
            return this.View();
        }
        #endregion

        #region RemoveReason
        /// <summary>
        /// Get for remove reason
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/removereason")]
        [HttpGet, Dispatch("list", "inventory")]
        public ActionResult RemoveReason(RemoveReasonModel request)
        {
            return this.View();
        }
        #endregion

        #endregion
    }
}