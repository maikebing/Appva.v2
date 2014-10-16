// <copyright file="OAuth2NonceStore.cs" company="Appva AB">
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
    internal class OAuth2NonceStore : INonceStore
    {
        #region Variables.

        /// <summary>
        /// The nonces.
        /// </summary>
        private static IList<Nonce> Nonces = new List<Nonce>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2NonceStore"/> class.
        /// </summary>
        public OAuth2NonceStore()
        {
        }

        #endregion

        #region INonceStore Members

        /// <inheritdoc />
        public bool StoreNonce(string context, string nonce, DateTime timestampUtc)
        {
            var existingNonce = Nonces.Where(x => x.Context == context && x.Code == nonce && x.Timestamp == timestampUtc)
                .SingleOrDefault();
            if (existingNonce.IsNotNull())
            {
                return false;
            }
            Nonces.Add(new Nonce()
                {
                    Context = context,
                    Code = nonce,
                    Timestamp = timestampUtc
                });
            return true;
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// TODO Add a descriptive summary to increase readability.
        /// </summary>
        public class Nonce
        {
            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="Nonce"/> class.
            /// </summary>
            public Nonce()
            {
            }

            #endregion

            #region Properties.

            /// <summary>
            /// The nonce context.
            /// </summary>
            public string Context { get; set; }

            /// <summary>
            /// The nonce code.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// The nonce timestamp.
            /// </summary>
            public DateTime Timestamp { get; set; }

            #endregion
        }
        #endregion
    }
}