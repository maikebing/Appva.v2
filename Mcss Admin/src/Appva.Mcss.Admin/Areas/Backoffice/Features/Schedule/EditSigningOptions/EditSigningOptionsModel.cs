// <copyright file="EditSigningOptionsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;
using Appva.Mcss.Admin.Models;
using Appva.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditSigningOptionsModel : IRequest<Identity<DetailsScheduleModel>>
    {
        #region Properties.

        /// <summary>
        /// The schedulesettings id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The options
        /// </summary>
        public IList<Tickable> Options
        {
            get;
            set;
        }

        #endregion
    }
}