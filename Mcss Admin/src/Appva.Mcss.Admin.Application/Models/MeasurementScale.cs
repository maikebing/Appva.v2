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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

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
            Weight,
            Bristol,
            Common
        }

        /// <summary>
        /// Enum KingScale Values
        /// </summary>
        private enum KingScale : byte
        {
            A1, A2, A3,
            B1, B2, B3,
            C1, C2, C3,
            D1, D2, D3
        }

        /// <summary>
        /// Enum MeliorScale Values
        /// </summary>
        public enum CommonScale : byte
        {
            BigTripleA,
            BigDoubleA,
            BigA,
            SmallA,
            SmallTripleA,
            SmallD,
            BigD,
            K
        }

        public enum CommonScaleValues : byte
        {
            AAA,
            AA,
            A,
            a,
            aaa,
            d,
            D,
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
            Scale validScale;
            if (Enum.TryParse(scale, out validScale) == false)
            {
                return false;
            }

            switch (validScale)
            {
                case Scale.Weight:
                    result = ValidateWeightScale(value);
                    break;
                case Scale.Bristol:
                    result = ValidateBristolScale(value);
                    break;
                case Scale.Common:
                    result = ValidateCommonScale(value);
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        #endregion

        public static IEnumerable<Scale> All()
        {
            return Enum.GetValues(typeof(Scale)).Cast<Scale>();
        }

        #region GetUnitForScale

        /// <summary>
        /// Gets the unit for scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>System.String.</returns>
        public static string GetUnitForScale(string scale)
        {
            var result = string.Empty;
            Scale validScale;
            if (Enum.TryParse(scale, true, out validScale))
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
                case Scale.Weight:
                    result = "kg";
                    break;
                case Scale.Bristol:
                    result = "Typ";
                    break;
                case Scale.Common:
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
        /// Gets a readable the name for the a scale for presentation purposes.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>System.String.</returns>
        public static string GetNameForScale(string scale)
        {
            var result = string.Empty;
            Scale validScale;
            if (Enum.TryParse(scale, true, out validScale))
            {
                result = GetNameForScale(validScale);
            }

            return result;
        }

        /// <summary>
        /// Gets a readable the name for the a scale for presentation purposes.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>System.String.</returns>
        public static string GetNameForScale(Scale scale)
        {
            var result = string.Empty;

            switch (scale)
            {
                case Scale.Weight:
                    result = "Vikt (kg)";
                    break;
                case Scale.Bristol:
                    result = "Bristol Stool Scale (Typ 1-7)";
                    break;
                case Scale.Common:
                    result = "Generisk Avföringsskala (Typ A, a)";
                    break;
                default:
                    result = "na";
                    break;
            }

            return result;
        }

        #endregion

        #region GetScaleBounderies
        public static string GetScaleBounderies(Scale scale)
        {
            var retval = string.Empty;

            //// TODO: build an object with bounderies/instructions for the view to present and pass through validation in handlers.

            return retval;
        }

        #endregion

        #region CommonScaleHelpers

        /// <summary>
        /// Returns the value from the enum description of the value. 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetCommonScaleValue(string value)
        {
            CommonScaleValues result;
            if ((Enum.TryParse<CommonScaleValues>(value, false, out result)) == true)
            {
                return result.ToString();
            }

            return CommonToString((CommonScale)Enum.Parse(typeof(CommonScale), value, true));
        }

        /// <summary>
        /// MeliorScale to string value
        /// </summary>
        /// <param name="scale">The melior scale.</param>
        /// <returns>System.String.</returns>
        public static string CommonToString(this CommonScale scale)
        {
            var retval = string.Empty;

            switch (scale)
            {
                case CommonScale.BigTripleA:
                    retval = "AAA";
                    break;
                case CommonScale.BigDoubleA:
                    retval = "AA";
                    break;
                case CommonScale.BigA:
                    retval = "A";
                    break;
                case CommonScale.SmallA:
                    retval = "a";
                    break;
                case CommonScale.SmallTripleA:
                    retval = "aaa";
                    break;
                case CommonScale.SmallD:
                    retval = "d";
                    break;
                case CommonScale.BigD:
                    retval = "D";
                    break;
                case CommonScale.K:
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
        private static bool ValidateCommonScale(string value)
        {
            CommonScale scale;
            if ((Enum.TryParse(value, true, out scale)) == true)
            {
                return true;
            }
            CommonScaleValues scaleValues;
            if ((Enum.TryParse(value, out scaleValues)) == true)
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
        private static bool ValidateBristolScale(string value)
        {
            int bristol;
            if (int.TryParse(value, out bristol) == true)
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
        private static bool ValidateWeightScale(string value)
        {
            double weight;
            if (double.TryParse(value, out weight) == true)
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

    public static class MeasurementScaleExtensions
    {
        public static string Name(this MeasurementScale.Scale scale)
        {
            return MeasurementScale.GetNameForScale(scale);
        }
    }
}