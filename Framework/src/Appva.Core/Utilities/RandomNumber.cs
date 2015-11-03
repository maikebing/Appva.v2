// <copyright file="RandomNumber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Utilities
{
    #region Imports.

    using System;
    using System.Security.Cryptography;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class RandomNumber
    {
        /// <summary>
        /// The <see cref="RNGCryptoServiceProvider"/>.
        /// </summary>
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        /// <summary>
        /// Creates a new random number.
        /// </summary>
        /// <param name="minValue">The minimum value, defaults to 0</param>
        /// <param name="maxValue">The maximum value, defaults to <see cref="uint.MaxValue"/></param>
        /// <returns>A real random number</returns>
        public static int CreateNew(int minValue = 0, int maxValue = int.MaxValue)
        {
            Requires.ValidState(maxValue > 0, "Maximum value must be greater than zero");
            var bytes = new byte[4];
            Provider.GetBytes(bytes);
            var value = (double) BitConverter.ToUInt32(bytes, 0) / uint.MaxValue;
            var range = (long) maxValue - minValue;
            return (int)((long) Math.Floor(value * range) + minValue);
        }
    }
}