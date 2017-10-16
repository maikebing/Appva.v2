// <copyright file="MeasurementScale.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class MeasurementScale
    {
        #region Enum

        /// <summary>
        /// Enum Scale
        /// </summary>
        public enum Scale : byte
        {
            none,
            weight,
            bristol,
            melior
        }

        /// <summary>
        /// Enum KingScale Values
        /// </summary>
        private enum KingScale : byte
        {
            none,
            A1, A2, A3,
            B1, B2, B3,
            C1, C2, C3,
            D1, D2, D3
        }

        /// <summary>
        /// Enum MeliorScale Values
        /// </summary>
        public enum MeliorScale : byte
        {
            none,
            bigtriplea,
            bigdoublea,
            biga,
            smalla,
            smalltriplea,
            smalld,
            bigd,
            k
        }

        #endregion

        #region Public members
        
        #region HasValidValue

        /// <summary>
        /// Determines whether [has valid value] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="scale">The scale.</param>
        /// <returns><c>true</c> if [has valid value] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool HasValidValue(string value, string scale)
        {
            if (value == null || value.Trim() == string.Empty)
            {
                return false;
            }

            var result = false;

            if (Enum.TryParse(scale, out Scale validScale) == false)
            {
                return false;
            }

            switch (validScale)
            {
                case Scale.weight:
                    result = ValidateWeight(value);
                    break;
                case Scale.bristol:
                    result = ValidateBristol(value);
                    break;
                case Scale.melior:
                    result = ValidateMelior(value);
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        #endregion

        #region GetUnitForScale

        /// <summary>
        /// Gets the unit for scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>System.String.</returns>
        public static string GetUnitForScale(string scale)
        {
            var result = string.Empty;

            if (Enum.TryParse(scale, out Scale validScale))
            {
                result = GetUnitForScale(validScale);
            }

            return result;
        }

        /// <summary>
        /// Gets the unit for scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>System.String.</returns>
        public static string GetUnitForScale(Scale scale)
        {
            var result = string.Empty;

            switch (scale)
            {
                case Scale.weight:
                    result = "kg";
                    break;
                case Scale.bristol:
                    result = "Typ";
                    break;
                case Scale.melior:
                    result = "Typ";
                    break;
                default:
                    result = "na";
                    break;
            }

            return result;
        }

        #endregion

        #region GetNameForScale

        /// <summary>
        /// Gets a readable the name for the scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>System.String.</returns>
        public static string GetNameForScale(Scale scale)
        {
            var result = string.Empty;

            switch (scale)
            {
                case Scale.weight:
                    result = "Vikt (kg)";
                    break;
                case Scale.bristol:
                    result = "Bristol Stool Scale (Typ 1-7)";
                    break;
                case Scale.melior:
                    result = "Generisk Avföringsskala (Typ A, a)";
                    break;
                default:
                    result = "na";
                    break;
            }

            return result;
        }

        #endregion

        #region GetBounderiesForScale


        #endregion


        #region MeliorScaleHelpers

        /// <summary>
        /// Returns the value from the enum description of the value. 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetMeliorValue(string value)
        {
            return MeliorToString((MeliorScale)Enum.Parse(typeof(MeliorScale), value));
        }

        /// <summary>
        /// MeliorScale to string value
        /// </summary>
        /// <param name="scale">The melior scale.</param>
        /// <returns>System.String.</returns>
        public static string MeliorToString(this MeliorScale scale)
        {
            var retval = string.Empty;

            switch (scale)
            {
                case MeliorScale.bigtriplea:
                    retval = "AAA";
                    break;
                case MeliorScale.bigdoublea:
                    retval = "AA";
                    break;
                case MeliorScale.biga:
                    retval = "A";
                    break;
                case MeliorScale.smalla:
                    retval = "a";
                    break;
                case MeliorScale.smalltriplea:
                    retval = "aaa";
                    break;
                case MeliorScale.smalld:
                    retval = "d";
                    break;
                case MeliorScale.bigd:
                    retval = "D";
                    break;
                case MeliorScale.k:
                    retval = "k";
                    break;
                default:
                    retval = "n/a";
                    break;
            }
            return retval;
        }

        #endregion



        #endregion

        #region Private members.

        /// <summary>
        /// Validates to melior scale.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value meets the requirements, otherwise <c>false</c></returns>
        private static bool ValidateMelior(string value)
        {
            if ((Enum.TryParse(value, out MeliorScale scale)) == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates the bristol.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value meets the requirements, otherwise <c>false</c></returns>
        private static bool ValidateBristol(string value)
        {
            if (int.TryParse(value, out int bristol) == true)
            {
                if ((bristol <= 7 && bristol >= 1) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the weight.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value meets the requirements, otherwise <c>false</c></returns>
        private static bool ValidateWeight(string value)
        {
            if (double.TryParse(value, out double weight) == true)
            {
                if ((weight <= 450 && weight >= 5) == true)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}