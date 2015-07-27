// <copyright file="IHttpRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Adds custom headers to the request
        /// </summary>
        /// <param name="headers">The headers</param>
        HttpRequest Headers(IDictionary<string, string> headers);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        HttpRequest Body(string body);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        /// <param name="mediaType">The content media-type</param>
        HttpRequest Body(string body, string mediaType);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        /// <returns><see cref="HttpRequest"/></returns>
        HttpRequest Body(IDictionary<string, string> body);

        /// <summary>
        /// Returns the content of the response
        /// </summary>
        /// <returns></returns>
        string GetContentAsString();
    }
}