// <copyright file="SecurityEventObjectSensitivity.cs" company="Appva AB">
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
    /// The sensitivity of an object in a secuity event resource. May also encompass 
    /// confidentiality and rudimentary access control.
    /// <externalLink>
    ///     <linkText>1.15.2.1.222.1 SecurityEventObjectSensitivity</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/valueset-security-event-sensitivity.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SecurityEventObjectSensitivity
    {
        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("v3/vs/Confidentiality");

        /// <summary>
        /// A specializable code and its leaf codes used in Confidentiality value sets to 
        /// value the Act.Confidentiality and Role.Confidentiality attribute in accordance 
        /// with the definition for concept domain "Confidentiality".
        /// </summary>
        public static readonly CodeableConcept Confidentiality = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("_Confidentiality"), "Confidentiality")
            });

        /// <summary>
        /// Definition: Privacy metadata indicating that the information has been 
        /// de-identified, and there are mitigating circumstances that prevent 
        /// re-identification, which minimize risk of harm from unauthorized disclosure. The 
        /// information requires protection to maintain low sensitivity. Examples: Includes 
        /// anonymized, pseudonymized, or non-personally identifiable information such as 
        /// HIPAA limited data sets. 
        /// <para>
        /// Map: No clear map to ISO 13606-4 Sensitivity Level (1) Care Management: 
        /// RECORD_COMPONENTs that might need to be accessed by a wide range of 
        /// administrative staff to manage the subject of care's access to health services.
        /// </para>
        /// <para>
        /// Usage Note: This metadata indicates the receiver may have an obligation to comply 
        /// with a data use agreement.
        /// </para>
        /// </summary>
        public static readonly CodeableConcept Low = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("L"), "low")
            });

        /// <summary>
        /// Definition: Privacy metadata indicating moderately sensitive information, which 
        /// presents moderate risk of harm if disclosed without authorization.
        /// <para>
        /// Examples: Includes allergies of non-sensitive nature used inform food service; 
        /// health information a patient authorizes to be used for marketing, released to a 
        /// bank for a health credit card or savings account; or information in personal 
        /// health record systems that are not governed under health privacy laws.
        /// Map: Partial Map to ISO 13606-4 Sensitivity Level (2) Clinical Management: Less 
        /// sensitive RECORD_COMPONENTs that might need to be accessed by a wider range of 
        /// personnel not all of whom are actively caring for the patient (e.g. radiology 
        /// staff).
        /// </para>
        /// <para>
        /// Usage Note: This metadata indicates that the receiver may be obligated to comply 
        /// with the receiver's terms of use or privacy policies.
        /// </para>
        /// </summary>
        public static readonly CodeableConcept Moderate = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("M"), "moderate")
            });

        /// <summary>
        /// Definition: Privacy metadata indicating that the information is typical, 
        /// non-stigmatizing health information, which presents typical risk of harm if 
        /// disclosed without authorization.
        /// <para>
        /// Examples: In the US, this includes what HIPAA identifies as the minimum 
        /// necessary protected health information (PHI) given a covered purpose of use 
        /// (treatment, payment, or operations). Includes typical, non-stigmatizing health 
        /// information disclosed in an application for health, workers compensation, 
        /// disability, or life insurance.
        /// </para>
        /// <para>
        /// Map: Partial Map to ISO 13606-4 Sensitivity Level (3) Clinical Care: Default for 
        /// normal clinical care access (i.e. most clinical staff directly caring for the 
        /// patient should be able to access nearly all of the EHR). Maps to normal 
        /// confidentiality for treatment information but not to ancillary care, payment and 
        /// operations.
        /// </para>
        /// <para>
        /// Usage Note: This metadata indicates that the receiver may be obligated to comply 
        /// with applicable jurisdictional privacy law or disclosure authorization.
        /// </para>
        /// </summary>
        public static readonly CodeableConcept Normal = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("N"), "normal")
            });

        /// <summary>
        /// Privacy metadata indicating highly sensitive, potentially stigmatizing 
        /// information, which presents a high risk to the information subject if disclosed 
        /// without authorization. May be preempted by jurisdictional law, e.g., for public 
        /// health reporting or emergency treatment.
        /// <para>
        /// Examples: In the US, this includes what HIPAA identifies as the minimum 
        /// necessary protected health information (PHI) given a covered purpose of use 
        /// (treatment, payment, or operations). Includes typical, non-stigmatizing health 
        /// information disclosed in an application for health, workers compensation, 
        /// disability, or life insurance.
        /// </para>
        /// <para>
        /// Map: Partial Map to ISO 13606-4 Sensitivity Level (3) Clinical Care: Default for 
        /// normal clinical care access (i.e. most clinical staff directly caring for the 
        /// patient should be able to access nearly all of the EHR). Maps to normal 
        /// confidentiality for treatment information but not to ancillary care, payment and 
        /// operations.
        /// </para>
        /// <para>
        /// Usage Note: This metadata indicates that the receiver may be obligated to comply 
        /// with applicable, prevailing (default) jurisdictional privacy law or disclosure 
        /// authorization.
        /// </para>
        /// </summary>
        public static readonly CodeableConcept Restricted = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("R"), "restricted")
            });

        /// <summary>
        /// Definition: Privacy metadata indicating that the information is not classified 
        /// as sensitive. 
        /// <para>
        /// Examples: Includes publicly available information, e.g., business name, phone, 
        /// email or physical address.
        /// </para>
        /// <para>
        /// Usage Note: This metadata indicates that the receiver has no obligation to 
        /// consider additional policies when making access control decisions. Note that in 
        /// some jurisdictions, personally identifiable information must be protected as 
        /// confidential, so it would not be appropriate to assign a confidentiality code of 
        /// "unrestricted" to that information even if it is publicly available.
        /// </para>
        /// </summary>
        public static readonly CodeableConcept Unrestricted = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("U"), "unrestricted")
            });

        /// <summary>
        /// Privacy metadata indicating that the information is extremely sensitive and 
        /// likely stigmatizing health information that presents a very high risk if 
        /// disclosed without authorization. This information must be kept in the highest 
        /// confidence. 
        /// <para>
        /// Examples: Includes information about a victim of abuse, patient requested 
        /// information sensitivity, and taboo subjects relating to health status that must 
        /// be discussed with the patient by an attending provider before sharing with the 
        /// patient. May also include information held under legal lock? or attorney-client 
        /// privilege.
        /// </para>
        /// <para>
        /// Map: This metadata indicates that the receiver may not disclose this information 
        /// except as directed by the information custodian, who may be the information subject.
        /// </para>
        /// <para>
        /// Usage Note: This metadata indicates that the receiver may not disclose this 
        /// information except as directed by the information custodian, who may be the 
        /// information subject.
        /// </para>
        /// </summary>
        public static readonly CodeableConcept VeryRestricted = new CodeableConcept(new Collection<Coding>
            {
                new Coding(System, new Code("V"), "very restricted")
            });
    }
}