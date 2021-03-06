﻿// <copyright file="SearchPatientModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SearchPatientModel
    {
        #region Properties

        public bool IsActive
        {
            get;
            set;
        }

        public bool IsDeceased
        {
            get;
            set;
        }

        public string SearchQuery
        {
            get;
            set;
        }

        public string TaxonFilter
        {
            get;
            set;
        }

        #endregion
    }
}