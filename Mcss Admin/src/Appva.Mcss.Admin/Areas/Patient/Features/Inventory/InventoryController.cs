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
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize]
    [RouteArea("patient"), RoutePrefix("{id:guid}/inventory")]
    public sealed class InventoryController : Controller
    {
        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list of 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="inventoryId">The inventory id</param>
        /// <param name="year">Optional year</param>
        /// <param name="month">Optional month</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <param name="page">Optional page - defaults to 1</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        public ActionResult List(ListInventory request)
        {
            return this.View();
        }

        #endregion

        #region Add.

        /// <summary>
        /// Returns the add inventory view.
        /// </summary>
        /// <param name="id">TODO: This is the patient id ... The inventory id</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("add")]
        [HttpGet, Dispatch]
        public ActionResult Add(AddInventoryItem request)
        {
            return this.View();
        }

        #endregion

        #region Withdraw.

        /// <summary>
        /// Returns a withdraw from inventory view.
        /// </summary>
        /// <param name="id">The inventory id</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("withdraw")]
        [HttpGet, Dispatch]
        public ActionResult Withdraw(WithdrawInventoryItem request)
        {
            return this.View();
        }

        #endregion

        #region Recount.

        /// <summary>
        /// Returns the recount inventory view.
        /// </summary>
        /// <param name="id">The inventory id</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("recount")]
        [HttpGet, Dispatch]
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
        /// <param name="id">TODO: what id is id?</param>
        /// <param name="model">The inventory transaction model</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create")]
        [HttpPost, Dispatch, Validate, ValidateAntiForgeryToken]
        public ActionResult Create(CreateInventoryItem request)
        {
            return this.Redirect(request.ReturnUrl);
        }

        #endregion

        #endregion
    }
}