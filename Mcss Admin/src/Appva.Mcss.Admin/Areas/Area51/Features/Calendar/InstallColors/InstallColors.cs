﻿// <copyright file="InstallColors.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
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
    public sealed class InstallColors : IRequest<CalendarIndex>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallColors"/> class.
        /// </summary>
        public InstallColors()
        {
        }

        #endregion
    }
}