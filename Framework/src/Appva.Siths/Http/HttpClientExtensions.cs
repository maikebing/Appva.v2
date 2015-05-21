// <copyright file="HttpClientExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Http
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class HttpClientExtensions
    {
        /// <summary>
        /// Posts as form url encoded.
        /// </summary>
        /// <typeparam name="T">The response type</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/></param>
        /// <param name="requestUri">The request URI</param>
        /// <param name="keyValues">The key value</param>
        /// <returns>A <see cref="Task{T}"/></returns>
        public static async Task<T> PostAsFormUrlEncodedAsync<T>(this HttpClient httpClient, string requestUri, IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            var response = await httpClient.PostAsync(requestUri, new FormUrlEncodedContent(keyValues))
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string))
            {
                return (T) (object) content;
            }
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}