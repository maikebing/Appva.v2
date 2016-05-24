// <copyright file="ListLogHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Log.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Log.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListLogHandler : RequestHandler<ListLog, ListLogModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ILogService"/>
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListLogHandler"/> class.
        /// </summary>
        public ListLogHandler(ILogService logService, ITaxonomyService taxonomyService)
        {
            this.logService = logService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListLogModel Handle(ListLog message)
        {
            var list = this.logService.List(message.Cursor, message.Page);
            var rootTaxon = this.taxonomyService.Roots(TaxonomicSchema.Organization).FirstOrDefault();
            return new ListLogModel
            {
                Logs = list.Entities,
                Page = (int)list.CurrentPage,
                PageSize = (int)list.PageSize,
                TotalCount = (int)list.TotalCount,
                TenantName = rootTaxon.Name
            };
        }

        #endregion
    }
}