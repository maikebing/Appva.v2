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

    #endregion

    /// <summary>
    /// GetManualEventModel, Http model.
    /// </summary>
    [HttpRequest]
    public sealed class GetManualEventModel
    {
        #region Fields.

        /// <summary>
        /// Id.
        /// </summary>
        [HttpRequestProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// ImportResult.
        /// </summary>
        [HttpRequestProperty("importResult")]
        public string ImportResult
        {
            get;
            set;
        }
        #endregion
    }
}
