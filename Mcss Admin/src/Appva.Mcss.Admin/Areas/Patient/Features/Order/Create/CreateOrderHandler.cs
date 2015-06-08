// <copyright file="CreateOrderHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.Controllers;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Mcss.Admin.Application.Auditing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateOrderHandler : RequestHandler<CreateOrder, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RefillOrderHandler"/> class.
        /// </summary>
        public CreateOrderHandler(IAuditService auditing, IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(CreateOrder message)
        {
            var sequence = this.persistence.Get<Sequence>(message.Id);
            if (sequence.RefillInfo.Refill)
            {
                sequence.RefillInfo.Ordered = true;
                sequence.RefillInfo.OrderedBy = null/*Identity()*/;
                sequence.RefillInfo.OrderedDate = DateTime.Now;
                this.auditing.Update(
                    sequence.Patient,
                    "beställde preparat till {0} ({1})",
                    sequence.Name, 
                    sequence.Id);
            }
            return true;
        }

        #endregion
    }
}