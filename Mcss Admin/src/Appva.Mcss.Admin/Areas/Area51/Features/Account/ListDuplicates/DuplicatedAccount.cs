// <copyright file="DuplicatedAccount.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DuplicatedAccount
    {
        #region Properties.

        /// <summary>
        /// The primary account
        /// </summary>
        public Account Primary
        {
            get;
            set;
        }

        /// <summary>
        /// The secondary account
        /// </summary>
        public Account Secondary
        {
            get;
            set;
        }

        #endregion
    }
}