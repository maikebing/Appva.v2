// <copyright file="UpdateAttributesFormHandler.cs" company="Appva AB">
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
    internal sealed class UpdateAttributesFormHandler : RequestHandler<UpdateAttributesModel, Parameterless<EhmSettingsModel>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttributesFormHandler"/> class.
        /// </summary>
        public UpdateAttributesFormHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override Parameterless<EhmSettingsModel> Handle(UpdateAttributesModel message)
        {
            
            var attr = new TenantAttributes
            {
                Adress          = message.Adress,
                City            = message.City,
                Phone           = message.Phone,
                Workplace       = message.Workplace,
                WorkplaceCode   = message.WorkplaceCode,
                Zip             = message.Zip,
                OrganizationId  = message.OrganizationId
            };
            this.settings.Upsert<TenantAttributes>(ApplicationSettings.EhmTenantUserAttributes, attr);

            return new Parameterless<EhmSettingsModel>();
        }

        #endregion

        
    }
}