// <copyright file="Hash.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography
{
    #region Imports.

    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// Creates a non cryptographic hash using a <see cref="HashAlgorithm"/>. Used 
    /// primarily for easy (unreliable) verification of data integrity.
    /// </summary>
    /// <example>
    ///     <code language="cs" title="Example #1">
    ///         Hash.Using{MurmurHash} (value: "foo").Build();
    ///     </code>
    /// </example>
    /// <example>
    ///     <code language="cs" title="Example #2">
    ///         Hash.Using{MurmurHash} (value: "foo").BuildAsBase64();
    ///     </code>
    /// </example>
    /// <example>
    ///     <code language="cs" title="Example #3">
    ///         Hash.Assert{MurmurHash}(value: "foo").Equals(hash: "bar");
    ///     </code>
    /// </example>
    public static class Hash
    {
        /// <summary>
        /// Sets the value which will be hashed using a specific <see cref="HashAlgorithm"/>.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="HashAlgorithm"/></typeparam>
        /// <param name="value">The value which is to be hashed</param>
        /// <returns>The hashed value</returns>
        public static INonCryptographicHashBuild Using<T>(string value)
            where T : HashAlgorithm, new()
        {
            return new NonCryptographicHash<T>(value);
        }

        /// <summary>
        /// Simply creates a random hash, handy for e.g. salts. 
        /// </summary>
        /// <param name="byteSize">Optional byte size</param>
        /// <returns>A byte hash</returns>
        public static byte[] Random(int byteSize = 32)
        {
            var bytes = new byte[byteSize];
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                randomNumberGenerator.GetBytes(bytes);
            }
            return bytes;
        }
    }
}
