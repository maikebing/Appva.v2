// <copyright file="AuthorizationServerOwinConstants.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class AuthorizationServerOwinConstants
    {
        /// <summary>
        /// 
        /// </summary>
        internal const string OwinAuthenticationService = "AuthorizationServerOwin.AuthenticationService";
        
        /// <summary>
        /// 
        /// </summary>
        public const string AuthenticationType = "AuthorizationServerOwin";
        
        /// <summary>
        /// 
        /// </summary>
        public const string AuthenticationTwoFactorType = "AuthorizationServerOwin.2fa";
    }
}