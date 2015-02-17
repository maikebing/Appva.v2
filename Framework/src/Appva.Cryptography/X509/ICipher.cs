// <copyright file="ICipher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Security;

    #endregion

    /// <summary>
    /// Key pair generator interface.
    /// </summary>
    public interface ICipher
    {
        /// <summary>
        /// Returns a <see cref="SecureRandom"/>
        /// </summary>
        SecureRandom Random
        {
            get;
        }

        /// <summary>
        /// Creates and initialize a new instance of <see cref="IAsymmetricCipherKeyPairGenerator"/>.
        /// </summary>
        /// <returns>A <see cref="IAsymmetricCipherKeyPairGenerator"/></returns>
        IAsymmetricCipherKeyPairGenerator CreateNew();
    }
}
