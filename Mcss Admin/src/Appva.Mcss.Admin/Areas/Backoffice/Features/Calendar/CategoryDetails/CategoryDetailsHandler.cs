// <copyright file="CategoryDetailsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CategoryDetailsHandler : RequestHandler<Identity<CategoryDetailsModel>, CategoryDetailsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IEventService"/>
        /// </summary>
        private readonly IEventService eventService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDetailsHandler"/> class.
        /// </summary>
        public CategoryDetailsHandler(IEventService eventService)
        {
            this.eventService = eventService;            
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override CategoryDetailsModel Handle(Identity<CategoryDetailsModel> message)
        {
            //var category = eventService.CreateCategory();
            return new CategoryDetailsModel();
        }

        #endregion
    }
}