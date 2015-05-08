// <copyright file="MaritalStatus.cs" company="Appva AB">
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
    /// This value set defines the set of codes that can be used to indicate the marital 
    /// status of a person.
    /// <externalLink>
    ///     <linkText>1.23.2.1.25.1 Marital Status Codes</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/valueset-marital-status.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class MaritalStatus : CodeableConcept
    {
        #region Variables.

        /// <summary>
        /// Include these codes as defined in http://hl7.org/fhir/v3/MaritalStatus.
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("v3/MaritalStatus");

        /// <summary>
        /// The person is not presently married. The marital history is not known or stated.
        /// </summary>
        public static readonly MaritalStatus Unmarried = new MaritalStatus(
            new Coding(System, new Code("U"), "Unmarried"));

        /// <summary>
        /// Marriage contract has been declared null and to not have existed.
        /// </summary>
        public static readonly MaritalStatus Annulled = new MaritalStatus(
            new Coding(System, new Code("A"), "Annulled"));

        /// <summary>
        /// Marriage contract has been declared dissolved and inactive.
        /// </summary>
        public static readonly MaritalStatus Divorced = new MaritalStatus(
            new Coding(System, new Code("D"), "Divorced"));

        /// <summary>
        /// Subject to an Interlocutory Decree.
        /// </summary>
        public static readonly MaritalStatus Interlocutory = new MaritalStatus(
            new Coding(System, new Code("I"), "Interlocutory"));

        /// <summary>
        /// Legally Separated.
        /// </summary>
        public static readonly MaritalStatus LegallySeparated = new MaritalStatus(
            new Coding(System, new Code("L"), "Legally Separated"));

        /// <summary>
        /// A current marriage contract is active.
        /// </summary>
        public static readonly MaritalStatus Married = new MaritalStatus(
            new Coding(System, new Code("M"), "Married"));

        /// <summary>
        /// More than 1 current spouse.
        /// </summary>
        public static readonly MaritalStatus Polygamous = new MaritalStatus(
            new Coding(System, new Code("P"), "Polygamous"));

        /// <summary>
        /// No marriage contract has ever been entered.
        /// </summary>
        public static readonly MaritalStatus NeverMarried = new MaritalStatus(
            new Coding(System, new Code("S"), "Never Married"));

        /// <summary>
        /// Person declares that a domestic partner relationship exists.
        /// </summary>
        public static readonly MaritalStatus DomesticPartner = new MaritalStatus(
            new Coding(System, new Code("T"), "Domestic Partner"));

        /// <summary>
        /// The spouse has died.
        /// </summary>
        public static readonly MaritalStatus Widowed = new MaritalStatus(
            new Coding(System, new Code("W"), "Widowed"));

        /// <summary>
        /// A proper value is applicable, but not known.
        /// Usage Notes: This means the actual value is not known. If the only thing that 
        /// is unknown is how to properly express the value in the necessary constraints 
        /// (value set, datatype, etc.), then the OTH or UNC flavor should be used. No 
        /// properties should be included for a datatype with this property unless:
        /// Those properties themselves directly translate to a semantic of "unknown". (E.g. 
        /// a local code sent as a translation that conveys 'unknown') Those properties 
        /// further qualify the nature of what is unknown. (E.g. specifying a use code of 
        /// "H" and a URL prefix of "tel:" to convey that it is the home phone number that 
        /// is unknown.).
        /// </summary>
        public static readonly MaritalStatus Unknown = new MaritalStatus(
            new Coding(System, new Code("UNK"), "Unknown"));

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MaritalStatus"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        private MaritalStatus(params Coding[] coding)
            : base(coding)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MaritalStatus" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private MaritalStatus()
            : base()
        {
        }

        #endregion
    }
}