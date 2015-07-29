// <copyright file="IHttpRequestClient.cs" company="Appva AB">
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
    using System.Net.Http;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IHttpRequestClient
    {
        /// <summary>
        /// Creates a new <see cref="HttpRequest"/> by GET
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        IHttpRequest Get(string url);

        /// <summary>
        /// Creates a new <see cref="HttpRequest"/> by PUT
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        IHttpRequest Put(string url);

        /// <summary>
        /// Creates a new <see cref="HttpRequest"/> by POST
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        IHttpRequest Post(string url);
    }
}