// <copyright file="TaxonModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Taxon model.
    /// </summary>
    [JsonObject]
    public class TaxonModel 
    {
        /// <summary>
        /// <code>Taxon</code> ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The <code>Taxon</code> name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Taxon</code> description.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Taxon</code> type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Number of incomplete tasks below <code>Taxon</code>
        /// </summary>
        [JsonProperty(PropertyName = "incomplete_tasks")]
        public int IncompleteTask 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If <code>Taxon</code> is root
        /// </summary>
        [JsonProperty(PropertyName = "is_root")]
        public bool IsRoot 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If <code>Taxon</code> has children
        /// </summary>
        [JsonProperty(PropertyName = "has_children")]
        public bool HasChildren 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// All <code>Patient</code> connected to this taxon
        /// </summary>
        [JsonProperty(PropertyName = "patient_count")]
        public int PatientCount 
        { 
            get; 
            set; 
        }
    }
}