// <copyright file="ReadUpdateClientForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ReadUpdateClientForm : CommonClient
    {
        /// <summary>
        /// The client password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string Secret
        {
            get;
            set;
        }

        /// <summary>
        /// The client logotype if any.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}