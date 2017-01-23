// <copyright file="Color.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public enum Color
    {
        Blue, DarkGrey, 
    }

    new SelectListItem { Text = "Mörkgrå", Value = "#5d5d5d" },
            new SelectListItem { Text = "Turkos", Value = "#1ed288" },
            new SelectListItem { Text = "Orange", Value = "#e28a00" },
            new SelectListItem { Text = "Lila", Value = "#b668ca" },
            new SelectListItem { Text = "Mörkblå", Value = "#47597f" },
            new SelectListItem { Text = "Gul", Value = "#e9d600" },
            new SelectListItem { Text = "Blå", Value = "#0091ce" },
            new SelectListItem { Text = "Grön", Value = "#349d00" }
}

