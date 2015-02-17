// <copyright file="Fhir.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir
{
    #region Imports.

    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// Fhir constants.
    /// </summary>
    public static class Fhir
    {
        /// <summary>
        /// The FHIR version v0.4.0-3886.
        /// </summary>
        public const string V040 = "v0.4.0-3886";

        /// <summary>
        /// The FHIR namespace.
        /// </summary>
        public const string Namespace = "http://hl7.org/fhir";

        /// <summary>
        /// The FHIR value set namespace.
        /// </summary>
        public const string ValueSetNamespace = "http://hl7.org/fhir/vs";

        /// <summary>
        /// Creates a new Uri with the default namespace.
        /// </summary>
        /// <param name="name">The namespace name</param>
        /// <returns>A new instance of <c>Uri</c></returns>
        public static Uri CreateNewUri(string name)
        {
            return new Uri(Namespace + "/" + name);
        }
    }
}