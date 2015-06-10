// <copyright file="ListMedication.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListMedication : IRequest<ListMedicationModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMedication"/> class.
        /// </summary>
        public ListMedication()
        {
        }

        #endregion

        public Guid Id
        {
            get;
            set;
        }
    }
}