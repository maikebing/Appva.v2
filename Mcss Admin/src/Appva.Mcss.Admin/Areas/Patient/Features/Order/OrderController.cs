// <copyright file="OrderController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Order
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class OrderController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        public OrderController(IMediator mediator, IIdentityService identities, IAccountService accounts, IPersistenceContext context, ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region Routes.

        #region Overview Gadget

        /// <summary>
        /// Returns the dashboard widget.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        public PartialViewResult Overview()
        {
            Taxon filterTaxon = FilterCache.Get(this.context);
            if (!FilterCache.HasCache())
            {
                filterTaxon = FilterCache.GetOrSet(Identity(), this.context);
            }
            var orders = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .And(x => x.RefillInfo.Refill)
                .Fetch(x => x.RefillInfo.RefillOrderedBy).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            orders.JoinQueryOver<Patient>(x => x.Patient)
                .Where(x => x.IsActive)
                .And(x => !x.Deceased)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(filterTaxon.Id.ToString(), MatchMode.Anywhere));
            return PartialView(new OrderOverviewViewModel
            {
                Orders = orders.OrderBy(x => x.RefillInfo.RefillOrderedDate).Asc.List()
            });
        }

        #endregion

        #region Ajax.

        /// <summary>
        /// Refills the sequence and resets the refill indicator.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        public JsonResult Refill(Guid sequence)
        {
            var seq = this.context.Get<Sequence>(sequence);
            seq.RefillInfo.Refill = false;
            seq.RefillInfo.Ordered = false;
            this.logService.Info(string.Format("{0} fyllde på {1} ({2})", Identity().FullName, seq.Name, seq.Id), Identity(), seq.Patient);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Undo a refill and reverts the refill indicator.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        public JsonResult UndoRefill(Guid sequence)
        {
            var seq = this.context.Get<Sequence>(sequence);
            seq.RefillInfo.Refill = true;
            if (seq.RefillInfo.RefillOrderedDate.GetValueOrDefault() < seq.RefillInfo.OrderedDate.GetValueOrDefault())
            {
                seq.RefillInfo.Ordered = true;
            }
            this.logService.Info(string.Format("{0} ångrade påfyllning av {1} ({2})", Identity().FullName, seq.Name, seq.Id), Identity(), seq.Patient);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Makes an order (sets the indicator to ordered) for a
        /// sequence.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        public JsonResult Order(Guid sequence)
        {
            var seq = this.context.Get<Sequence>(sequence);
            if (seq.RefillInfo.Refill)
            {
                seq.RefillInfo.Ordered = true;
                seq.RefillInfo.OrderedBy = Identity();
                seq.RefillInfo.OrderedDate = DateTime.Now;
                this.logService.Info(string.Format("{0} beställde preparat till {1} ({2})", Identity().FullName, seq.Name, seq.Id), Identity(), seq.Patient);
            }
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Undo an order.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        public JsonResult UndoOrder(Guid sequence)
        {
            var seq = this.context.Get<Sequence>(sequence);
            seq.RefillInfo.Ordered = false;
            this.logService.Info(string.Format("{0} ångrade beställning av preparat till {1} ({2})", Identity().FullName, seq.Name, seq.Id), Identity(), seq.Patient);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        #endregion

        #endregion
    }
}