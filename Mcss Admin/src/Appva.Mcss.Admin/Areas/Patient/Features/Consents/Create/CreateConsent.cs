// <copyright file="CreateConsentModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Hip.Model;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateConsent : Identity<bool>
    {
        #region Properties

        /// <summary>
        /// The refferencing page
        /// </summary>
        public string Referer
        {
            get;
            set;
        }

        /// <summary>
        /// The Patient
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The registerd Consents
        /// </summary>
        public Consents Consents 
        {
            get;
            set; 
        }

        /// <summary>
        /// Register consent for this time
        /// </summary>
        public bool DruglistSingelAccess 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Register ongoing consent for this patient
        /// </summary>
        public bool DruglistOngoingAccess
        {
            get;
            set;
        }

        public bool PdlOnlyMe
        {
            get;
            set;
        }

        public DateTime PdlValidTo
        { 
            get;
            set;
        }
     
        public bool ValidPdlExists
        {
            get;
            set;
        }

        #endregion

        
    }
}