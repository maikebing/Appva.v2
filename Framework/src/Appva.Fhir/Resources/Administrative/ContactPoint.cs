// <copyright file="ContactPoint.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// Details for all kinds of technology-mediated contact points for a person or 
    /// organization, including telephone, email, etc.
    /// <para>
    /// If capturing a phone, fax or similar contact point, the value should be a 
    /// properly formatted telephone number according to ITU-T E.123. However, this is 
    /// frequently not possible due to legacy data and/or recording methods.
    /// </para>
    /// <externalLink>
    ///     <linkText>1.14.0.14 ContactPoint</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#ContactPoint
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class ContactPoint : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPoint"/> class.
        /// </summary>
        public ContactPoint()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Telecommunications form for contact point - what communications system is 
        /// required to make use of the contact. See <see cref="ContactPointSystem"/>.
        /// <externalLink>
        ///     <linkText>System</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#ContactPoint.system
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public ContactPointSystem System
        {
            get;
            private set;
        }

        /// <summary>
        /// The actual contact point details, in a form that is meaningful to the designated 
        /// communication system (i.e. phone number or email address).
        /// <externalLink>
        ///     <linkText>Value</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#ContactPoint.value
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Additional out of band data such as extensions, or notes about use of the 
        /// contact are sometimes included in the value.
        /// </remarks>
        public string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Identifies the purpose for the contact point. 
        /// See <see cref="ContactPointUse"/>.
        /// <externalLink>
        ///     <linkText>Use</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#ContactPoint.use
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Need to track the way a person uses this contact, so a user can choose which is 
        /// appropriate for their purpose.
        /// </remarks>
        public ContactPointUse Use
        {
            get;
            private set;
        }

        /// <summary>
        /// Identifies the purpose for the contact point.Time period when the contact point 
        /// was/is in use.
        /// <externalLink>
        ///     <linkText>Period</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#ContactPoint.period
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public Period Period
        {
            get;
            private set;
        }

        #endregion
    }
}