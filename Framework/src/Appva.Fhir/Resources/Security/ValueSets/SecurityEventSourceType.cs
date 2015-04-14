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
    using Newtonsoft.Json;
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
    public sealed class SecurityEventSourceType : CodeableConcept
    {
        #region Variables.

        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("security-source-type");

        /// <summary>
        /// End-user display device, diagnostic device.
        /// </summary>
        public static readonly SecurityEventSourceType UserDevice = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("1"), "User Device")
            });

        /// <summary>
        /// Data acquisition device or instrument.
        /// </summary>
        public static readonly SecurityEventSourceType DataInterface = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("2"), "Data Interface")
            });

        /// <summary>
        /// Web Server process or thread.
        /// </summary>
        public static readonly SecurityEventSourceType WebServer = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("3"), "Web Server")
            });

        /// <summary>
        /// Application Server process or thread.
        /// </summary>
        public static readonly SecurityEventSourceType ApplicationServer = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("4"), "Application Server")
            });

        /// <summary>
        /// Database Server process or thread.
        /// </summary>
        public static readonly SecurityEventSourceType DatabaseServer = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("5"), "Database Server")
            });

        /// <summary>
        /// Security server, e.g., a domain controller.
        /// </summary>
        public static readonly SecurityEventSourceType SecurityServer = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("6"), "Security Server")
            });

        /// <summary>
        /// ISO level 1-3 network component.
        /// </summary>
        public static readonly SecurityEventSourceType NetworkDevice = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("7"), "Network Device")
            });

        /// <summary>
        /// ISO level 4-6 operating software.
        /// </summary>
        public static readonly SecurityEventSourceType NetworkRouter = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("8"), "Network Router")
            });

        /// <summary>
        /// Other kind of device (defined by DICOM, but some other code/system can be used).
        /// </summary>
        public static readonly SecurityEventSourceType Other = new SecurityEventSourceType(new Collection<Coding>
            {
                new Coding(System, new Code("9"), "Other")
            });

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventSourceType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public SecurityEventSourceType(Collection<Coding> coding)
            : base(null, coding)
        {
        }

        [JsonConstructor]
        public SecurityEventSourceType()
        {
        }

        #endregion
    }
}