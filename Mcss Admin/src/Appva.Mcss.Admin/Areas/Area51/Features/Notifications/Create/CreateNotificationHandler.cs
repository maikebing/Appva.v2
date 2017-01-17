// <copyright file="CreateNotificationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Caching.Providers;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Features.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Application.Services;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateNotificationHandler : RequestHandler<Parameterless<CreateNotificationModel>,CreateNotificationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="INotificationService"/>
        /// </summary>
        private readonly INotificationService notificationService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNotificationHandler"/> class.
        /// </summary>
        public CreateNotificationHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateNotificationModel Handle(Parameterless<CreateNotificationModel> message)
        {
            return new CreateNotificationModel
            {
                Templates = this.notificationService.GetTemplates().Select(x => new SelectListItem { Text = x, Value = x }).ToList()
            };
        }

        #endregion
    }
}