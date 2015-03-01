// <copyright file="Dicom.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir
{
    #region Imports.

    using Primitives;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class Dicom
    {
        /// <summary>
        /// The DICOM namespace.
        /// </summary>
        public static readonly string Namespace = "http://nema.org/dicom/dcid";

        /// <summary>
        /// The DICOM code system.
        /// </summary>
        public static readonly Uri System = new Uri(Namespace);
    }
}