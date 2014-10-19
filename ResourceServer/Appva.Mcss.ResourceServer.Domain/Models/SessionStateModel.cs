// <copyright file="SessionStateModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Session state model.
    /// </summary>
    /// <example>
    /// Example response:
    /// {
    ///    "timeout": "2880",
    ///    "entity": {
    ///         "full_name": "John Doe",
    ///         ...
    ///     }
    /// }
    /// </example>
    /// <typeparam name="T">An AccountModel</typeparam>
    [JsonObject]
    public class SessionStateModel<T> where T : AccountModel
    {
        /// <summary>
        /// Time out in seconds.
        /// </summary>
        [JsonProperty(PropertyName = "timeout")]
        public int TimeOut 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Generic session based entity.
        /// </summary>
        [JsonProperty(PropertyName = "entity")]
        public T Entity 
        { 
            get; 
            set; 
        } 
    }
}