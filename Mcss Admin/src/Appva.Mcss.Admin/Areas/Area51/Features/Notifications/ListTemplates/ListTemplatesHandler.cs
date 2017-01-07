// <copyright file="ListTemplatesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTemplatesHandler : RequestHandler<Parameterless<ListTemplatesModel>, ListTemplatesModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="INotificationService"/>.
        /// </summary>
        private readonly INotificationService notificationService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTemplatesHandler"/> class.
        /// </summary>
        public ListTemplatesHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListTemplatesModel Handle(Parameterless<ListTemplatesModel> message)
        {
            return new ListTemplatesModel
            {
                Templates = this.notificationService.GetTemplates()
            };
        }

        #endregion
    }
}