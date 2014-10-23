// <copyright file="SymmetricCryptoKeyStore.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using DotNetOpenAuth.Messaging.Bindings;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed class SymmetricCryptoKeyStore : ICryptoKeyStore
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoKeyStore"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public SymmetricCryptoKeyStore(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region ICryptoKeyStore Members.

        /// <inheritdoc />
        public CryptoKey GetKey(string bucket, string handle)
        {
            var keys = this.persistenceContext.QueryOver<SymmetricCryptoKey>()
                .Where(x => x.Bucket == bucket)
                .And(x => x.Handle == handle)
                .List();
            var matches = keys.Where(x => string.Equals(x.Bucket, bucket, StringComparison.Ordinal) && string.Equals(x.Handle, handle, StringComparison.Ordinal))
                .Select(x => new CryptoKey(x.Secret, x.ExpiresAt.ToUtc()));
            return matches.FirstOrDefault();
        }

        /// <inheritdoc />
        public IEnumerable<KeyValuePair<string, CryptoKey>> GetKeys(string bucket)
        {
            var kvs = this.persistenceContext.QueryOver<SymmetricCryptoKey>()
                .Where(x => x.Bucket == bucket)
                .OrderBy(x => x.ExpiresAt).Desc
                .List()
                .Select(x => new KeyValuePair<string, CryptoKey>(x.Handle, new CryptoKey(x.Secret, x.ExpiresAt.ToUtc())))
                .ToList();
            return kvs;
        }

        /// <inheritdoc />
        public void RemoveKey(string bucket, string handle)
        {
            var match = this.persistenceContext.QueryOver<SymmetricCryptoKey>()
                .Where(x => x.Bucket == bucket)
                .And(x => x.Handle == handle)
                .SingleOrDefault();
            if (match.IsNotNull())
            {
                this.persistenceContext.Delete(match);
            }
        }

        /// <inheritdoc />
        public void StoreKey(string bucket, string handle, CryptoKey key)
        {
            var symmetricCryptoKey = new SymmetricCryptoKey(bucket, handle, key.ExpiresUtc, key.Key);
            this.persistenceContext.Save(symmetricCryptoKey);
        }

        #endregion
    }
}