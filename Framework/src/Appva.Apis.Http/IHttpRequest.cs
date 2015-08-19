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
    using System.Net;
    using System.Threading.Tasks;

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
        IHttpRequest WithHeaders(IDictionary<string, string> headers);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        IHttpRequest WithBody(string body);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        /// <param name="mediaType">The content media-type</param>
        IHttpRequest WithBody(string body, string mediaType);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        /// <returns><see cref="HttpRequest"/></returns>
        IHttpRequest WithFormUrlEncodedBody(IDictionary<string, string> body);

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        /// <returns><see cref="HttpRequest"/></returns>
        IHttpRequest WithFormUrlEncodedBody<T>(object body) where T : class;

        /// <summary>
        /// Serializes the object to Json and sets the content of the request
        /// </summary>
        /// <param name="body">The content</param>
        /// <returns><see cref="HttpRequest"/></returns>
        IHttpRequest WithJsonEncodedBody(object body);

        /// <summary>
        /// Returns the content as object
        /// </summary>
        /// <returns></returns>
        Task<T> ToResultAsync<T>();

        /// <summary>
        /// Returns the content of the response
        /// </summary>
        /// <returns></returns>
        Task<string> ToResultAsString();

        /// <summary>
        /// Returns the statuscode of the response
        /// </summary>
        /// <returns></returns>
        HttpStatusCode GetResponseStatusCode();
    }
}