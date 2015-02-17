// <copyright file="SecurityEventObjectType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// Code for the participant object type being audited.
    /// <externalLink>
    ///     <linkText>1.15.2.1.166.1 SecurityEventObjectType</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/object-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SecurityEventObjectType
    {
        /// <summary>
        /// Person.
        /// </summary>
        public static readonly Code Person = new Code("1");

        /// <summary>
        /// System Object.
        /// </summary>
        public static readonly Code SystemObject = new Code("2");

        /// <summary>
        /// Organization.
        /// </summary>
        public static readonly Code Organization = new Code("3");

        /// <summary>
        /// Other.
        /// </summary>
        public static readonly Code Other = new Code("4");
    }
}