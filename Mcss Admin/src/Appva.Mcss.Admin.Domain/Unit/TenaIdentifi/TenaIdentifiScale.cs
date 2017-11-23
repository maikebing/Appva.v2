// <copyright file="TenaIdentifiScale.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// The TENA Identifi svale
    /// </summary>
    public enum TenaIdentifiScale
    {
        Toilet,
        ToiletUnsuccessful,
        Leakage,
        Feces
    }

    /// <summary>
    /// Tena identifi scale extensions
    /// </summary>
    public static class TenaIdentifiScaleExtensions {

        /// <summary>
        /// To the readable string.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public static string ToReadableString(this TenaIdentifiScale unit) {
            switch (unit) {
                case TenaIdentifiScale.Toilet:
                    return "Toalettbesök med resultat";
                case TenaIdentifiScale.ToiletUnsuccessful:
                    return "Toalettbesök utan resultat";
                case TenaIdentifiScale.Leakage:
                    return "Läckage utanför skyddet vid byte av skydd";
                case TenaIdentifiScale.Feces:
                    return "Avföring i skyddet vid byte av skydd";
                default:
                    return string.Empty;
            }
        }
    }
}