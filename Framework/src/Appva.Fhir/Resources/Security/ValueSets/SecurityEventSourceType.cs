// <copyright file="SecurityEventSourceType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using System.Collections.ObjectModel;
    using Complex;
    using Primitives;

    #endregion

    /// <summary>
    /// A <c>SecurityEventSourceType</c>. 
    /// <externalLink>
    ///     <linkText>1.15.2.1.165.1 SecurityEventSourceType</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/valueset-security-source-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SecurityEventSourceType
    {
        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("security-source-type");

        /// <summary>
        /// End-user display device, diagnostic device.
        /// </summary>
        public static readonly CodeableConcept UserDevice = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("1"), "User Device")
            });

        /// <summary>
        /// Data acquisition device or instrument.
        /// </summary>
        public static readonly CodeableConcept DataInterface = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("2"), "Data Interface")
            });

        /// <summary>
        /// Web Server process or thread.
        /// </summary>
        public static readonly CodeableConcept WebServer = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("3"), "Web Server")
            });

        /// <summary>
        /// Application Server process or thread.
        /// </summary>
        public static readonly CodeableConcept ApplicationServer = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("4"), "Application Server")
            });

        /// <summary>
        /// Database Server process or thread.
        /// </summary>
        public static readonly CodeableConcept DatabaseServer = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("5"), "Database Server")
            });

        /// <summary>
        /// Security server, e.g., a domain controller.
        /// </summary>
        public static readonly CodeableConcept SecurityServer = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("6"), "Security Server")
            });

        /// <summary>
        /// ISO level 1-3 network component.
        /// </summary>
        public static readonly CodeableConcept NetworkDevice = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("7"), "Network Device")
            });

        /// <summary>
        /// ISO level 4-6 operating software.
        /// </summary>
        public static readonly CodeableConcept NetworkRouter = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("8"), "Network Router")
            });

        /// <summary>
        /// Other kind of device (defined by DICOM, but some other code/system can be used).
        /// </summary>
        public static readonly CodeableConcept Other = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("9"), "Other")
            });
    }
}