// <copyright file="FhirVersion.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// A version attribute for FHIR contracts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class FhirVersion : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirVersion"/> class.
        /// </summary>
        /// <param name="value">The version value</param>
        public FhirVersion(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The version value.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        #endregion
    }
}