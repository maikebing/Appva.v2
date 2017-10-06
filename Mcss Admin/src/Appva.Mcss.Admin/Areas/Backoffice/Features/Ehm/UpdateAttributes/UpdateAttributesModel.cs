// <copyright file="EhmSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateAttributesModel : IRequest<Parameterless<EhmSettingsModel>> 
    {
        #region Properties.

        public string Adress
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Zip
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }
        public string WorkplaceCode
        {
            get;
            set;
        }

        public string Workplace
        {
            get;
            set;
        }

        #endregion
    }
}