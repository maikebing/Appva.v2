// <copyright file="LogClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.Audit
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Http;
    using Appva.Fhir.Resources.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LogClient : RestfulClient
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogClient"/> class.
        /// </summary>
        public LogClient(string baseAddress)
            : base(baseAddress)
        {
        }

        #endregion

        #region Public Methods.

        public bool Create(AuditEvent securityEvent)
        {
            return this.CreateNewPostAsync<bool>("/v1/logs/", securityEvent).Result;
        }

        public async void CreateAsync(AuditEvent securityEvent)
        {
            await this.CreateNewPostAsync<bool>("/v1/logs/", securityEvent);
        }

        #endregion
    }
}