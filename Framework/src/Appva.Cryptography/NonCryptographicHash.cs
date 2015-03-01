// <copyright file="NonCryptographicHash.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography
{
    #region Imports.

    using System.Globalization;
    using System.Security.Cryptography;
    using Core.Extensions;
    using Validation;

    #endregion

    //// Interface constraints in order to make the fluent interface follow
    //// explicitly.

    #region Constraints.

    /// <summary>
    /// Constraint for all bit hash build implementations.
    /// </summary>
    public interface INonCryptographicHashBuild
    {
        /// <summary>
        /// Creates a string representation of the hash.
        /// </summary>
        /// <returns>The hashed string</returns>
        string Build();

        /// <summary>
        /// Creates a base64 representation of the hash. 
        /// </summary>
        /// <returns>The hashed string as base64</returns>
        string BuildAsBase64();
    }

    /// <summary>
    /// Constraint for verify equality.
    /// </summary>
    public interface INonCryptographicHashEquals
    {
        /// <summary>
        /// Checks whether or not the unhashed is equal to the
        /// hashed hash.
        /// </summary>
        /// <param name="hashed">The hashed value</param>
        /// <returns>False if the hashed value not equals the unhashed value</returns>
        bool Equals(string hashed);

        /// <summary>
        /// Checks whether or not the unhashed is equal to the
        /// hashed hash.
        /// </summary>
        /// <param name="hashed">The hashed value</param>
        /// <param name="format">If the hash has a specific format, e.g. base64</param>
        /// <returns>False if the hashed value not equals the unhashed value</returns>
        bool Equals(string hashed, HashFormat format);
    }

    #endregion

    /// <summary>
    /// Implementation of hash functions.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="HashAlgorithm"/></typeparam>
    internal sealed class NonCryptographicHash<T> : 
        INonCryptographicHashBuild,
        INonCryptographicHashEquals
        where T : HashAlgorithm, new()
    {
        #region Variables.

        /// <summary>
        /// The value to be hashed.
        /// </summary>
        private readonly byte[] value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NonCryptographicHash{T}"/> class.
        /// </summary>
        /// <param name="value">The value to be hashed</param>
        public NonCryptographicHash(string value)
        {
            Requires.NotNullOrEmpty(value, "value");
            this.value = value.ToUtf8Bytes();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonCryptographicHash{T}"/> class.
        /// </summary>
        /// <param name="bytes">The bytes</param>
        public NonCryptographicHash(byte[] bytes)
        {
            Requires.NotNull(bytes, "bytes");
            this.value = bytes;
        }

        #endregion

        #region Private Properties.

        /// <summary>
        /// Returns a stringified hash based on hash size.
        /// </summary>
        private string Hash
        {
            get
            {
                int size;
                var bytes = this.ComputeHash(out size);
                switch (size)
                {
                    case 64:
                        return bytes.ToUInt64().ToString(CultureInfo.InvariantCulture);
                    case 32:
                        return bytes.ToUInt32().ToString(CultureInfo.InvariantCulture);
                    default:
                        return bytes.ToHex();
                }
            }
        }

        #endregion

        #region INonCryptographicHashBuild<T> Members.

        /// <inheritdoc />
        string INonCryptographicHashBuild.Build()
        {
            return this.Hash;
        }

        /// <inheritdoc />
        string INonCryptographicHashBuild.BuildAsBase64()
        {
            return this.Hash.ToBase64();
        }

        #endregion

        #region INonCryptographicHashEquals<T> Members.

        /// <inheritdoc />
        bool INonCryptographicHashEquals.Equals(string hashed)
        {
            Requires.NotNullOrEmpty(hashed, "hashed");
            return this.IsHashEqual(hashed, HashFormat.None); 
        }

        /// <inheritdoc />
        bool INonCryptographicHashEquals.Equals(string hashed, HashFormat format)
        {
            Requires.NotNullOrEmpty(hashed, "hashed");
            return this.IsHashEqual(hashed, format);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Computes the hash as a byte array.
        /// </summary>
        /// <param name="hashSize">The hash size</param>
        /// <returns>The hash as a byte array</returns>
        private byte[] ComputeHash(out int hashSize)
        {
            using (var provider = new T())
            {
                hashSize = provider.HashSize;
                return provider.ComputeHash(this.value);
            }
        }

        /// <summary>
        /// Checks whether or not the unhashed is equal to the
        /// hashed hash.
        /// </summary>
        /// <param name="hashed">The hashed string</param>
        /// <param name="withCustomFormat">The custom hash format</param>
        /// <returns>False if the hashed value not equals the unhashed value</returns>
        private bool IsHashEqual(string hashed, HashFormat withCustomFormat)
        {
            switch (withCustomFormat)
            {
                case HashFormat.None:
                    return this.Hash.Equals(hashed);
                case HashFormat.Base64:
                    return this.Hash.ToBase64().Equals(hashed);
                default:
                    return false;
            }
        }

        #endregion
    }
}
