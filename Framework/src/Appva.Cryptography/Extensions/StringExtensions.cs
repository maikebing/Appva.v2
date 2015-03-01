// <copyright file="StringExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.Extensions
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Collection of <see cref="String"/> extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// A Fischer-Yates shuffle.
        /// </summary>
        /// <param name="value">The string to be shuffles</param>
        /// <returns>A shuffled string</returns>
        public static string Shuffle(this string value)
        {
            var data = value.ToCharArray();
            var randomizer = new Random(DateTime.Now.Millisecond);
            int count = data.Length;
            while (count > 1)
            {
                var random = randomizer.Next(0, count);
                var tmp = data[random];
                data[random] = data[count];
                data[count] = tmp;
                count--;
            }
            return new string(data);
        }
    }
}
