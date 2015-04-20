// <copyright file="AuthenticationType.cs" company="Appva AB">
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
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthenticationType
    {
        #region Authentication Types Implementation.

        /// <summary>
        /// The authentication type for MCSS Administrative web application.
        /// </summary>
        public static readonly AuthenticationType Administrative = new AuthenticationType("37F085B0-55E7-4846-93DB-A46D009A21B0-Appva-Mcss-Administration");

        #endregion

        #region Variables.

        /// <summary>
        /// The internal authentication type value.
        /// </summary>
        private readonly string authenticationType;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationType"/> class.
        /// </summary>
        /// <param name="authenticationType">The authentication type</param>
        private AuthenticationType(string authenticationType)
        {
            this.authenticationType = authenticationType;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the authentication type value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.authenticationType;
            }
        }

        #endregion
    }
}