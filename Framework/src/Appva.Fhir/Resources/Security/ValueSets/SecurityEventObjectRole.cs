// <copyright file="SecurityEventObjectRole.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using System;
    using Primitives;

    #endregion

    /// <summary>
    /// Code representing the functional application role of Participant Object being 
    /// audited.
    /// <externalLink>
    ///     <linkText>1.15.2.1.167.1 SecurityEventObjectRole</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/object-role.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventObjectRole : Code
    {
        #region Variables.

        /// <summary>
        /// This object is the patient that is the subject of care related to this event. It
        /// is identifiable by patient ID or equivalent. The patient may be either human or 
        /// animal.
        /// </summary>
        public static readonly SecurityEventObjectRole Patient = new SecurityEventObjectRole("1");

        /// <summary>
        /// This is a location identified as related to the event. This is usually the 
        /// location where the event took place. Note that for shipping, the usual events 
        /// are arrival at a location or departure from a location.
        /// </summary>
        public static readonly SecurityEventObjectRole Location = new SecurityEventObjectRole("2");

        /// <summary>
        /// This object is any kind of persistent document created as a result of the event. 
        /// This could be a paper report, film, electronic report, DICOM Study, etc. Issues 
        /// related to medical records life cycle management are conveyed elsewhere.
        /// </summary>
        public static readonly SecurityEventObjectRole Report = new SecurityEventObjectRole("3");

        /// <summary>
        /// A logical object related to the event. (Deprecated).
        /// </summary>
        public static readonly SecurityEventObjectRole DomainResource = new SecurityEventObjectRole("4");

        /// <summary>
        /// This is any configurable file used to control creation of documents. Examples 
        /// include the objects maintained by the HL7 Master File transactions, Value Sets, 
        /// etc.
        /// </summary>
        public static readonly SecurityEventObjectRole MasterFile = new SecurityEventObjectRole("5");

        /// <summary>
        /// A human participant not otherwise identified by some other category.
        /// </summary>
        public static readonly SecurityEventObjectRole User = new SecurityEventObjectRole("6");

        /// <summary>
        /// Deprecated.
        /// </summary>
        [Obsolete]
        public static readonly SecurityEventObjectRole List = new SecurityEventObjectRole("7");

        /// <summary>
        /// Typically a licensed person who is providing or performing care related to the 
        /// event, generally a physician. The key distinction between doctor and 
        /// practitioner is with regards to their role, not the licensing. The doctor is the 
        /// human who actually performed the work. The practitioner is the human or 
        /// organization that is responsible for the work.
        /// </summary>
        public static readonly SecurityEventObjectRole Doctor = new SecurityEventObjectRole("8");

        /// <summary>
        /// A person or system that is being notified as part of the event. This is relevant 
        /// in situations where automated systems provide notifications to other parties 
        /// when an event took place.
        /// </summary>
        public static readonly SecurityEventObjectRole Subscriber = new SecurityEventObjectRole("9");

        /// <summary>
        /// Insurance company, or any other organization who accepts responsibility for 
        /// paying for the healthcare event.
        /// </summary>
        public static readonly SecurityEventObjectRole Guarantor = new SecurityEventObjectRole("10");

        /// <summary>
        /// A person or active system object involved in the event with a security role.
        /// </summary>
        public static readonly SecurityEventObjectRole SecurityUserEntity = new SecurityEventObjectRole("11");

        /// <summary>
        /// A person or system object involved in the event with the authority to modify 
        /// security roles of other objects.
        /// </summary>
        public static readonly SecurityEventObjectRole SecurityUserGroup = new SecurityEventObjectRole("12");

        /// <summary>
        /// A passive object, such as a role table, that is relevant to the event.
        /// </summary>
        public static readonly SecurityEventObjectRole SecurityResource = new SecurityEventObjectRole("13");

        /// <summary>
        /// Deprecated. Relevant to certain RBAC security methodologies.
        /// </summary>
        [Obsolete]
        public static readonly SecurityEventObjectRole SecurityGranularityDefinition = new SecurityEventObjectRole("14");

        /// <summary>
        /// Any person or organization responsible for providing care. This encompasses all 
        /// forms of care, licensed or otherwise, and all sorts of teams and care groups. 
        /// Note, the distinction between practitioners and the doctor that actually 
        /// provided the care to the patient.
        /// </summary>
        public static readonly SecurityEventObjectRole Practitioner = new SecurityEventObjectRole("15");

        /// <summary>
        /// The source or destination for data transfer, when it does not match some other 
        /// role.
        /// </summary>
        public static readonly SecurityEventObjectRole DataDestination = new SecurityEventObjectRole("16");

        /// <summary>
        /// A source or destination for data transfer, that acts as an archive, database, or 
        /// similar role.
        /// </summary>
        public static readonly SecurityEventObjectRole DataRepository = new SecurityEventObjectRole("17");

        /// <summary>
        /// An object that holds schedule information. This could be an appointment book, 
        /// availability information, etc.
        /// </summary>
        public static readonly SecurityEventObjectRole Schedule = new SecurityEventObjectRole("18");

        /// <summary>
        /// An organization or person that is the recipient of services. This could be an 
        /// organization that is buying services for a patient, or a person that is buying 
        /// services for an animal.
        /// </summary>
        public static readonly SecurityEventObjectRole Customer = new SecurityEventObjectRole("19");

        /// <summary>
        /// An order, task, work item, procedure step, or other description of work to be 
        /// performed. E.g., a particular instance of an MPPS.
        /// </summary>
        public static readonly SecurityEventObjectRole Job = new SecurityEventObjectRole("20");

        /// <summary>
        /// A list of jobs or a system that provides lists of jobs. E.g., an MWL SCP.
        /// </summary>
        public static readonly SecurityEventObjectRole JobStream = new SecurityEventObjectRole("21");

        /// <summary>
        /// Deprecated.
        /// </summary>
        [Obsolete]
        public static readonly SecurityEventObjectRole Table = new SecurityEventObjectRole("22");

        /// <summary>
        /// An object that specifies or controls the routing or delivery of items. For 
        /// example, a distribution list is the routing criteria for mail. The items 
        /// delivered may be documents, jobs, or other objects.
        /// </summary>
        public static readonly SecurityEventObjectRole RoutingCriteria = new SecurityEventObjectRole("23");

        /// <summary>
        /// The contents of a query. This is used to capture the contents of any kind of 
        /// query. For security surveillance purposes knowing the queries being made is very 
        /// important.
        /// </summary>
        public static readonly SecurityEventObjectRole Query = new SecurityEventObjectRole("24");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventObjectRole"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventObjectRole(string value) 
            : base(value)
        {
        }

        #endregion
    }
}