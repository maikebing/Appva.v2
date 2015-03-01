// <copyright file="AuditActiveParticipantRoleIdCode.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using System.Collections.ObjectModel;
    using Complex;
    using Primitives;

    #endregion

    /// <summary>
    /// Audit Active Participant Role ID Code.
    /// <externalLink>
    ///     <linkText>FHIR Role Id Code</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/valueset-dicm-402-roleid.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class AuditActiveParticipantRoleIdCode
    {
        /// <summary>
        /// Audit participant role ID of software application.
        /// </summary>
        public static readonly CodeableConcept Application = new CodeableConcept(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110150"), "Audit participant role ID of software application")
            });

        /// <summary>
        /// Audit participant role ID of software application launcher, i.e., the entity 
        /// that started or stopped an application.
        /// </summary>
        public static readonly CodeableConcept ApplicationLauncher = new CodeableConcept(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110151"), "Audit participant role ID of software application launcher, i.e., the entity that started or stopped an application")
            });

        /// <summary>
        /// Audit participant role ID of the receiver of data.
        /// </summary>
        public static readonly CodeableConcept DestinationRoleId = new CodeableConcept(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110152"), "Audit participant role ID of the receiver of data")
            });

        /// <summary>
        /// Audit participant role ID of the sender of data.
        /// </summary>
        public static readonly CodeableConcept SourceRoleId = new CodeableConcept(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110153"), "Audit participant role ID of the sender of data")
            });

        /// <summary>
        /// Audit participant role ID of media receiving data during an export.
        /// </summary>
        public static readonly CodeableConcept DestinationMedia = new CodeableConcept(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110154"), "Audit participant role ID of media receiving data during an export")
            });

        /// <summary>
        /// Audit participant role ID of media providing data during an import.
        /// </summary>
        public static readonly CodeableConcept SourceMedia = new CodeableConcept(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110155"), "Audit participant role ID of media providing data during an import")
            });
    }
}