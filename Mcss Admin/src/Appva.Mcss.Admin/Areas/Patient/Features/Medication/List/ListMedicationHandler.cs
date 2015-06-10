// <copyright file="ListMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListMedicationHandler : RequestHandler<ListMedication,ListMedicationModel>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMedicationHandler"/> class.
        /// </summary>
        public ListMedicationHandler(IPersistenceContext persistence, IPatientTransformer transformer)
        {
            this.persistence = persistence;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler overrides

        public override ListMedicationModel Handle(ListMedication message)
        {
            return new ListMedicationModel
            {
                Patient = this.transformer.ToPatient(this.persistence.Get<Patient>(message.Id))    
            };
        }

        #endregion
    }
}