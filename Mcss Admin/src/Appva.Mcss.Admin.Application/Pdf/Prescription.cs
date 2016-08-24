// <copyright file="Prescription.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf.Prescriptions
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Prescription
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Prescription"/> class.
        /// </summary>
        /// <param name="name">The prescription name</param>
        /// <param name="reference">The prescription reference</param>
        /// <param name="time">The scheduled time</param>
        /// <param name="days">The scheduled days</param>
        /// <param name="symbols">The symbols</param>
        public Prescription(string name, References reference, DateTimeOffset? time, IList<int> days = null, IDictionary<int, string> symbols = null)
        {
            this.Name      = name;
            this.Reference = reference;
            this.Time      = time;
            this.Days      = days ?? new List<int>();
            this.Symbols   = symbols ?? new Dictionary<int, string>();
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the reference.
        /// </summary>
        public References Reference
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the date time offset.
        /// </summary>
        public DateTimeOffset? Time
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the precribed days.
        /// </summary>
        public IList<int> Days
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the symbols, e.g. signature per day.
        /// </summary>
        public IDictionary<int, string> Symbols
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="Prescription"/> class.
        /// </summary>
        /// <param name="name">The prescription name</param>
        /// <param name="reference">The prescription reference</param>
        /// <param name="time">The scheduled time</param>
        /// <param name="days">The scheduled days</param>
        /// <param name="symbols">The symbols</param>
        /// <returns>A new <see cref="Prescription"/> instance</returns>
        public static Prescription CreateNew(string name, References reference, DateTimeOffset? time, IList<int> days = null, IDictionary<int, string> symbols = null)
        {
            return new Prescription(name, reference, time, days, symbols);
        }

        #endregion
    }
}