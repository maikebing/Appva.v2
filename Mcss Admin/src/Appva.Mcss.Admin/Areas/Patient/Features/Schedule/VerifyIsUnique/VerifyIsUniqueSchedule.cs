﻿// <copyright file="VerifyIsUniqueSchedule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class VerifyIsUniqueSchedule : IRequest<bool>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule setting ID.
        /// </summary>
        public Guid ScheduleSetting
        {
            get;
            set;
        }
    }
}