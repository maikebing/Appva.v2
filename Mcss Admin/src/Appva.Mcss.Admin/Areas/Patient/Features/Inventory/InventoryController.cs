// <copyright file="InventoryController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/inventory")]
    public sealed class InventoryController : Controller
    {
        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list of 
        /// </summary>
        /// <param name="request">The <see cref="ListInventory"/></param>
        /// <returns>A <see cref="ListInventoryModel"/></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Inventory.ReadValue)]
        public ActionResult List(ListInventory request)
        {
            return this.View();
        }

        #endregion

        #region Create

        /// <summary>
        /// Returns the inventory create view
        /// </summary>
        /// <param name="request">A <see cref="Identity{CreateInventoryModel}"/></param>
        /// <returns>A <see cref="ListInventory"/></returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Inventory.CreateValue)]
        public ActionResult Create(Identity<CreateInventoryModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// The create POST
        /// </summary>
        /// <param name="request">A <see cref="CreateInventoryModel"/></param>
        /// <returns>A <see cref="ListInventory"/></returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "inventory")]
        [PermissionsAttribute(Permissions.Inventory.CreateValue)]
        public ActionResult Create(CreateInventoryModel request)
        {
            return this.View();
        }

        #endregion

        #region Update.

        /// <summary>
        /// Returns the inventory update view
        /// </summary>
        /// <param name="request">The <see cref="UpdateInventory"/></param>
        /// <returns>A <see cref="UpdateInventoryModel"/></returns>
        [Route("update/{inventory:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Inventory.UpdateValue)]
        public ActionResult Update(UpdateInventory request)
        {
            return this.View();
        }

        /// <summary>
        /// The update POST.
        /// </summary>
        /// <param name="request">An <see cref="UpdateInventoryModel"/></param>
        /// <returns>A <see cref="ListInventory"/></returns>
        [Route("update/{inventory:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "inventory")]
        [PermissionsAttribute(Permissions.Inventory.UpdateValue)]
        public ActionResult Update(UpdateInventoryModel request)
        {
            return this.View();
        }

        #endregion

        #region Inactivate

        /// <summary>
        /// Inactivates an inventory
        /// </summary>
        /// <param name="request">The <see cref="InactivateInventory"/></param>
        /// <returns>A <see cref="ListInventory"/></returns>
        [Route("inactivate/{inventory:guid}")]
        [HttpGet, Dispatch("list", "inventory")]
        [PermissionsAttribute(Permissions.Inventory.DeleteValue)]
        public ActionResult Inactivate(InactivateInventory request)
        {
            return this.View();
        }

        #endregion

        #region Reactivate

        /// <summary>
        /// Reactivates an inventory
        /// </summary>
        /// <param name="request">The <see cref="ReactivateInventory"/></param>
        /// <returns>A <see cref="ListInventory"/></returns>
        [Route("reactivate/{inventory:guid}")]
        [HttpGet, Dispatch("list", "inventory")]
        [PermissionsAttribute(Permissions.Inventory.UpdateValue)]
        public ActionResult Reactivate(ReactivateInventory request)
        {
            return this.View();
        }

        #endregion

        #region Add.

        /// <summary>
        /// Returns the add inventory view.
        /// </summary>
        /// <param name="request">The <see cref="AddInventoryItem"/></param>
        /// <returns>A <see cref="InventoryTransactionItemViewModel"/></returns>
        [Route("add")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Inventory.CreateValue)]
        public ActionResult Add(AddInventoryItem request)
        {
            return this.View();
        }

        #endregion

        #region Withdraw.

        /// <summary>
        /// Returns a withdraw from inventory view.
        /// </summary>
        /// <param name="request">The <see cref="WithdrawInventoryItem"/></param>
        /// <returns>A <see cref="InventoryTransactionItemViewModel"/></returns>
        [Route("withdraw")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Inventory.UpdateValue)]
        public ActionResult Withdraw(WithdrawInventoryItem request)
        {
            return this.View();
        }

        #endregion

        #region Recount.

        /// <summary>
        /// Returns the recount inventory view.
        /// </summary>
        /// <param name="request">The <see cref="RecountInventoryItem"/></param>
        /// <returns>A <see cref="InventoryTransactionItemViewModel"/></returns>
        [Route("recount")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Inventory.UpdateValue)]
        public ActionResult Recount(RecountInventoryItem request)
        {
            return this.View();
        }

        #endregion

        #region Overview Widget.

        /// <summary>
        /// Returns the dashboard widget.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("~/patient/inventory/overview")]
        [HttpGet, Dispatch(typeof(OverviewInventory))]
        [PermissionsAttribute(Permissions.Dashboard.ReadControlCountNarcoticsValue)]
        public PartialViewResult Overview()
        {
            return this.PartialView();
        }

        #endregion

        #region Create Transaction.

        /// <summary>
        /// Creates a transaction - either an addition, withdrawal or a recount for
        /// the specified inventory.
        /// </summary>
        /// <param name="request">The <see cref="CreateInventoryTransactionItem"/></param>
        /// <returns>A redirect to the return URL</returns>
        [Route("transaction/create")]
        [HttpPost, Dispatch, Validate, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Inventory.CreateValue)]
        public ActionResult CreateTransaction(CreateInventoryTransactionItem request)
        {
            return this.Redirect(request.ReturnUrl);
        }

        #endregion

        #endregion
    }
}