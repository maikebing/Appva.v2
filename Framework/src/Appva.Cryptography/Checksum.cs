// <copyright file="Checksum.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Cryptography
{
    #region Imports.

    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// Creates a non cryptographic hash using a <see cref="HashAlgorithm"/>. 
    /// Used primarily for easy (unreliable) verification of data integrity.
    /// </summary>
    /// <example>Checksum.Using{MurmurHash}(value: "foo").Build();</example>
    /// <example>Checksum.Using{MurmurHash}(value: "foo").BuildAsBase64();</example>
    /// <example>Checksum.Assert{MurmurHash}(value: "foo").Equals(hash: "bar");</example>
    public static class Checksum
    {
        /// <summary>
        /// Sets the value which will be hashed using a specific <see cref="HashAlgorithm"/>.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="HashAlgorithm"/></typeparam>
        /// <param name="value">The value which is to be hashed</param>
        /// <returns>The hashed value</returns>
        public static INonCryptographicHashBuild<T> Using<T>(string value) 
            where T : HashAlgorithm, new()
        {
            return new NonCryptographicHash<T>(value);
        }

        /// <summary>
        /// Sets the value which will be hashed using a specific <see cref="HashAlgorithm"/>.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="HashAlgorithm"/></typeparam>
        /// <param name="bytes">The bytes</param>
        /// <returns>The hashed value</returns>
        public static INonCryptographicHashBuild<T> Using<T>(byte[] bytes)
            where T : HashAlgorithm, new()
        {
            return new NonCryptographicHash<T>(bytes);
        }

        /// <summary>
        /// Verifies that the original value is equal to the hash.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="HashAlgorithm"/></typeparam>
        /// <param name="unhashed">The original unhashed value</param>
        /// <returns>False if the hash does not match the original value</returns>
        public static INonCryptographicHashEquals<T> Assert<T>(string unhashed) 
            where T : HashAlgorithm, new()
        {
            return new NonCryptographicHash<T>(unhashed);
        }
    }
}
