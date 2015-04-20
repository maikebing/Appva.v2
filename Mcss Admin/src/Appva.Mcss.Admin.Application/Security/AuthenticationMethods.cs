// <copyright file="AuthenticationMethod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthenticationMethod
    {
        #region Variables.

        /// <summary>
        /// Authentication by using a password.
        /// </summary>
        public static readonly AuthenticationMethod Password = new AuthenticationMethod(AuthenticationMethods.Password);

        /// <summary>
        /// The internal authentication method.
        /// </summary>
        private readonly string method;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationMethod"/> class.
        /// </summary>
        /// <param name="method">The authentication method to use</param>
        private AuthenticationMethod(string method)
        {
            this.method = method;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the authentication method value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.method;
            }
        }

        #endregion
    }
}