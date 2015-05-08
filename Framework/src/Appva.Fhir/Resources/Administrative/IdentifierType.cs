// <copyright file="IdentifierType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Complex;
    using Primitives;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A coded type for an identifier that can be used to determine which identifier to 
    /// use for a specific purpose.
    /// <externalLink>
    ///     <linkText>1.23.2.1.2114.1 Identifier Type Codes</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/valueset-identifier-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class IdentifierType : CodeableConcept
    {
        #region Variables.

        /// <summary>
        /// Include these codes as defined in http://hl7.org/fhir/v2/0203.
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("v2/0203");

        /// <summary>
        /// Driver's license number.
        /// </summary>
        public static readonly IdentifierType DriversLicenseNumber = new IdentifierType(
            new Coding(System, new Code("DL"), "Driver's license number"));

        /// <summary>
        /// Passport number.
        /// </summary>
        public static readonly IdentifierType PassportNumber = new IdentifierType(
            new Coding(System, new Code("PPN"), "Passport number"));

        /// <summary>
        /// Breed Registry Number.
        /// </summary>
        public static readonly IdentifierType BreedRegistryNumber = new IdentifierType(
            new Coding(System, new Code("BRN"), "Breed Registry Number"));

        /// <summary>
        /// Medical record number.
        /// </summary>
        public static readonly IdentifierType MedicalRecordNumber = new IdentifierType(
            new Coding(System, new Code("MR"), "Medical record number"));

        /// <summary>
        /// Microchip Number.
        /// </summary>
        public static readonly IdentifierType MicrochipNumber = new IdentifierType(
            new Coding(System, new Code("MCN"), "Microchip Number"));

        /// <summary>
        /// Employer number.
        /// </summary>
        public static readonly IdentifierType EmployerNumber = new IdentifierType(
            new Coding(System, new Code("EN"), "Employer number"));

        /// <summary>
        /// Social Security number.
        /// </summary>
        public static readonly IdentifierType SocialSecurityNumber = new IdentifierType(
            new Coding(System, new Code("SS"), "Social Security number"));

        /// <summary>
        /// Tax ID number.
        /// </summary>
        public static readonly IdentifierType TaxIdNumber = new IdentifierType(
            new Coding(System, new Code("TAX"), "Tax ID number"));

        /// <summary>
        /// Provider number.
        /// </summary>
        public static readonly IdentifierType ProviderNumber = new IdentifierType(
            new Coding(System, new Code("PRN"), "Provider number"));

        /// <summary>
        /// Medical License number.
        /// </summary>
        public static readonly IdentifierType MedicalLicenseNumber = new IdentifierType(
            new Coding(System, new Code("MD"), "Medical License number"));

        /// <summary>
        /// Universal Device Identifier.
        /// </summary>
        public static readonly IdentifierType UniversalDeviceIdentifier = new IdentifierType(
            new Coding(System, new Code("UDI"), "Universal Device Identifier"));

        /// <summary>
        /// Serial Number.
        /// </summary>
        public static readonly IdentifierType SerialNumber = new IdentifierType(
            new Coding(System, new Code("SNO"), "Serial Number"));

        /// <summary>
        /// Donor Registration Number.
        /// </summary>
        public static readonly IdentifierType DonorRegistrationNumber = new IdentifierType(
            new Coding(System, new Code("DR"), "Donor Registration Number"));

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        private IdentifierType(params Coding[] coding)
            : base(coding)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="IdentifierType" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private IdentifierType()
            : base()
        {
        }

        #endregion
    }
}