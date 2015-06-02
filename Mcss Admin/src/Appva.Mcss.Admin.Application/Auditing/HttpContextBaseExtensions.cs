// <copyright file="HttpContextBaseExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Auditing
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;
    using System.Web;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class HttpContextBaseExtensions
    {
        /// <summary>
        /// Matching an IPv4 or v6 address that ends a string.
        /// </summary>
        private static Regex LastAddress = new Regex(@"\b(\d|a-f|\.|:)+$", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string Url(this HttpContextBase context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            return context.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// The IP address (v4 or v6) that the current request originated from.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string RemoteIP(this HttpContextBase context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            var server = context.Request.ServerVariables;
            var headers = context.Request.Headers;
            return GetRemoteIP(headers["X-Real-IP"], server["REMOTE_ADDR"], headers["X-Forwarded-For"]);
        }

        /// <summary>
        /// Takes in the REMOTE_ADDR and X-Forwarded-For headers and returns what
        /// we consider the current requests IP to be, for logging and throttling 
        /// purposes.
        /// The logic is, basically, if xForwardedFor *has* a value and the apparent
        /// IP (the last one in the hop) is not local; use that.  Otherwise, use remoteAddr.
        /// </summary>
        internal static string GetRemoteIP(string realIp, string remoteAddr, string xForwardedFor)
        {
            if (realIp.IsNotEmpty() && ! IsPrivateIP(realIp))
            {
                return realIp;
            }
            if (xForwardedFor.IsNotEmpty())
            {
                xForwardedFor = LastAddress.Match(xForwardedFor).Value;
                if (xForwardedFor.IsNotEmpty() && ! IsPrivateIP(xForwardedFor))
                {
                    remoteAddr = xForwardedFor;
                }
            }
            return remoteAddr;
        }

        /// <summary>
        /// Returns true if this is a private network IP (v4 or v6)
        /// http://en.wikipedia.org/wiki/Private_network
        /// </summary>
        internal static bool IsPrivateIP(string ip)
        {
            var ipv4Check = (ip.StartsWith("192.168.") || ip.StartsWith("10.") || ip.StartsWith("127.0.0."));
            if (ipv4Check)
            {
                return true;
            }
            IPAddress ipAddress;
            if (! IPAddress.TryParse(ip, out ipAddress) || ipAddress.AddressFamily != AddressFamily.InterNetworkV6)
            {
                return false;
            }
            //// IPv6 reserves fc00::/7 for local usage
            //// http://en.wikipedia.org/wiki/Unique_local_address
            var address = ipAddress.GetAddressBytes();
            //// FC + the L-bit set to make FD
            return address[0] == (byte) 0xFD;
        }
    }
}