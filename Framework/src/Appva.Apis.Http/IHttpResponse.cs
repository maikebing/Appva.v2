// <copyright file="IHttpReponse.cs" company="Appva AB">
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
    public interface IHttpResponse
    {
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
        HttpStatusCode GetStatusCode();

        /// <summary>
        /// Returns if the operation was succeded or not
        /// </summary>
        /// <returns></returns>
        bool IsSuccessStatusCode(); 
    }
}