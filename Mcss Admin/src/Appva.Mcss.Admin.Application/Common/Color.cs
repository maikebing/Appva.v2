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
    /// Predefined colors for MCSS
    /// </summary>
    public enum Color
    {
        Blue, DarkGrey, Orange, Purple, DarkBlue, Yellow, Green, Turquoise
    }
    
    public static class ColorExtensions {

        /// <summary>
        /// The readable name of a color
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Readable name </returns>
        public static string Name(this Color color)
        {
            switch (color) {
                case Color.Blue:
                    return "Blå";
                case Color.DarkBlue:
                    return "Mörkblå";
                case Color.DarkGrey:
                    return "Mörkgrå";
                case Color.Green:
                    return "Grön";
                case Color.Orange:
                    return "Orange";
                case Color.Purple:
                    return "Lila";
                case Color.Turquoise:
                    return "Turkos";
                case Color.Yellow:
                    return "Gul";
                default:
                    return "Transparent";
            }
        }

        /// <summary>
        /// The hex-representation of a color
        /// </summary>
        /// <param name="color">The <see cref="Color"/></param>
        /// <returns>The hex-color as a string including #</returns>
        public static string Hex(this Color color)
        {
            switch (color) {
                case Color.Blue:
                    return "#0091ce";
                case Color.DarkBlue:
                    return "#47597f";
                case Color.DarkGrey:
                    return "#5d5d5d";
                case Color.Green:
                    return "#349d00";
                case Color.Orange:
                    return "#e28a00";
                case Color.Purple:
                    return "#b668ca";
                case Color.Turquoise:
                    return "#1ed288";
                case Color.Yellow:
                    return "#e9d600";
                default:
                    return "Transparent";
            }
        }

    }
}

