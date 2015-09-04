// <copyright file="DemoHipIdentity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip.Identity
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DemoHipIdentity : IHipIdentity
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoHipIdentity"/> class.
        /// </summary>
        public DemoHipIdentity()
        {
        }

        #endregion

        #region IHipIdentity Members

        /// <inheritdoc />
        public IDictionary<string, string> GetDefaultHeaders()
        {
            return new Dictionary<string, string>
            {
                {"X-IPV-User-UserId", "appva"},
                {"X-IPV-User-CareUnitId", "appva"},
                {"X-IPV-User-CareProviderId", "appva"},
                {"X-IPV-User-AssignmentId", "superadmin"},
                {"X-IPV-User-PrescriptionCode", "prescription-code"},
                {"X-IPV-User-Permission", "LÄSA;alla;SJF"},
                {"X-IPV-Commission-Purpose", "Vård och behandling"},
                {"X-IPV-User-SessionId", "xxxxxyyyyy"},
                {"Cache-Control", "no-cache"},
                
                //// Following headers is only needed in dev and demo, not for prod
                {"X-IPV-Development-CN", "vpv-client.dev.chorus.se"},
                {"X-subject-of-care", "191212121212"}
            };
        }

        #endregion
    }
}