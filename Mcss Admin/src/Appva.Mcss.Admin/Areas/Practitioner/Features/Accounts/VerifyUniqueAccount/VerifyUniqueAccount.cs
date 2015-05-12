// <copyright file="VerifyUniqueAccount.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class VerifyUniqueAccount : IRequest<bool>
    {
        /// <summary>
        /// The account ID.
        /// </summary>
        public Guid? Id
        {
            get;
            set;
        }

        /// <summary>
        /// The personal identity number.
        /// </summary>
        public string UniqueIdentifier
        {
            get;
            set;
        }
    }
}