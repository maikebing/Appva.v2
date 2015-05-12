// <copyright file="AuditingService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Fhir.Resources.Security;
    using Appva.Fhir.Resources.Security.ValueSets;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuditingService : IService
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditingService"/> class.
        /// </summary>
        public AuditingService()
        {
        }

        #endregion

        #region Public Methods.

        public void CreateReadLog()
        {
        }

        #endregion

        #region Private.

        public void Setup()
        {
            
        }

        #endregion
    }
}