// <copyright file="UriHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca
{
    /// <summary>
    /// UriHelper
    /// </summary>
    internal static class UriHelper
    {
        #region Fields.

        /// <summary>
        /// ManualEvent Enpoint url.
        /// </summary>
        internal const string ManualEventUrl = "api/manualevent/";

        /// <summary>
        /// Token Endpoint url.
        /// </summary>
        internal const string TokenUrl = "api/token/";

        /// <summary>
        /// Redident Endpoint url
        /// </summary>
        private const string ResidentUrl = "api/resident/";

        #endregion

        #region Members.

        /// <summary>
        /// GetResidentUrl
        /// </summary>
        /// <param name="id">External Id.</param>
        /// <returns>A full resident url as string</returns>
        internal static string GetResidentUrl(string id)
        {
            return string.Format("{0}{1}", ResidentUrl, id);
        }

        #endregion
    }
}
