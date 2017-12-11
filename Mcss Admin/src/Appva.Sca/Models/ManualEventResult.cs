// <copyright file="GetManualEventModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca.Models
{
    #region Imports.

    using Appva.Http;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// GetManualEventModel, Http model.
    /// </summary>
    [JsonObject]
    public sealed class ManualEventResult
    {
        #region Fields.

        /// <summary>
        /// Id.
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// ImportResult.
        /// </summary>
        [JsonProperty("importResult")]
        public ImportResult ImportResult
        {
            get;
            set;
        }


        #endregion
    }
}
