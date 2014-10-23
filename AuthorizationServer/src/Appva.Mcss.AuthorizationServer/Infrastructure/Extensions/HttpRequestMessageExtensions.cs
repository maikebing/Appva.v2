// <copyright file="HttpRequestMessageExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Returns true if the IP address of the request originator was localhost,
        /// 127.0.0.1 or if the IP address of the request is the same as the 
        /// server's IP address.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/></param>
        /// <returns>Returns true if local request</returns>
        public static bool IsLocal(this HttpRequestMessage request)
        {
            var isLocal = request.Properties["MS_IsLocal"] as Lazy<bool>;
            return isLocal.IsNotNull() && isLocal.Value;
        }
    }
}