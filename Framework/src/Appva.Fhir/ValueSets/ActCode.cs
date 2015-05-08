// <copyright file="ActCode.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.ValueSets
{
    #region Imports.

    using System.Collections.Generic;
    using Complex;
    using Primitives;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A code specifying the particular kind of Act that the Act-instance represents 
    /// within its class.
    /// <externalLink>
    ///     <linkText>1.23.4.1.4 v3 Code System ActCode</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/v3/ActCode/
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class ActCode : CodeableConcept, ISecurityLabel
    {
        #region Variables.

        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("v3/ActCode");

        /// <summary>
        /// Policy for handling information related to a celebrity (people of public 
        /// interest (VIP), which will be afforded heightened confidentiality. Celebrities 
        /// are people of public interest (VIP) about whose information an enterprise may 
        /// have a policy that requires heightened confidentiality. Information deemed 
        /// sensitive may include health information and patient role information including 
        /// patient status, demographics, next of kin, and location.
        /// Usage Note: For use within an enterprise in which the information subject is 
        /// deemed a celebrity or very important person. If there is a jurisdictional 
        /// mandate, then use the applicable ActPrivacyLaw code system, and specify the law 
        /// rather than or in addition to this more generic code.
        /// Use on any resource to indicate that the subject/patient is a celebrity or well 
        /// known to the staff in the institution. 
        /// Notes:
        /// This may be applied to the Patient resource, with implied behavior for the 
        /// entire patient compartment, or it may be applied to individual resources. 
        /// Resources affected by this label are more likely to be the subject of active 
        /// audit maintenance or additional security policy
        /// </summary>
        public static readonly ISecurityLabel CelebrityInformationSensitivityPolicy = new ActCode(new Coding(System, new Code("CEL"), "Celebrity information sensitivity"));

        /// <summary>
        /// Policy for handling information related to an employee, which will be afforded 
        /// heightened confidentiality. When a patient is an employee, an enterprise may 
        /// have a policy that requires heightened confidentiality. Information deemed 
        /// sensitive typically includes health information and patient role information 
        /// including patient status, demographics, next of kin, and location.
        /// Description: When a patient is an employee, an enterprise may have a policy that 
        /// requires heightened confidentiality. Information deemed sensitive typically 
        /// includes health information and patient role information including patient 
        /// status, demographics, next of kin, and location.
        /// Use on a Patient resource and resources with a subject of that patient to 
        /// indicate that the patient is a staff member of the institution. This is a 
        /// variation on being a celebrity. 
        /// Notes:
        /// This may be applied to the Patient resource, with implied behavior for the entire 
        /// patient compartment, or it may be applied to individual resources.
        /// Resources affected by this label are (even) more likely to be the subject of 
        /// active audit maintenance or additional security policy.
        /// </summary>
        public static readonly ISecurityLabel EmployeeInformationSensitivity = new ActCode(new Coding(System, new Code("EMP"), "Employee information sensitivity"));

        /// <summary>
        /// Policy for handling information not to be initially disclosed or discussed with 
        /// patient except by a physician assigned to patient in this case. Information 
        /// handling protocols based on organizational policies related to sensitive patient 
        /// information that must be initially discussed with the patient by an attending 
        /// physician before being disclosed to the patient.
        /// Usage Note: If there is a jurisdictional mandate, then use the applicable 
        /// ActPrivacyLaw code system, and specify the law rather than or in addition to 
        /// this more generic code.
        /// Open Issue: This definition conflates a rule and a characteristic, and there may 
        /// be a similar issue with ts sibling codes.
        /// </summary>
        public static readonly ISecurityLabel Taboo = new ActCode(new Coding(System, new Code("TBOO"), "Taboo"));

        /// <summary>
        /// Policy for handling all demographic information about an information subject, 
        /// which will be afforded heightened confidentiality. Policies may govern 
        /// sensitivity of information related to all demographic about an information 
        /// subject, the disclosure of which could impact the privacy, well-being, or safety 
        /// of that subject.
        /// Usage Note: If there is a jurisdictional mandate, then use the applicable 
        /// ActPrivacyLaw code system, and specify the law rather than or in addition to 
        /// this more generic code.
        /// Used on a Patient resource to indicate that the patient's address and contact 
        /// details (phone numbers, email addresses) - including employment information - 
        /// are sensitive and shouldn't be shared with the patient's family or others without 
        /// specific authorization.
        /// </summary>
        public static readonly ISecurityLabel AllDemographicInformationSensitivity = new ActCode(new Coding(System, new Code("DEMO"), "All demographic information sensitivity"));

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActCode"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public ActCode(IList<Coding> coding)
            : base(null, coding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActCode"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public ActCode(params Coding[] coding)
            : base(null, coding)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ActCode" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private ActCode()
            : base()
        {
        }

        #endregion
    }
}