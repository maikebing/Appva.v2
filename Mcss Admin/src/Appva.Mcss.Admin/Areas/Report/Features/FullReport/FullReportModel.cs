﻿// <copyright file="FullReportModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FullReportModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullReportModel"/> class.
        /// </summary>
        public FullReportModel()
        {
        }

        #endregion

        public Repository.PageableSet<Domain.Entities.Task> Tasks { get; set; }

        public Application.Models.ReportData Report { get; set; }
    }
}