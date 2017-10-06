// <copyright file="UpdateAttributesRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Mock;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateAttributesRequestHandler : RequestHandler<Parameterless<UpdateAttributesModel>, UpdateAttributesModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttributesRequestHandler"/> class.
        /// </summary>
        public UpdateAttributesRequestHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateAttributesModel Handle(Parameterless<UpdateAttributesModel> message)
        {
            var attr = this.settings.Find<TenantAttributes>(ApplicationSettings.EhmTenantUserAttributes);
            return new UpdateAttributesModel
            {
                Adress = attr.Adress,
                City = attr.City,
                Phone = attr.Phone,
                Workplace = attr.Workplace,
                WorkplaceCode = attr.WorkplaceCode,
                Zip = attr.Zip
            };
        }

        #endregion

        
    }
}