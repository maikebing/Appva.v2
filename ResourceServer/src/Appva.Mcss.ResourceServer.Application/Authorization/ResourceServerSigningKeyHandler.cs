// <copyright file="ResourceServerSigningKeyHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Authorization
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// Responsible for providing the key to verify the token is 
    /// intended for this resource.
    /// </summary>
    public sealed class ResourceServerSigningKeyHandler : ISigningKeyHandler
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="RSACryptoServiceProvider"/>.
        /// </summary>
        private readonly RSACryptoServiceProvider cspProvider;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceServerSigningKeyHandler"/> class.
        /// </summary>
        public ResourceServerSigningKeyHandler()
            : this(ConfigurationManager.AppSettings.Get("ResourceServerPrivateKey"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceServerSigningKeyHandler"/> class.
        /// </summary>
        /// <param name="key">The signing key</param>
        public ResourceServerSigningKeyHandler(string key)
        {
            this.cspProvider = new RSACryptoServiceProvider();
            this.cspProvider.FromXmlString(key.FromBase64().ToUtf8());
        }

        #endregion

        #region Public Properties.

        /// <inheritdoc />
        public RSACryptoServiceProvider Provider
        {
            get
            {
                return this.cspProvider;
            }
        }

        #endregion
    }
}