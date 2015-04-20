// <copyright file="EncryptionUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class EncryptionUtils
    {

        #region Private Fields.

        /// <summary>
        /// Default iterations.
        /// </summary>
        private const int DefaultIterations = 1000;

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a hash from value and salt.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        public static string Hash(string value, string salt)
        {
            var i = salt.IndexOf('.');
            var iterations = int.Parse(salt.Substring(0, i), System.Globalization.NumberStyles.HexNumber);
            salt = salt.Substring(i + 1);
            using (var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(value), Convert.FromBase64String(salt), iterations))
            {
                var key = pbkdf2.GetBytes(24);
                return Convert.ToBase64String(key);
            }
        }

        /// <summary>
        /// Generates a salt.
        /// </summary>
        public static string GenerateSalt(DateTime dateTime, int? iterations = null)
        {
            return GenerateSalt(dateTime.Ticks, iterations);
        }

        /// <summary>
        /// Generates a salt.
        /// </summary>
        public static string GenerateSalt(long salt, int? iterations = null)
        {
            if (iterations.HasValue && iterations.Value < DefaultIterations)
            {
                throw new ArgumentException(string.Format("Cannot be less than {0} iterations", DefaultIterations));
            }
            else
            {
                iterations = DefaultIterations;
            }
            var bytes = BitConverter.GetBytes(salt);
            return string.Format("{0}.{1}", iterations.Value, Convert.ToBase64String(bytes));
        }

        /// <summary>
        /// Creates a default password.
        /// </summary>
        public static string GeneratePassword()
        {
            var retval = string.Empty;
            retval += GetNumberOfCharactersFrom("abcdefghijklmnopqrstuvwxyz", 3);
            retval += GetNumberOfCharactersFrom("0123456789", 1);
            retval += GetNumberOfCharactersFrom("!@#$%&()?", 1);
            retval += GetNumberOfCharactersFrom("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3);
            Random random = new Random();
            return new string(retval.ToCharArray().OrderBy(s => (random.Next(2) % 2) == 0).ToArray());
        }

        /// <summary>
        /// Helper for generating password.
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="length"></param>
        private static string GetNumberOfCharactersFrom(string characters, int length)
        {
            Random random = new Random();
            StringBuilder buffer = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int randomNumber = random.Next(0, characters.Length);
                buffer.Append(characters, randomNumber, 1);
            }
            return buffer.ToString();
        }

        #endregion

    }
}