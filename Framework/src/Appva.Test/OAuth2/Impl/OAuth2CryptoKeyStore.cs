// <copyright file="OAuth2CryptoStore.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.OAuth2.Impl
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotNetOpenAuth.Messaging.Bindings;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class OAuth2CryptoStore : ICryptoKeyStore
    {
        #region Variables.

        /// <summary>
        /// The crypto keys.
        /// </summary>
        private static IList<SymmetricCryptoKey> CryptoKeys = new List<SymmetricCryptoKey>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2CryptoKeyStore"/> class.
        /// </summary>
        public OAuth2CryptoStore()
        {
        }

        #endregion

        #region ICryptoKeyStore Members

        /// <inheritdoc />
        public CryptoKey GetKey(string bucket, string handle)
        {
            return CryptoKeys.Where(x => x.Bucket == bucket && x.Handle == handle)
                .Select(x => new CryptoKey(x.Secret, x.ExpiresUtc)).SingleOrDefault();
        }

        /// <inheritdoc />
        public IEnumerable<KeyValuePair<string, CryptoKey>> GetKeys(string bucket)
        {
            return CryptoKeys.Where(key => key.Bucket == bucket).OrderByDescending(x => x.ExpiresUtc).ToList()
                .Select(key => new KeyValuePair<string, CryptoKey>(key.Handle, new CryptoKey(key.Secret, key.ExpiresUtc)))
                .ToList();
        }

        /// <inheritdoc />
        public void RemoveKey(string bucket, string handle)
        {
            var item = CryptoKeys.FirstOrDefault(x => x.Bucket == bucket && x.Handle == handle);
            if (item.IsNotNull())
            {
                CryptoKeys.Remove(item);
            }
        }

        /// <inheritdoc />
        public void StoreKey(string bucket, string handle, CryptoKey key)
        {
            CryptoKeys.Add(new SymmetricCryptoKey
            {
                Bucket = bucket,
                Handle = handle,
                Secret = key.Key,
                ExpiresUtc = key.ExpiresUtc
            });
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// TODO Add a descriptive summary to increase readability.
        /// </summary>
        public class SymmetricCryptoKey
        {
            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="SymmetricCryptoKey"/> class.
            /// </summary>
            public SymmetricCryptoKey()
            {
            }

            #endregion

            #region Properties.

            /// <summary>
            /// The bucket.
            /// </summary>
            public string Bucket { get; set; }

            /// <summary>
            /// The handle.
            /// </summary>
            public string Handle { get; set; }

            /// <summary>
            /// The expiration.
            /// </summary>
            public DateTime ExpiresUtc { get; set; }

            /// <summary>
            /// The secret.
            /// </summary>
            public byte[] Secret { get; set; }

            #endregion
        }

        #endregion
    }
}