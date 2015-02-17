// <copyright file="PatientModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Patient model.
    /// </summary>
    [JsonObject]
    public class PatientModel 
    {
        /// <summary>
        /// <code>Patient</code> ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The <code>Patient</code> first and last name.
        /// </summary>
        [JsonProperty(PropertyName = "full_name")]
        public string FullName 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Patient</code> Personal Identity Number.
        /// </summary>
        [JsonProperty(PropertyName = "personal_identity_number")]
        public string PersonalIdentityNumber 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Patient</code> organisation taxon.
        /// </summary>
        [JsonProperty(PropertyName = "organisation_taxon")]
        public TaxonModel OrganisationTaxon 
        { 
            get; 
            set; 
        }
        
        /// <summary>
        /// <code>Patient</code> has incomplete tasks
        /// </summary>
        [JsonProperty(PropertyName = "has_incomplete_tasks")]
        public bool HasIncompleteTasks 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Patient</code> profile taxons.
        /// </summary>
        [JsonProperty(PropertyName = "profiles")]
        public IList<ProfileModel> Profiles 
        { 
            get; 
            set; 
        }
    }
}