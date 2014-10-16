// <copyright file="Password.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Cryptography
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Core.Extensions;
    using Validation;

    #endregion

    /// <summary>
    /// Helper class for password generation, hashing and verification.
    /// </summary>
    /// <example>Password.Pbkdf2(plainText: "foo");</example>
    /// <example>Password.Equals(plainText: "foo", hashedPassword: "4934.a+fwhJygTAB82U2Umlvhgs8YYhr8myJmXALnG4YwNDI=.K3wLfSYqEZTlSSQcXmYtuPqOnD4SJBdBfVPJHWMnnG8=");</example>
    /// <example>Password.Random();</example>
    public static class Password
    {
        /// <summary>
        /// A salted (256 bit) PBKDF2 (256 bit) with a random
        /// iteration between 10000 and 50000.
        /// Password format: {iterations}.{base64 salt}.{base64 hashed password}.
        /// See <a href="http://en.wikipedia.org/wiki/PBKDF2">PBKDF2</a>
        /// </summary>
        /// <param name="plainText">The password as plain text</param>
        /// <returns>A PBKDF2 hashed password</returns>
        public static string Pbkdf2(string plainText)
        {
            Requires.NotNull(plainText, "plainText");
            var salt = Hash.Random();
            var iterations = new Random().Next(10000, 50000);
            using (var pbkdf2 = new Rfc2898DeriveBytes(plainText.ToUtf8Bytes(), salt, iterations))
            {
                var password = pbkdf2.GetBytes(32);
                return string.Join(".", iterations, salt.ToBase64(), password.ToBase64());
            }
        }

        /// <summary>
        /// If an unhashed password is equal to the hashed password.
        /// </summary>
        /// <param name="plainText">The password as plain text</param>
        /// <param name="hashedPassword">The hashed password</param>
        /// <returns>True if the unhashed password is equal</returns>
        public static bool Equals(string plainText, string hashedPassword)
        {
            if (plainText.IsEmpty() || hashedPassword.IsEmpty())
            {
                return false;
            }
            var frgm = hashedPassword.Split('.');
            var iter = int.Parse(frgm[0]);
            var salt = Convert.FromBase64String(frgm[1]);
            using (var pbkdf2 = new Rfc2898DeriveBytes(plainText.ToUtf8Bytes(), salt, iter))
            {
                var password = pbkdf2.GetBytes(32).ToBase64();
                return TimingAttackEquals(password.ToUtf8Bytes(), frgm[2].ToUtf8Bytes());
            }
        }

        /// <summary>
        /// If an unhashed password is NOT equal to the hashed password.
        /// </summary>
        /// <param name="plainText">The password as plain text</param>
        /// <param name="hashedPassword">The hashed password</param>
        /// <returns>True if the unhashed password is NOT equal</returns>
        public static bool NotEquals(string plainText, string hashedPassword)
        {
            return ! Equals(plainText, hashedPassword);
        }

        /// <summary>
        /// Generates a random password with the format 4114 -
        /// three a-z characters, one digit, one special character
        /// and three A-Z characters. If shuffle is used then the
        /// latter format will be shuffled in random order. 
        /// </summary>
        /// <param name="shuffle">Whether to keep the 4114 format or shuffle the result</param>
        /// <returns>A random password</returns>
        public static string Random(bool shuffle = false)
        {
            var latoz = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var num   = "0123456789".ToCharArray();
            var spec  = "!@#$%&?".ToCharArray();
            var uatoz = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            return Random(
                10,
                new Dictionary<char[], int>
                {
                    { latoz, 4 },
                    { num,   1 },
                    { spec,  1 },
                    { uatoz, 4 }
                },
                shuffle);
        }

        /// <summary>
        /// Generates a random password by the dictionary key values.
        /// </summary>
        /// <param name="length">The total length of the random string</param>
        /// <param name="items">Key (characters available) value (number of characters to pick)</param>
        /// <param name="shuffle">Whether to keep the original format of the items or shuffle the result</param>
        /// <returns>A random password</returns>
        public static string Random(int length, IDictionary<char[], int> items, bool shuffle = false)
        {
            var randomizer = new Random(DateTime.Now.Millisecond);
            var sequence = new char[length];
            var seqIndex = 0;
            foreach (var item in items)
            {
                for (var i = 0; i < item.Value; i++)
                {
                    var random = randomizer.Next(0, item.Key.Length);
                    if (seqIndex > 0)
                    {
                        while (sequence[seqIndex-1].Equals(item.Key[random]))
                        {
                            random = randomizer.Next(0, item.Key.Length);
                        }
                    }
                    sequence[seqIndex] = item.Key[random];
                    seqIndex++;
                }
            }
            var password = new string(sequence);
            return shuffle ? password.Shuffle() : password;
        }

        /// <summary>
        /// Prevent timing attack with a slow equals.
        /// See <a href="http://en.wikipedia.org/wiki/Timing_attack">Timing attack</a>
        /// </summary>
        /// <param name="a">The first byte array</param>
        /// <param name="b">The second byte array</param>
        /// <returns>True if the byte arrays are equal</returns>
        private static bool TimingAttackEquals(byte[] a, byte[] b)
        {
            var diff = a.Length ^ b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }
    }
}