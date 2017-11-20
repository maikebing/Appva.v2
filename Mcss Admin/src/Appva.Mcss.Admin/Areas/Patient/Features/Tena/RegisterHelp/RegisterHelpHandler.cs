// <copyright file="RegisterHelpHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Configuration;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class RegisterHelpHandler : RequestHandler<Parameterless<RegisterHelpModel>, RegisterHelpModel>
    {
        #region RequestHandler overrides.

        /// <inheritdoc />
        public override RegisterHelpModel Handle(Parameterless<RegisterHelpModel> message)
        {
            return new RegisterHelpModel
            {
                TenaUrl = TenaIdentifiConfiguration.ServerUrl
            };
        }

        #endregion
    }
}