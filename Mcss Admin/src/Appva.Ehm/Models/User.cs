// <copyright file="User.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm.Models
{
    #region Imports.

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [JsonObject]
    public class User
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the prescriber code.
        /// </summary>
        /// <value>
        /// The prescriber code.
        /// </value>
        [JsonProperty("forskrivarkod")]
        public string PrescriberCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the legitimation code.
        /// </summary>
        /// <value>
        /// The legitimation code.
        /// </value>
        [JsonProperty("legitimationskod")]
        public string LegitimationCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [JsonProperty("fornamn")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [JsonProperty("efternamn")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the workplace.
        /// </summary>
        /// <value>
        /// The workplace.
        /// </value>
        [JsonProperty("arbetsplats")]
        public string Workplace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the workplace code.
        /// </summary>
        /// <value>
        /// The workplace code.
        /// </value>
        [JsonProperty("arbetsplatsKod")]
        public string WorkplaceCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the adress.
        /// </summary>
        /// <value>
        /// The adress.
        /// </value>
        [JsonProperty("adress")]
        public string Adress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        [JsonProperty("postnummer")]
        public string Zip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty("stad")]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [JsonProperty("telefonnummer")]
        public string Phone
        {
            get;
            set;
        }

        #endregion
    }
}