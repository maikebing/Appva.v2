﻿// <copyright file="VerifyUniqueAccount.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;

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
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }
    }
}