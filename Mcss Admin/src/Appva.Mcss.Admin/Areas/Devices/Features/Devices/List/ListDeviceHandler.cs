// <copyright file="ListDeviceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.List
{
    using Admin.Models;
    #region Imports.

    using Application.Services;
    using Appva.Cqrs;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListDeviceHandler : RequestHandler<ListDevice, ListDeviceModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService service;

        /// <summary>
        /// The <see cref="IDeviceTransformer"/>
        /// </summary>
        private readonly IDeviceTransformer transformer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDeviceHandler"/> class.
        /// </summary>
        public ListDeviceHandler(IDeviceService service, IDeviceTransformer transformer)
        {
            this.service = service;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override ListDeviceModel Handle(ListDevice message)
        {
            var isActive = message.IsActive;
            var pageSize = 10;
            var pageIndex = message.Page ?? 1;
            var isAscending = message.IsAscending;
            var orderBy = string.IsNullOrEmpty(message.OrderBy) ? "CreatedAt" : message.OrderBy;
            var result = this.service.Search(
                new Domain.Models.SearchDeviceModel
                {
                    OrderBy = orderBy,
                    IsActive = isActive,
                    IsAscending = isAscending,
                    SearchQuery = message.SearchQuery                    
                }, 
                pageIndex, 
                pageSize);

            return new ListDeviceModel
            {
                OrderBy = orderBy,
                IsAscending = isAscending,
                IsActive = isActive,
                Items = this.transformer.ToDeviceList(result.Entities),
                PageNumber = (int)result.CurrentPage,
                PageSize = (int)result.PageSize,
                TotalItemCount = (int)result.TotalCount
            };
        }

        #endregion
    }
}