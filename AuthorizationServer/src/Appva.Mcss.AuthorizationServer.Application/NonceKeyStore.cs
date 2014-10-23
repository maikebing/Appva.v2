// <copyright file="NonceKeyStore.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using DotNetOpenAuth.Messaging.Bindings;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NonceKeyStore : INonceStore
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NonceKeyStore"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public NonceKeyStore(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region INonceStore Members.

        /// <inheritdoc />
        public bool StoreNonce(string context, string nonce, DateTime timestampUtc)
        {
            var existingNonce = this.persistenceContext.QueryOver<Nonce>()
                .Where(x => x.Context == context)
                .And(x => x.Code == nonce)
                .And(x => x.Timestamp == timestampUtc)
                .SingleOrDefault();
            if (existingNonce.IsNotNull())
            {
                return false;
            }
            try
            {
                this.persistenceContext.Save(new Nonce(context, nonce, timestampUtc));
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        #endregion
    }
}