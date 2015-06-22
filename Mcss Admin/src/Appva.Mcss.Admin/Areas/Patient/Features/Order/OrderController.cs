// <copyright file="OrderController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Order
{
    #region Imports.

    using System.Web.Mvc;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Models;
using Appva.Mvc;
using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize]
    [RouteArea("patient"), RoutePrefix("order")]
    public sealed class OrderController : Controller
    {
        #region Routes.

        #region Overview Gadget

        /// <summary>
        /// Returns the dashboard widget.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("overview")]
        [HttpGet, Dispatch(typeof(OverviewOrder))]
        [PermissionsAttribute(Permissions.Dashboard.ReadOrderRefillValue)]
        public PartialViewResult Overview()
        {
            return this.PartialView();
        }

        #endregion

        #region Ajax.

        /// <summary>
        /// Refills the sequence and resets the refill indicator.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("refill")]
        [HttpPost, /*Validate, ValidateAntiForgeryToken,*/ Dispatch]
        public DispatchJsonResult Refill(RefillOrder request)
        {
            return this.JsonPost();
        }

        /// <summary>
        /// Undo a refill and reverts the refill indicator.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("refill/undo")]
        [HttpPost, /*Validate, ValidateAntiForgeryToken,*/ Dispatch]
        public DispatchJsonResult UndoRefill(UndoRefillOrder request)
        {
            return this.JsonPost();
        }

        /// <summary>
        /// Makes an order (sets the indicator to ordered) for a
        /// sequence.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("create")]
        [HttpPost, /*Validate, ValidateAntiForgeryToken,*/ Dispatch]
        public DispatchJsonResult Create(CreateOrder request)
        {
            return this.JsonPost();
        }

        /// <summary>
        /// Undo an order.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("undo")]
        [HttpPost, /*Validate, ValidateAntiForgeryToken,*/ Dispatch]
        public DispatchJsonResult Undo(UndoOrder request)
        {
            return this.JsonPost();
        }

        #endregion

        #endregion
    }
}