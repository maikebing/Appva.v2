// <copyright file="PrescriptionList.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf.Prescriptions
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PrescriptionList
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionList"/> class.
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="period">The period</param>
        /// <param name="patient">The patient information</param>
        /// <param name="prescriptions">The prescriptions</param>
        /// <param name="symbols">The symbols</param>
        /// <param name="signatures">The signatures</param>
        public PrescriptionList(
            string title,
            Period period,
            PatientInformation patient,
            IList<Prescription> prescriptions = null,
            IList<Symbol> symbols = null,
            IList<Signature> signatures = null)
        {
            this.Title = title;
            this.Period = period;
            this.Patient = patient;
            this.Prescriptions = prescriptions ?? new List<Prescription>();
            this.Signatures = signatures ?? new List<Signature>();
            this.Symbols = symbols ?? new List<Symbol>();
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the title.
        /// </summary>
        public string Title
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the period.
        /// </summary>
        public Period Period
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the patient information.
        /// </summary>
        public PatientInformation Patient
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the prescriptions.
        /// </summary>
        public IList<Prescription> Prescriptions
        {
            get;
            private set;
        }

        /// <summary>
        /// The symbols.
        /// </summary>
        public IList<Symbol> Symbols
        {
            get;
            private set;
        }

        /// <summary>
        /// The signatures.
        /// </summary>
        public IList<Signature> Signatures
        {
            get;
            private set;
        }

        #endregion
    }
}