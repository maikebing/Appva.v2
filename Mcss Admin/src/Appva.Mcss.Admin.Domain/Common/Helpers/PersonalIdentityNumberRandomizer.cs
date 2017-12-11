// <copyright file="PersonalIdentityNumberRandomizer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class PersonalIdentityNumberRandomizer
    {
        /// <summary>
        /// The gender third digit.
        /// </summary>
        private static readonly IReadOnlyDictionary<Gender, IReadOnlyList<int>> ThirdDigits = new Dictionary<Gender, IReadOnlyList<int>> 
        { 
            { Gender.Male,   new List<int> { 1, 3, 5, 7, 9 } },
            { Gender.Female, new List<int> { 0, 2, 4, 6, 8 } }
        };

        /// <summary>
        /// Creates a random valid <see cref="PersonalIdentityNumber"/>
        /// </summary>
        /// <param name="gender">The gender</param>
        /// <returns>A new <see cref="PersonalIdentityNumber"/> instance</returns>
        public static PersonalIdentityNumber Random(Gender gender)
        {
            var date     = new DateTime(RandomNumber.CreateNew(1900, 1999), RandomNumber.CreateNew(1, 12), 1, 0, 0, 0, 0, DateTimeKind.Local);
            var digits   = (RandomNumber.CreateNew(0, 99) * 10) + ThirdDigits[gender][RandomNumber.CreateNew(0, ThirdDigits[gender].Count)];
            var datePart = new DateTime(date.Year, date.Month, RandomNumber.CreateNew(1, date.DaysInMonth()), 0, 0, 0, 0, DateTimeKind.Local);
            var personalIdentityNumber = "{0:yyyyMMdd}-{1}".FormatWith(datePart, digits.ToString().PadLeft(3, '0'));
            return PersonalIdentityNumber.New(personalIdentityNumber + CreateControlDigit(personalIdentityNumber));
        }

        /// <summary>
        /// Creates a personal identity checksum.
        /// </summary>
        /// <param name="personalIdentityNumber">The personal identity number</param>
        /// <returns>A checksum</returns>
        private static int CreateControlDigit(string personalIdentityNumber)
        {
            var checksum = 0;
            var temp     = personalIdentityNumber.Substring(2).ReplaceAll("-");
            for (var i = 0; i < temp.Length; i++)
            {
                var current  = int.Parse(temp[i].ToString()) * (i % 2 == 0 ? 2 : 1);
                checksum    += current > 9 ? current - 9 : current;
            }
            var control = checksum % 10;
            return control == 0 ? 0 : 10 - control;
        }
    }
}