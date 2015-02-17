// <copyright file="AuthenticationCredentialsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// User authentication credentials.
    /// </summary>
    /// <example>
    /// Example request:
    /// {
    ///    "user_name": "foobar",
    ///    "Password": "bar1231!BAZ"
    /// }
    /// </example>
    [JsonObject]
    public class AuthenticationCredentialsModel
    {
        /// <summary>
        /// The user name.
        /// </summary>
        [JsonProperty(PropertyName = "user_name", Required = Required.Always)]
        public string UserName 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The user password.
        /// </summary>
        [JsonProperty(PropertyName = "password", Required = Required.Always)]
        public string Password 
        { 
            get; 
            set; 
        }
    }
}