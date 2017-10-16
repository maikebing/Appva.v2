// <copyright file="CreateMedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateMedicationModel : CreateOrUpdateSequence, IAsyncRequest<DetailsMedicationRequest>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMedicationModel"/> class.
        /// </summary>
        public CreateMedicationModel()
            : base()
        {
        }

        #endregion

        #region Properties 
        
        /// <summary>
        /// Gets or sets the ordination identifier.
        /// </summary>
        /// <value>
        /// The ordination identifier.
        /// </value>
        public long OrdinationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get;
            set;
        }

        #endregion
    }
}