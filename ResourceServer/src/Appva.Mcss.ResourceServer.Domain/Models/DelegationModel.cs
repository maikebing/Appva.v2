// <copyright file="DelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Hydrated delegation model.
    /// </summary>
    /// <example>
    /// Example response:
    /// {
    ///    "id": "b4e58ca3-4533-4722-b1c2-b27635302de6",
    ///    "name": "Rights to administer X and Y"
    /// }
    /// </example>
    [JsonObject]
    public class DelegationModel
    {
        /// <summary>
        /// <code>Delegation</code> ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Delegation</code> ID.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name 
        { 
            get; 
            set; 
        }
    }
}