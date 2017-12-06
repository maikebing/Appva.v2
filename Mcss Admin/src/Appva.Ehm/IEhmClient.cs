// <copyright file="IEhmClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm
{
    #region Imports.

    using Appva.Ehm.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IEhmClient
    {
        /// <summary>
        /// List all ordinations for a patient
        /// </summary>
        /// <param name="forPatient"></param>
        /// <param name="byUser"></param>
        /// <returns></returns>
        Task<IList<Ordination>> ListOrdinations(string forPatientUniqueId, User byUser);
    }
}