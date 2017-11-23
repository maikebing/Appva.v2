// <copyright file="Signature.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Appva.Core.Extensions;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Signature : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="signator">The signator.</param>
        /// <param name="data">The signed data.</param>
        public Signature(Account signator, IList<SignedData> data)
        {
            Requires.NotNull(signator,       "signator"  );
            Requires.NotNull(data,           "data"      );
            Requires.Range  (data.Count > 0, "data.Count");
            this.Who      = signator;
            this.Data     = data;
            this.Checksum = this.ComputeChecksum();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Signature()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The signator.
        /// </summary>
        public virtual Account Who
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The data which is signed.
        /// </summary>
        public virtual IList<SignedData> Data
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The checksum to verify data integrity (unintentional corruption).
        /// </summary>
        /// <remarks>Uses MD5.</remarks>
        public virtual string Checksum
        {
            get;
            internal protected set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="type">The signature type.</param>
        /// <param name="signator">The signator.</param>
        /// <param name="data">The signed data.</param>
        /// <returns>A new <see cref="Signature"/> instance.</returns>
        public static Signature New(Account signator, IList<SignedData> data)
        {
            return new Signature(signator, data);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Calculates the checksum.
        /// </summary>
        /// <returns>A checksum string representation.</returns>
        private string ComputeChecksum()
        {
            var data = string.Join(".", this.Data.Select(x => x.Data).ToArray());
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data.ToUtf8Bytes()).ToHex();
            }
        }

        #endregion
    }
}