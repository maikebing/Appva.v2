// <copyright file="MedicationAdministration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Models;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    public class MedicationAdministration : Administration
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationAdministration"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sequence"><see cref="Sequence"/>.</param>
        /// <param name="valueModel"><see cref="AdministrationValueModel"/>.</param>
        public MedicationAdministration(Sequence sequence, AdministrationValueModel valueModel)
            : base(sequence, valueModel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationAdministration"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal MedicationAdministration()
        {
        }

        #endregion

        #region Administration Overrides.

        /// <inheritdoc />
        protected override ArbitraryValue NewValue(double value)
        {
            Requires.ValidState(this.Validate(value), "value"); //// TODO: Insert validation.
            return ArbitraryValue.New(value, LevelOfMeasurement.Quantitative, this.CustomValues.Unit);
        }

        #endregion

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Validate(double value)
        {
            return this.CustomValues.Validate(value);
        }
    }
}
