// <copyright file="AccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Represents the Account response model.
    /// </summary>
    /// <example>
    /// Example of a response:
    /// {
    ///    "id": "b4e58ca3-4533-4722-b1c2-b27635302de6",
    ///    "full_name": "John Doe",
    ///    "personal_identity_number": "18990101-0101",
    ///    "delegations": [
    ///        ...
    ///    ],
    ///    "beacon": {
    ///        ...
    ///    }
    /// }
    /// </example>
    [JsonObject]
    public class AccountModel 
    {
        /// <summary>
        /// <code>Account</code> ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The <code>Account</code> first and last name.
        /// </summary>
        [JsonProperty(PropertyName = "full_name")]
        public string FullName
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Account</code> Personal Identity Number.
        /// </summary>
        [JsonProperty(PropertyName = "personal_identity_number")]
        public string PersonalIdentityNumber 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// <code>Account</code> delegations.
        /// </summary>
        [JsonProperty(PropertyName = "delegations")]
        public IList<DelegationModel> Delegations 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The beason model.
        /// </summary>
        [JsonProperty(PropertyName="beacon")]
        public BeaconModel Beacon 
        { 
            get; 
            set; 
        }
    }
}