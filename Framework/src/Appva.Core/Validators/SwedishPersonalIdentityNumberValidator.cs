// <copyright file="SwedishPersonalIdentityNumberValidator.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Core
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Extensions;

    #endregion

    /// <summary>
    /// The century omittion mode.
    /// </summary>
    public enum SwedishPersonalIdentityNumberCenturyOmittionMode
    {
        /// <summary>
        /// The century digits must exist; a 12 digit personal identity number.
        /// </summary>
        Disallow,

        /// <summary>
        /// The century digits are allowed to be omitted; a 10 digit personal identity 
        /// number.
        /// </summary>
        Allow
    }

    /// <summary>
    /// All persons who are registered in Sweden are given a personal identity number. 
    /// The personal identity number is an identity designation that you retain your 
    /// whole life. The personal identity number consists of a person’s date of birth, a 
    /// birth number and a check digit. The only information that can be gleaned from 
    /// your personal identity number is your date of birth and sex. The sex is 
    /// specified by the second to last digit, and is odd for males and even for 
    /// females.
    /// <para>
    /// A co-ordination number is an identification for people who are not or have not 
    /// been registered in Sweden. The purpose of co-ordination numbers is so that 
    /// public agencies and other functions in society are able to identify people even 
    /// if they are not registered in Sweden. The Swedish Tax Agency allocates 
    /// co-ordination numbers upon request. All Government authorities and certain 
    /// private higher education institutions have theright to request co-ordination 
    /// numbers.
    /// </para>
    /// <externalLink>
    ///     <linkText>Personal Identity Number</linkText>
    ///     <linkUri>
    ///         http://www.skatteverket.se/download/18.8dcbbe4142d38302d74be9/1387372677650/717B06.pdf
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SwedishPersonalIdentityNumberValidator
    {
        #region Variables.

        /// <summary>
        /// A '+' separator the year the a person is 100 years old.
        /// </summary>
        private static readonly string Plus = "+";

        /// <summary>
        /// A '-' separator.
        /// </summary>
        private static readonly string Minus = "-";

        /// <summary>
        /// Valid centuries.
        /// <externalLink>
        ///     <linkText>Oldest living person in Sweden</linkText>
        ///     <linkUri>
        ///         http://sv.wikipedia.org/wiki/Sveriges_%C3%A4ldsta_personer
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        private static readonly IReadOnlyList<string> ValidCenturies = new List<string> { "19", "20" };

        #endregion

        #region Public Functions.

        /// <summary>
        /// Validates a Swedish personal identity number or a Swedish co-ordination number.
        /// </summary>
        /// <param name="str">
        /// The string representation of a Swedish personal identity number to check
        /// </param>
        /// <param name="omit">Whether or not century is allowed to be omitted</param>
        /// <returns>True if it is a valid personal identity number</returns>
        public static bool IsValid(string str, SwedishPersonalIdentityNumberCenturyOmittionMode omit = SwedishPersonalIdentityNumberCenturyOmittionMode.Disallow)
        {
            var test = str.ReplaceAll(Plus, Minus);
            if (test.IsEmpty())
            {
                return false;
            }
            if (! (test.Length == 10 || test.Length == 12))
            {
                return false;
            }
            if (test.Length == 10 && omit == SwedishPersonalIdentityNumberCenturyOmittionMode.Disallow)
            {
                return false;
            }
            if (! test.All(char.IsDigit))
            {
                return false;
            }
            if (test.Length == 10)
            {
                test = string.Concat(ValidCenturies[0], test);
                if (str.Contains(Plus) && ! IsValidAge(test))
                {
                    return false;
                }
                //// Could check (20000101-1111) 000101 with today 2015, 00 < 15 = born either way so check for plus sign.
                //// if (int.parse(substring(2, 2)) <= 15) { if (!str.contains(Plus)) { } }
                //// Return an error message e.g. new SwedishPersonalIdentityNumberValidationResult("The PIN is ambigious")
            }
            if (! IsValidDate(test))
            {
                return false;
            }
            if (! IsValidChecksum(test))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates a Swedish personal identity number or a Swedish co-ordination number.
        /// </summary>
        /// <param name="str">
        /// The string representation of a Swedish personal identity number to check
        /// </param>
        /// <param name="omit">Whether or not century is allowed to be omitted</param>
        /// <returns>True if it is a valid personal identity number</returns>
        public static bool IsValid(object obj, SwedishPersonalIdentityNumberCenturyOmittionMode omit = SwedishPersonalIdentityNumberCenturyOmittionMode.Disallow)
        {
            return IsValid(obj as string, omit);
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Validates a the age if a '+' is used as a separator.
        /// </summary>
        /// <param name="personalIdentityNumber">
        /// The personal identity number to check
        /// </param>
        /// <returns>True if the age is 100 or above</returns>
        private static bool IsValidAge(string personalIdentityNumber)
        {
            var year = int.Parse(personalIdentityNumber.Substring(0, 4));
            return (DateTime.Now.Year - year) >= 100;
        }

        /// <summary>
        /// Checks that the personal identity number has a valid century, 
        /// is a valid date (year, month, day) and not a future date.
        /// </summary>
        /// <param name="personalIdentityNumber">
        /// The personal identity number to check
        /// </param>
        /// <returns>True if the date is valid</returns>
        private static bool IsValidDate(string personalIdentityNumber)
        {
            if (! IsValidCentury(personalIdentityNumber))
            {
                return false;
            }
            var skipDay = IsCoordinationNumber(personalIdentityNumber);
            DateTime result;
            if (! DateTime.TryParseExact(personalIdentityNumber.Substring(0, skipDay ? 6 : 8), skipDay ? "yyyyMM" : "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return false;
            }
            return result < DateTime.Now;
        }
        
        /// <summary>
        /// Checks whether or not the century is valid
        /// </summary>
        /// <param name="personalIdentityNumber">
        /// The personal identity number to check
        /// </param>
        /// <returns>True if a valid century</returns>
        private static bool IsValidCentury(string personalIdentityNumber)
        {
            return ValidCenturies.Contains(personalIdentityNumber.Substring(0, 2));
        }

        /// <summary>
        /// Checks whether or not the personal identity number is a co-ordination 
        /// number.
        /// </summary>
        /// <param name="personalIdentityNumber">
        /// The personal identity number to check
        /// </param>
        /// <returns>True if it is a co-ordination number</returns>
        private static bool IsCoordinationNumber(string personalIdentityNumber)
        {
            return int.Parse(personalIdentityNumber.Substring(6, 1)) > 3;
        }

        /// <summary>
        /// Checks whether or not the personal identity number has a correct 
        /// checksum number.
        /// </summary>
        /// <param name="personalIdentityNumber">
        /// The personal identity number to check
        /// </param>
        /// <returns>True if the checksum is correct</returns>
        private static bool IsValidChecksum(string personalIdentityNumber)
        {
            personalIdentityNumber = personalIdentityNumber.Substring(2);
            int checksum = 0;
            for (int i = 0; i < personalIdentityNumber.Length; i++)
            {
                int current = int.Parse(personalIdentityNumber[i].ToString()) * ((i % 2 == 0) ? 2 : 1);
                checksum += (current > 9) ? current - 9 : current;
            }
            return checksum % 10 == 0;
        }

        #endregion
    }
}