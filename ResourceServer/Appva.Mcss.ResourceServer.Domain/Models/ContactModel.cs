// <copyright file="ContactModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Contact model.
    /// </summary>
    [JsonObject]
    public class ContactModel
    {
        /// <summary>
        /// Title of the contact-dialog.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title 
        {
            get;
            set;
        }

        /// <summary>
        /// Text in the contact-dialog.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Account to contact in the dialog.
        /// </summary>
        [JsonProperty(PropertyName = "accounts")]
        public List<AccountModel> Accounts 
        { 
            get; 
            set; 
        }
    }
}
