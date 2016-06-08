// <copyright file="PatientInformation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf.Prescriptions
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientInformation
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientInformation"/> class.
        /// </summary>
        /// <param name="name">The patient name</param>
        /// <param name="personalIdentityNumber">The personal identity number</param>
        public PatientInformation(string name, PersonalIdentityNumber personalIdentityNumber)
        {
            this.Name = name;
            this.PersonalIdentityNumber = personalIdentityNumber;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The patient name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The patient personal identity number.
        /// </summary>
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            private set;
        }

        #endregion
    }
}