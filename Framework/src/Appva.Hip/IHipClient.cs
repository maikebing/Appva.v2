// <copyright file="IHipClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip
{
    #region Imports.

    using Appva.Hip.Clients;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IHipClient
    {
        #region Properties

        /// <summary>
        /// Druglist operations
        /// </summary>
        DruglistClient Druglist
        {
            get;
        }

        /// <summary>
        /// Consent operations
        /// </summary>
        ConsentsClient Consents
        {
            get;
        }

        /// <summary>
        /// Medication operations
        /// </summary>
        MedicationHistoryClient Medication
        { 
            get;
        }
        
        #endregion

        /// <summary>
        /// Checks if there is a valid consent for druglist-interactions
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<bool> CheckDruglistConsent(string patientId);
    }
}