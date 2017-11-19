// <copyright file="ITenaIdentifiClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Sca
{
    #region Imports.

    using Appva.Sca.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// The <see cref="ITenaIdentifiClient"/>.
    /// </summary>
    public interface ITenaIdentifiClient
    {
        /// <summary>
        /// GetResident
        /// </summary>
        /// <param name="id">Identifi ID</param>
        /// <returns>Returns a <see cref="Resident"/>.</returns>
        Task<Resident> GetResidentAsync(string id, TenaIdentifiCredentials credentials);

        /// <summary>
        /// Posts the manual event asynchronous.
        /// </summary>
        /// <param name="manualEvents">The manual events.</param>
        /// <returns>Task&lt;List&lt;GetManualEventModel&gt;&gt;.</returns>
        Task<List<ManualEventResult>> PostManualEventsAsync(List<ManualEvent> manualEvents, TenaIdentifiCredentials credentials);
    }
}