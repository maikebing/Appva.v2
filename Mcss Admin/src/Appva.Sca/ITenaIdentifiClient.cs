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
        /// <returns>Returns a <see cref="GetResidentModel"/>.</returns>
        Task<GetResidentModel> GetResidentAsync(string id);

        /// <summary>
        /// Posts the manual event asynchronous.
        /// </summary>
        /// <param name="manualEvents">The manual events.</param>
        /// <returns>Task&lt;List&lt;GetManualEventModel&gt;&gt;.</returns>
        Task<List<GetManualEventModel>> PostManualEventAsync(List<PostManualEventModel> manualEvents);

        /// <summary>
        /// Gets a value indicating whether this instance has credentials.
        /// </summary>
        /// <value><c>true</c> if this instance has credentials; otherwise, <c>false</c>.</value>
        bool HasCredentials { get; }

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        void SetCredentials(string credentials);

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="tenant">The tenant.</param>
        /// <param name="credentials">The credentials.</param>
        void SetCredentials(string tenant, string credentials);
    }
}