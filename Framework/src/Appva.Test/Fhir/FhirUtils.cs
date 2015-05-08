// <copyright file="FhirUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Fhir
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Appva.Fhir.Complex;
    using Appva.Fhir.Primitives;
    using Appva.Fhir.Resources;
    using Appva.Fhir.Resources.Security;
    using Appva.Fhir.Resources.Security.ValueSets;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class FhirUtils
    {
        /// <summary>
        /// Creates a new instance of <see cref="AuditEvent"/>.
        /// </summary>
        /// <returns>A new <see cref="AuditEvent"/> instance</returns>
        public static AuditEvent CreateNewAuditEvent()
        {
            var events = new AuditEventEvent(
                AuditEventType.ApplicationActivity, 
                new Collection<AuditEventSubType> { AuditEventSubType.Create }, 
                AuditEventAction.Create, 
                DateTime.Now,
                AuditEventOutcome.Success, 
                "");
            var participants = new AuditEventParticipant(
                new Collection<CodeableConcept> { new CodeableConcept(new Coding(new Appva.Fhir.Primitives.Uri("http://example.com"), new Code("Role1"), "Role1" )) },
                "userId", "alternativeId",
                "Name",
                true,
                null,
                new AuditEventParticipantNetwork("127.0.0.1", AuditEventParticipantNetworkType.IpAddress));
            var objects = new AuditEventObject("identifier"
                , AuditEventObjectType.Person,
                AuditEventObjectRole.Patient,
                AuditEventObjectLifecycle.Access,
                AuditEventObjectSensitivity.Normal,
                "name",
                "description",
                new Base64Binary("query".ToBase64()));
            var source = new AuditEventSource(
                "site",
                "identifier",
                AuditEventSourceType.WebServer);

            var ae = new AuditEvent(events, new List<AuditEventParticipant> { participants }, source, new List<AuditEventObject> { objects });
            ae.Language = Language.English;
            ae.Meta = new Meta();
            ae.Meta.LastUpdated = new Instant(DateTimeOffset.Now);
            return ae;
        }
    }
}