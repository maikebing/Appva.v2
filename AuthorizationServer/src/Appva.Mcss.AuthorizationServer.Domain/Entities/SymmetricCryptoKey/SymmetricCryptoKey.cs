// <copyright file="SymmetricCryptoKey.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class SymmetricCryptoKey : Entity<SymmetricCryptoKey>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoKey"/> class.
        /// </summary>
        /// <param name="bucket">The bucket</param>
        /// <param name="handle">The handle</param>
        /// <param name="expiration">The expiration</param>
        /// <param name="secret">The secret</param>
        public SymmetricCryptoKey(string bucket, string handle, DateTime expiration, byte[] secret)
        {
            this.Bucket = bucket;
            this.Handle = handle;
            this.ExpiresAt = expiration;
            this.Secret = secret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoKey"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected SymmetricCryptoKey()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The bucket.
        /// </summary>
        public virtual string Bucket
        {
            get;
            protected set;
        }

        /// <summary>
        /// THe handle.
        /// </summary>
        public virtual string Handle
        {
            get;
            protected set;
        }

        /// <summary>
        /// The expiration.
        /// </summary>
        public virtual DateTime ExpiresAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The secret.
        /// </summary>
        public virtual byte[] Secret
        {
            get;
            protected set;
        }

        #endregion
    }
}