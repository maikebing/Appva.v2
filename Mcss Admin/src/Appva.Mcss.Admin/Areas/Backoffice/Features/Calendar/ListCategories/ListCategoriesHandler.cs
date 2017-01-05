// <copyright file="ListCategoriesHandler.cs" company="Appva AB">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListCategoriesHandler : RequestHandler<Parameterless<ListCategoriesModel>, ListCategoriesModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IEventService"/>
        /// </summary>
        private readonly IEventService eventService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCategoriesHandler"/> class.
        /// </summary>
        public ListCategoriesHandler(IEventService eventService)
        {
            this.eventService = eventService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListCategoriesModel Handle(Parameterless<ListCategoriesModel> message)
        {
            var categories = this.eventService.GetCategories();

            return new ListCategoriesModel
            {
                Categories = categories
            };
        }

        #endregion
    }
}