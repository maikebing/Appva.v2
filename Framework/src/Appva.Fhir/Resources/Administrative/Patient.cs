// <copyright file="Patient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// Demographics and other administrative information about a person or animal 
    /// receiving care or other health-related services.
    /// <para>
    /// This Resource covers data about persons and animals involved in a wide range of 
    /// health-related activities, including:
    /// <list type="bullet">
    ///     <item>Curative activities</item>
    ///     <item>Psychiatric care</item>
    ///     <item>Social services</item>
    ///     <item>Pregnancy care</item>
    ///     <item>Nursing and assisted living</item>
    ///     <item>Dietary services</item>
    ///     <item>Tracking of personal health and exercise data</item>
    /// </list>
    /// </para>
    /// <para>
    /// The data in the Resource covers the "who" information about the patient: its 
    /// attributes are focused on the demographic information necessary to support the 
    /// administrative, financial and logistic procedures. A Patient record is generally 
    /// created and maintained by each organization providing care for a patient. A 
    /// person or animal receiving care at multiple organizations may therefore have its 
    /// information present in multiple Patient Resources.
    /// </para>
    /// <externalLink>
    ///     <linkText>5.1 Patient</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/patient.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Patient : DomainResource
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /// <param name="name">Temp</param>
        public Patient(string name)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Patient" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Patient()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// An identifier for the person as this patient.
        /// <externalLink>
        ///     <linkText>Identifier</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.identifier
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public IList<Identifier> Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// A name associated with the individual.
        /// <externalLink>
        ///     <linkText>Name</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.name
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Person may have multiple names with different uses or applicable periods. For 
        /// animals, the name is a "HumanName" in the sense that is assigned and used by 
        /// humans and has the same patterns.
        /// </remarks>
        [ProtoMember(2), JsonProperty]
        public HumanName Name
        {
            get;
            private set;
        }

        /// <summary>
        /// A contact detail (e.g. a telephone number or an email address) by which the 
        /// individual may be contacted.
        /// <externalLink>
        ///     <linkText>Telecom</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.telecom
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Person may have multiple ways to be contacted with different uses or applicable 
        /// periods. May need to have options for contacting the person urgently and also to 
        /// help with identification. The address may not go directly to the individual, but 
        /// may reach another party that is able to proxy for the patient (i.e. home phone, 
        /// or pet owner's phone).
        /// </remarks>
        [ProtoMember(3), JsonProperty]
        public ContactPoint Telecom
        {
            get;
            private set;
        }

        /// <summary>
        /// Administrative Gender - the gender that the patient is considered to have for 
        /// administration and record keeping purposes.
        /// <externalLink>
        ///     <linkText>Gender</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.gender
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// The gender may not match the biological sex as determined by genetics, or the 
        /// individual's preferred identification. Note that for both humans and 
        /// particularly animals, there are other legitimate possibilities than M and F, 
        /// though the vast majority of systems and contexts only support M and F.
        /// </remarks>
        [ProtoMember(4), JsonProperty]
        public AdministrativeGender Gender
        {
            get;
            private set;
        }

        /// <summary>
        /// The date and time of birth for the individual.
        /// <externalLink>
        ///     <linkText>BirthDate</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.birthDate
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// At least an estimated year should be provided as a guess if the real dob is 
        /// unknown.
        /// </remarks>
        [ProtoMember(5), JsonProperty]
        public DateTime? BirthDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Addresses for the individual.
        /// <externalLink>
        ///     <linkText>Address</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.address
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Patient may have multiple addresses with different uses or applicable periods.
        /// </remarks>
        [ProtoMember(6), JsonProperty]
        public IList<Address> Addresses
        {
            get;
            private set;
        }

        /// <summary>
        /// This field contains a patient's most recent marital (civil) status.
        /// <externalLink>
        ///     <linkText>MaritalStatus</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/patient-definitions.html#Patient.maritalStatus
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(7), JsonProperty]
        public MaritalStatus MaritalStatus
        {
            get;
            private set;
        }

        #endregion
    }
}