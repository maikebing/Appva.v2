// <copyright file="IHipIdentity.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IHipIdentity
    {
        /// <summary>
        /// Gets the default headers for an IPV-call
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        IDictionary<string, string> GetDefaultHeaders();
    }
}